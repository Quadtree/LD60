using System;
using Godot;

public partial class Asteroid : AnimatableBody3D
{
    Vector3 RotationAxis;

    public override void _Ready()
    {
        base._Ready();

        RotationAxis = new Vector3(Util.RandF(-1, 1), Util.RandF(-1, 1), Util.RandF(-1, 1)).Normalized();

        //GlobalRotation = new Vector3(Util.RandF(-1000, 1000), Util.RandF(-1000, 1000), Util.RandF(-1000, 1000));

        GlobalPosition += new Vector3(0, Util.RandF(-30, 30), 0);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Rotate(RotationAxis, (float)delta * 0.1f);
    }
}
