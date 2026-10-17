using System;
using Godot;

public partial class InStationPC : CharacterBody3D
{
    [Export]
    float SpinRate = 1f;

    [Export]
    float MoveSpeed;

    public bool Frozen = false;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (!this.FindChildByType<Camera3D>().Current || Frozen) return;

        if (Input.IsActionPressed("left")) RotateY((float)(SpinRate * delta));
        if (Input.IsActionPressed("right")) RotateY(-(float)(SpinRate * delta));

        if (Input.IsActionPressed("forward")) Velocity = -GlobalTransform.Basis.Column2 * MoveSpeed;
        else if (Input.IsActionPressed("backward")) Velocity = GlobalTransform.Basis.Column2 * MoveSpeed;
        else Velocity = Vector3.Zero;

        MoveAndSlide();
    }
}
