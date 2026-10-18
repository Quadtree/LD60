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

    bool PlayedAudio = false;

    public override void _Ready()
    {
        base._Ready();

        this.FindChildByType<Area3D>().BodyEntered += HitSomething;


    }

    void HitSomething(Node3D thing)
    {
        GD.Print($"We've hit {thing}");

        if (this.FindParentByType<Default>().IsInStationMode) return;

        if (CanHitEnemy && thing is PirateFighter pf)
        {
            pf.TakeDamage(1);
            QueueFree();
        }
        if (CanHitEnemy && thing is PirateStation ps)
        {
            ps.TakeDamage(1);
            QueueFree();
        }
        if (CanHitPlayer && thing is ShipPC sp)
        {
            sp.TakeDamage(1);
            QueueFree();
        }

        Util.SpawnOneShotSound("res://sounds/hit_space.wav", this, GlobalPosition);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (!PlayedAudio)
        {
            Util.SpawnOneShotSound("res://sounds/fire_space_bolt.wav", this, GlobalPosition);
            PlayedAudio = true;
        }

        // foreach (var it in GetTree().CurrentScene.FindChildrenByPredicate<Node3D>(it => (CanHitPlayer && it is ShipPC) || (CanHitEnemy && it is PirateFighter)))
        // {
        //     if (it.GlobalPosition.DistanceTo(GlobalPosition) < 2)
        //     {
        //         QueueFree();
        //         if (it is PirateFighter pf) pf.TakeDamage(1);
        //         return;
        //     }
        // }

        GlobalPosition += -GlobalTransform.Basis.Column2 * Speed * (float)delta;

        TimeToLive -= (float)delta;

        if (TimeToLive <= 0)
        {
            QueueFree();
        }
    }
}
