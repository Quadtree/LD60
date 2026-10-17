using System;
using Godot;

public partial class ShipPC : CharacterBody3D
{
    [Export]
    float SpinRate = 1f;

    [Export]
    float MoveSpeed;

    [Export]
    float Acceleration = 1;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (!this.FindChildByType<Camera3D>().Current) return;

        if (Input.IsActionPressed("left")) RotateY((float)(SpinRate * delta));
        if (Input.IsActionPressed("right")) RotateY(-(float)(SpinRate * delta));
        if (Input.IsActionPressed("forward")) RotateX(-(float)(SpinRate * delta));
        if (Input.IsActionPressed("backward")) RotateX((float)(SpinRate * delta));
        if (Input.IsActionPressed("spin_left")) RotateZ((float)(SpinRate * delta));
        if (Input.IsActionPressed("spin_right")) RotateZ(-(float)(SpinRate * delta));

        Vector3 desiredVelocity = Vector3.Zero;

        if (Input.IsActionPressed("accelerate")) desiredVelocity -= GlobalTransform.Basis.Column2 * MoveSpeed;
        else if (Input.IsActionPressed("decelerate")) desiredVelocity += GlobalTransform.Basis.Column2 * MoveSpeed;

        var accel = Acceleration * (float)delta;

        if (Velocity.DistanceTo(desiredVelocity) < accel)
        {
            Velocity = desiredVelocity;
        }
        else
        {
            Velocity += (desiredVelocity - Velocity).Normalized() * accel;
        }

        MoveAndSlide();
    }
}
