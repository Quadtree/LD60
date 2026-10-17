using System;
using Godot;

public partial class PirateStation : StaticBody3D
{
    [Export]
    float Health = 25;

    public void TakeDamage(float amt)
    {
        Health -= amt;
        if (Health <= 0)
        {
            QueueFree();
            // @TODO: player wins game
        }
    }
}
