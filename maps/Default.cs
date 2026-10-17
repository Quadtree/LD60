using System;
using Godot;

public partial class Default : Control
{
    public override void _Ready()
    {
        base._Ready();

        ShipMode();


    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (OS.IsDebugBuild() && @event.IsActionPressed("cheat_go_station_2")) Station2CheatMode();
        if (OS.IsDebugBuild() && @event.IsActionPressed("cheat_go_final")) CheatToPirateStation();
    }

    public void ShipMode()
    {
        this.FindChildByName<Node3D>("InStationPC").FindChildByType<Camera3D>().Current = false;
        this.FindChildByName<Node3D>("ShipPC").FindChildByType<Camera3D>().Current = true;
    }

    public void StationMode()
    {
        this.FindChildByName<Node3D>("InStationPC").FindChildByType<Camera3D>().Current = true;
        this.FindChildByName<Node3D>("ShipPC").FindChildByType<Camera3D>().Current = false;
    }

    public void Station1Mode()
    {

    }

    public void Station2Mode()
    {
        StationMode();

        var pc = this.FindChildByType<InStationPC>();

        var entryExit = this.FindChildByName<Node3D>("Station2EntryExitPoint");

        pc.GlobalPosition = entryExit.GlobalPosition;
    }

    public void Station2CheatMode()
    {
        StationMode();

        var pc = this.FindChildByType<InStationPC>();

        var entryExit = this.FindChildByName<Node3D>("Station2EntryExitPoint2");

        pc.GlobalPosition = entryExit.GlobalPosition;
    }

    public void CheatToPirateStation()
    {
        ShipMode();

        var pc = this.FindChildByType<ShipPC>();

        var entryExit = this.FindChildByName<Node3D>("CheatPirateStationLoc");

        pc.GlobalPosition = entryExit.GlobalPosition;
    }
}
