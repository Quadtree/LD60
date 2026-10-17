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

    float FireCharge;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (!this.FindChildByType<Camera3D>().Current) return;

        if (Input.IsActionPressed("fire") && FireCharge >= 0.35f)
        {
            foreach (var sp in new float[] { -1.5f, 1.5f })
            {
                var bolt = GD.Load<PackedScene>("res://actors/EnergyBoltPlayer.tscn").Instantiate<Node3D>();
                this.FindParentByType<SubViewport>().AddChild(bolt);
                bolt.GlobalPosition = GlobalPosition + GlobalTransform.Basis.Column0 * sp;
                bolt.GlobalRotation = GlobalRotation;
            }

            FireCharge = 0;
        }

        FireCharge += (float)delta;

        if (Input.IsActionPressed("spin_left")) Rotate(GlobalTransform.Basis.Column1, (float)(SpinRate * delta));
        if (Input.IsActionPressed("spin_right")) Rotate(GlobalTransform.Basis.Column1, -(float)(SpinRate * delta));
        //if (Input.IsActionPressed("forward")) Rotate(GlobalTransform.Basis.Column0, -(float)(SpinRate * delta));
        //if (Input.IsActionPressed("backward")) Rotate(GlobalTransform.Basis.Column0, (float)(SpinRate * delta));
        //if (Input.IsActionPressed("spin_left")) Rotate(GlobalTransform.Basis.Column2, (float)(SpinRate * delta));
        //if (Input.IsActionPressed("spin_right")) Rotate(GlobalTransform.Basis.Column2, -(float)(SpinRate * delta));

        Vector3 desiredVelocity = Vector3.Zero;

        if (Input.IsActionPressed("left")) desiredVelocity -= GlobalTransform.Basis.Column0 * MoveSpeed;
        if (Input.IsActionPressed("right")) desiredVelocity += GlobalTransform.Basis.Column0 * MoveSpeed;

        if (Input.IsActionPressed("forward")) desiredVelocity -= GlobalTransform.Basis.Column2 * MoveSpeed;
        if (Input.IsActionPressed("backward")) desiredVelocity += GlobalTransform.Basis.Column2 * MoveSpeed;

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
