using Godot;
using System;

public partial class EnergyBolt : Node3D
{
    [Export]
    bool CanHitPlayer;

    [Export]
    bool CanHitEnemy;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        
    }
}
