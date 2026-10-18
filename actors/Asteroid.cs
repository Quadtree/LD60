using System;
using Godot;

public partial class Asteroid : AnimatableBody3D
{
    Vector3 RotationAxis;

    bool HasBeenPlaced = false;

    public override void _Ready()
    {
        base._Ready();

        RotationAxis = new Vector3(Util.RandF(-1, 1), Util.RandF(-1, 1), Util.RandF(-1, 1)).Normalized();


    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (!HasBeenPlaced)
        {
            SyncToPhysics = false;

            GlobalRotation = new Vector3(Util.RandF(-1000, 1000), Util.RandF(-1000, 1000), Util.RandF(-1000, 1000));

            GD.Print($"Asteroid starting at {GlobalPosition}");

            var np = GlobalPosition + new Vector3(0, Util.RandF(-10, 10), 0);

            GlobalPosition = np;

            GD.Print($"Asteroid placed at {GlobalPosition} by placing it at {np}");

            HasBeenPlaced = true;
        }
        else
        {
            SyncToPhysics = true;
            Rotate(RotationAxis, (float)delta * 0.1f);
        }


    }
}
