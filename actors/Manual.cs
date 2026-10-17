using System;
using Godot;

public partial class Manual : Node3D
{
    int currentPageNumber = 1;

    public override void _Ready()
    {
        base._Ready();

        foreach (var it in this.FindChildrenByType<MeshInstance3D>())
        {
            it.Layers = 2;
        }

        this.FindChildByName<Node>("Page2").FindChildByType<AnimationPlayer>().CurrentAnimation = "RightSide";
        this.FindChildByName<Node>("Page3").FindChildByType<AnimationPlayer>().CurrentAnimation = "RightSide";
        this.FindChildByName<Node3D>("Page3").Visible = false;

        UpdatePages();
    }

    void UpdatePages()
    {
        this.FindChildByName<Node>("Page1").FindChildByType<MeshInstance3D>().MaterialOverride = CreatePageMat($"res://textures/manpages/page{currentPageNumber}.png", $"res://textures/manpages/page{currentPageNumber - 1}.png");
        this.FindChildByName<Node>("Page2").FindChildByType<MeshInstance3D>().MaterialOverride = CreatePageMat($"res://textures/manpages/page{currentPageNumber + 2}.png", $"res://textures/manpages/page{currentPageNumber + 1}.png");
        //this.FindChildByName<Node>("Page3").FindChildByType<MeshInstance3D>().MaterialOverride = CreatePageMat($"res://textures/manpages/page{currentPageNumber}.png", $"res://textures/manpages/page{currentPageNumber + 1}.png");
    }

    ShaderMaterial CreatePageMat(string frontURL, string backURL)
    {
        var ret = (ShaderMaterial)GD.Load<ShaderMaterial>("res://materials/PageMat.tres").Duplicate();

        ret.SetShaderParameter("texture_front", GD.Load<Texture2D>(frontURL));
        ret.SetShaderParameter("texture_back", GD.Load<Texture2D>(backURL));

        return ret;
    }
}
