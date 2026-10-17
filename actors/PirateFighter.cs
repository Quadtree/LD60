using System;
using Godot;

public partial class PirateFighter : AnimatableBody3D
{
    [Export]
    float Health;

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            QueueFree();
        }
    }
}
