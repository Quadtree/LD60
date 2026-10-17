using System;
using Godot;

public partial class InStationPC : CharacterBody3D
{
    [Export]
    float SpinRate = 1f;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (Input.IsActionPressed("left")) RotateY((float)(SpinRate * delta));
        if (Input.IsActionPressed("right")) RotateY(-(float)(SpinRate * delta));
    }
}
