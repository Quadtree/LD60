using System;
using Godot;

public partial class EnergyBolt : Node3D
{
    [Export]
    bool CanHitPlayer;

    [Export]
    bool CanHitEnemy;

    [Export]
    float Speed;

    [Export]
    float TimeToLive = 3;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        foreach (var it in GetTree().CurrentScene.FindChildrenByPredicate<Node3D>(it => (CanHitPlayer && it is ShipPC) || (CanHitEnemy && it is PirateFighter)))
        {
            if (it.GlobalPosition.DistanceTo(GlobalPosition) < 1)
            {
                QueueFree();
                return;
            }
        }

        GlobalPosition += -GlobalTransform.Basis.Column2 * Speed * (float)delta;

        TimeToLive -= (float)delta;

        if (TimeToLive <= 0)
        {
            QueueFree();
        }
    }
}
