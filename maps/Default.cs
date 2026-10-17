using System;
using Godot;

public partial class Default : Control
{
    public override void _Ready()
    {
        base._Ready();

        ShipMode();
    }

    void ShipMode()
    {
        this.FindChildByName<Node3D>("InStationPC").FindChildByType<Camera3D>().Current = false;
        this.FindChildByName<Node3D>("ShipPC").FindChildByType<Camera3D>().Current = true;
    }

    void StationMode()
    {
        this.FindChildByName<Node3D>("InStationPC").FindChildByType<Camera3D>().Current = true;
        this.FindChildByName<Node3D>("ShipPC").FindChildByType<Camera3D>().Current = false;
    }
}
