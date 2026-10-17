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

            var tree = GetTree();

            CrawlScreen.PutUpCrawlScreen(this, "You destroyed the\npirate station and\nrecovered the gems!", () => tree.ChangeSceneToFile("res://maps/Default.tscn"));
        }
    }
}
