using System;
using Godot;

public partial class Manual : Node3D
{
    int currentPageNumber = 1;

    AnimationPlayer TrackedAnimationPlayer;
    int CurrentMovement;

    public override void _Ready()
    {
        base._Ready();

        foreach (var it in this.FindChildrenByType<MeshInstance3D>())
        {
            it.Layers = 2;
        }

        UpdatePages();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event.IsActionPressed("advance_manual"))
        {
            AdvancePages();
        }
    }

    void UpdatePages()
    {
        this.FindChildByName<Node>("Page2").FindChildByType<AnimationPlayer>().CurrentAnimation = "RightSide";
        this.FindChildByName<Node>("Page3").FindChildByType<AnimationPlayer>().CurrentAnimation = "RightSide";
        this.FindChildByName<Node3D>("Page3").Visible = false;

        this.FindChildByName<Node>("Page1").FindChildByType<MeshInstance3D>().MaterialOverride = CreatePageMat($"res://textures/manpages/page{currentPageNumber}.png", $"res://textures/manpages/page{currentPageNumber - 1}.png", true, false);
        this.FindChildByName<Node>("Page2").FindChildByType<MeshInstance3D>().MaterialOverride = CreatePageMat($"res://textures/manpages/page{currentPageNumber + 2}.png", $"res://textures/manpages/page{currentPageNumber + 1}.png", true, false);
        this.FindChildByName<Node>("Page3").FindChildByType<MeshInstance3D>().MaterialOverride = CreatePageMat($"res://textures/manpages/page{currentPageNumber + 4}.png", $"res://textures/manpages/page{currentPageNumber + 3}.png", false, false);
    }

    void AdvancePages()
    {
        this.FindChildByName<Node>("Page2").FindChildByType<AnimationPlayer>().CurrentAnimation = "RightToLeft";
        TrackedAnimationPlayer = this.FindChildByName<Node>("Page2").FindChildByType<AnimationPlayer>();
        CurrentMovement = 1;

        this.FindChildByName<Node3D>("Page3").Visible = true;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (TrackedAnimationPlayer != null)
        {
            GD.Print(TrackedAnimationPlayer.CurrentAnimationPosition);
            if (TrackedAnimationPlayer.CurrentAnimationPosition >= 2.4f)
            {
                currentPageNumber += 2;
                UpdatePages();

                CurrentMovement = 0;
            }
        }
    }

    ShaderMaterial CreatePageMat(string frontURL, string backURL, bool flipFrontHorizontally, bool flipBackHorizontally)
    {
        var ret = (ShaderMaterial)GD.Load<ShaderMaterial>("res://materials/PageMat.tres").Duplicate();

        ret.SetShaderParameter("texture_front", GD.Load<Texture2D>(frontURL));
        ret.SetShaderParameter("texture_back", GD.Load<Texture2D>(backURL));
        ret.SetShaderParameter("flip_front_horizontally", flipFrontHorizontally);
        ret.SetShaderParameter("flip_back_horizontally", flipBackHorizontally);

        return ret;
    }
}
