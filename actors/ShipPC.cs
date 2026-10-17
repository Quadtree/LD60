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

        if (Input.IsActionPressed("left")) Rotate(GlobalTransform.Basis.Column1, (float)(SpinRate * delta));
        if (Input.IsActionPressed("right")) Rotate(GlobalTransform.Basis.Column1, -(float)(SpinRate * delta));
        if (Input.IsActionPressed("forward")) Rotate(GlobalTransform.Basis.Column0, -(float)(SpinRate * delta));
        if (Input.IsActionPressed("backward")) Rotate(GlobalTransform.Basis.Column0, (float)(SpinRate * delta));
        if (Input.IsActionPressed("spin_left")) Rotate(GlobalTransform.Basis.Column2, (float)(SpinRate * delta));
        if (Input.IsActionPressed("spin_right")) Rotate(GlobalTransform.Basis.Column2, -(float)(SpinRate * delta));

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
