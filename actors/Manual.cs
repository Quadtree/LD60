using System;
using Godot;

public partial class Manual : Node3D
{
    public override void _Ready()
    {
        base._Ready();

        foreach (var it in this.FindChildrenByType<MeshInstance3D>())
        {
            it.Layers = 2;
        }

        var page3 = this.FindChildByName<Node>("Page3");
        page3.FindChildByType<AnimationPlayer>().CurrentAnimation = "RightSide";

        this.FindChildByName<Node>("Page3").FindChildByType<MeshInstance3D>().MaterialOverride = GD.Load<ShaderMaterial>("res://materials/PageMat.tres").Duplicate();

        this.FindChildByName<Node>("Page3").FindChildByType<MeshInstance3D>().SetInstanceShaderParameter()
    }

    ShaderMaterial CreatePageMat(string frontURL, string backURL)
    {
        var ret = (ShaderMaterial)GD.Load<ShaderMaterial>("res://materials/PageMat.tres").Duplicate();

        ret.SetShaderParameter()

        return ret;
    }
}
