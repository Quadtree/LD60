using System;
using Godot;

public partial class SkySphere : MeshInstance3D
{
    public override void _Ready()
    {
        base._Ready();

        Visible = true;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        GlobalPosition = GetViewport().GetCamera3D().GlobalPosition;
    }
}
