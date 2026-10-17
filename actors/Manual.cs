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

        this.FindChildByName<Node>("Page3").FindChildByType<MeshInstance3D>().MaterialOverride = GD.Load<ShaderMaterial>("res://materials/PageMat.tres");

        this.FindChildByName<Node>("Page3").FindChildByType<MeshInstance3D>().SetInstanceShaderParameter("texture_front", GD.Load<Texture2D>("res://textures/manpages/page1.png"));
    }
}
