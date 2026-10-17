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

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        foreach (var it in GetTree().CurrentScene.FindChildrenByPredicate<Node3D>(it => (CanHitPlayer && it is ShipPC) || (CanHitEnemy && it is PirateFighter)))
        {
            if (it.GlobalPosition.DistanceTo(GlobalPosition) < 2)
            {
                QueueFree();
                return;
            }
        }

        GlobalPosition += Vector3.Forward * Speed;
    }
}
