using System;
using Godot;

public partial class Manual : Node3D
{
    [Export]
    float AnimSpeed = 6;

    Node3D Riser;

    int currentPageNumber = 1;

    AnimationPlayer TrackedAnimationPlayer;
    int CurrentMovement;

    const int LAST_PAGE = 8;

    public override void _Ready()
    {
        base._Ready();

        foreach (var it in this.FindChildrenByType<MeshInstance3D>())
        {
            it.Layers = 2;
        }

        this.FindChildByName<Node>("Page1").FindChildByType<AnimationPlayer>().SpeedScale = AnimSpeed;
        this.FindChildByName<Node>("Page2").FindChildByType<AnimationPlayer>().SpeedScale = AnimSpeed;
        this.FindChildByName<Node>("Page3").FindChildByType<AnimationPlayer>().SpeedScale = AnimSpeed;

        UpdatePages();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event.IsActionPressed("advance_manual"))
        {
            AdvancePages();
        }

        if (@event.IsActionPressed("retreat_manual"))
        {
            RetreatPages();
        }
    }

    void UpdatePages()
    {
        this.FindChildByName<Node3D>("Page1").Position = new Vector3(0, 0.081f, 0);
        this.FindChildByName<Node3D>("Page2").Position = new Vector3(0, 0.081f, 0);
        this.FindChildByName<Node3D>("Page3").Position = new Vector3(0, 0.081f, 0);

        this.FindChildByName<Node>("Page1").FindChildByType<AnimationPlayer>().CurrentAnimation = "LeftSide";
        this.FindChildByName<Node>("Page2").FindChildByType<AnimationPlayer>().CurrentAnimation = "RightSide";
        this.FindChildByName<Node3D>("Page3").Visible = false;

        this.FindChildByName<Node>("Page1").FindChildByType<MeshInstance3D>().MaterialOverride = CreatePageMat(ManPageResource(currentPageNumber), ManPageResource(currentPageNumber - 1), true, false);
        this.FindChildByName<Node>("Page2").FindChildByType<MeshInstance3D>().MaterialOverride = CreatePageMat(ManPageResource(currentPageNumber + 2), ManPageResource(currentPageNumber + 1), true, false);
    }

    string ManPageResource(int idx)
    {
        return $"res://manual/pages/page-{idx}.png";
    }

    void AdvancePages()
    {
        if (currentPageNumber >= (LAST_PAGE - 1)) return;

        this.FindChildByName<Node>("Page2").FindChildByType<AnimationPlayer>().CurrentAnimation = "RightToLeft";
        Riser = this.FindChildByName<Node3D>("Page2");
        //this.FindChildByName<Node3D>("Page2").Position = new Vector3(0, 0.15f, 0);
        TrackedAnimationPlayer = this.FindChildByName<Node>("Page2").FindChildByType<AnimationPlayer>();
        CurrentMovement = 1;

        this.FindChildByName<Node>("Page3").FindChildByType<AnimationPlayer>().CurrentAnimation = "RightSide";
        this.FindChildByName<Node>("Page3").FindChildByType<MeshInstance3D>().MaterialOverride = CreatePageMat(ManPageResource(currentPageNumber + 4), ManPageResource(currentPageNumber + 3), false, false);
        this.FindChildByName<Node3D>("Page3").Visible = true;
    }

    void RetreatPages()
    {
        if (currentPageNumber <= 1) return;

        this.FindChildByName<Node>("Page1").FindChildByType<AnimationPlayer>().CurrentAnimation = "LeftToRight";
        Riser = this.FindChildByName<Node3D>("Page1");
        //this.FindChildByName<Node3D>("Page1").Position = new Vector3(0, 0.15f, 0);
        TrackedAnimationPlayer = this.FindChildByName<Node>("Page1").FindChildByType<AnimationPlayer>();
        CurrentMovement = -1;

        this.FindChildByName<Node>("Page3").FindChildByType<AnimationPlayer>().CurrentAnimation = "LeftSide";
        this.FindChildByName<Node>("Page3").FindChildByType<MeshInstance3D>().MaterialOverride = CreatePageMat(ManPageResource(currentPageNumber - 2), ManPageResource(currentPageNumber - 1), true, false);
        this.FindChildByName<Node3D>("Page3").Visible = true;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (TrackedAnimationPlayer != null)
        {
            //GD.Print(TrackedAnimationPlayer.CurrentAnimationPosition);
            if (TrackedAnimationPlayer.CurrentAnimationPosition >= 2.45f && CurrentMovement == 1)
            {
                currentPageNumber += 2;
                UpdatePages();

                CurrentMovement = 0;
                TrackedAnimationPlayer = null;
            }
            else if (TrackedAnimationPlayer.CurrentAnimationPosition >= 2.45f && CurrentMovement == -1)
            {
                currentPageNumber -= 2;
                UpdatePages();

                CurrentMovement = 0;
                TrackedAnimationPlayer = null;
            }
        }

        if (TrackedAnimationPlayer != null && Riser != null && Riser.Position.Y < 0.15f)
        {
            Riser.Position += Vector3.Up * (float)delta * .5f;
        }
        // else
        // {
        //     for (int i = 0; i < 3; ++i)
        //     {
        //         var n = this.FindChildByName<Node3D>($"Page{i + 1}");
        //         if (n.Position.Y > 0.081f)
        //         {
        //             n.Position += Vector3.Down * (float)delta * .5f;
        //         }
        //         else
        //         {
        //             n.Position = new Vector3(0, 0.081f, 0);
        //         }
        //     }
        // }


    }

    ShaderMaterial CreatePageMat(string frontURL, string backURL, bool flipFrontHorizontally, bool flipBackHorizontally)
    {
        var ret = (ShaderMaterial)GD.Load<ShaderMaterial>("res://materials/PageMat.tres").Duplicate();

        if (ResourceLoader.Exists(frontURL)) ret.SetShaderParameter("texture_front", GD.Load<Texture2D>(frontURL));
        if (ResourceLoader.Exists(backURL)) ret.SetShaderParameter("texture_back", GD.Load<Texture2D>(backURL));
        ret.SetShaderParameter("flip_front_horizontally", flipFrontHorizontally);
        ret.SetShaderParameter("flip_back_horizontally", flipBackHorizontally);

        return ret;
    }
}
