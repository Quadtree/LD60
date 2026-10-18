using System;
using Godot;

public partial class Default : Control
{
    ulong LastShipMode;

    [Export]
    bool ThirtyFPSMode = true;

    public override void _Ready()
    {
        base._Ready();

        StationMode();

        this.FindChildByName<Area3D>("ExitStation2").BodyEntered += (body) =>
        {
            if (body is InStationPC pc)
            {
                if (!ShipMode()) return;

                var shp = this.FindChildByType<ShipPC>();
                shp.GlobalPosition = this.FindChildByName<Node3D>("Station2").FindChildByName<Node3D>("ExitPoint").GlobalPosition;
            }
        };

        this.FindChildByName<Area3D>("ExitStation1").BodyEntered += (body) =>
        {
            if (body is InStationPC pc)
            {
                if (!ShipMode()) return;

                var shp = this.FindChildByType<ShipPC>();
                shp.GlobalPosition = this.FindChildByName<Node3D>("Station").FindChildByName<Node3D>("ExitPoint").GlobalPosition;
            }
        };

        foreach (var stationEntry in new (StaticBody3D, object)[] {
            (this.FindChildByName<StaticBody3D>("Station"), null),
            (this.FindChildByName<StaticBody3D>("Station2"), null),
        })
        {

        }

        if (ThirtyFPSMode)
        {
            this.FindChildByName<SubViewport>("InGameViewport").RenderTargetUpdateMode = SubViewport.UpdateMode.Once;
        }
    }

    ulong LastFrameRenderTime;

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (ThirtyFPSMode)
        {
            if (Time.GetTicksMsec() - LastFrameRenderTime > 33)
            {
                this.FindChildByName<SubViewport>("InGameViewport").RenderTargetUpdateMode = SubViewport.UpdateMode.Once;
                LastFrameRenderTime = Time.GetTicksMsec();
            }
        }
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (OS.IsDebugBuild() && @event.IsActionPressed("cheat_go_station_1")) Station1Mode();
        if (OS.IsDebugBuild() && @event.IsActionPressed("cheat_go_station_2")) Station2CheatMode();
        if (OS.IsDebugBuild() && @event.IsActionPressed("cheat_go_final")) CheatToPirateStation();

        if (@event.IsActionPressed("toggle_manual_visibility"))
        {
            var man = this.FindChildByType<ManualViewport>();

            man.Visible = !man.Visible;
            GD.Print($"Changed manual visibility to {man.Visible}");

            this.FindChildByType<Manual>().PlayRandomPaperSound();
        }
    }

    public bool ShipMode()
    {
        if ((Time.GetTicksMsec() - LastShipMode) < 500)
        {
            GD.Print("Too soon!");
            return false;
        }

        GD.Print("Ship mode!");
        this.FindChildByName<Node3D>("InStationPC").FindChildByType<Camera3D>().Current = false;
        this.FindChildByName<Node3D>("ShipPC").FindChildByType<Camera3D>().Current = true;

        this.FindChildByName<Label>("EncryptedCoordinates").Visible = false;

        LastShipMode = Time.GetTicksMsec();

        return true;
    }

    public bool StationMode()
    {
        if ((Time.GetTicksMsec() - LastShipMode) < 500)
        {
            GD.Print("Too soon!");
            return false;
        }

        LastShipMode = Time.GetTicksMsec();

        GD.Print("Station mode!");
        this.FindChildByName<Node3D>("InStationPC").FindChildByType<Camera3D>().Current = true;
        this.FindChildByName<Node3D>("ShipPC").FindChildByType<Camera3D>().Current = false;

        return true;
    }

    public void Station1Mode()
    {
        if (!StationMode()) return;

        var pc = this.FindChildByType<InStationPC>();

        var entryExit = this.FindChildByName<Node3D>("Station1EntryExitPoint");

        pc.GlobalTransform = entryExit.GlobalTransform;
    }

    public void Station2Mode()
    {
        if (!StationMode()) return;

        var pc = this.FindChildByType<InStationPC>();

        var entryExit = this.FindChildByName<Node3D>("Station2EntryExitPoint");

        pc.GlobalTransform = entryExit.GlobalTransform;
    }

    public void Station2CheatMode()
    {
        if (!StationMode()) return;

        var pc = this.FindChildByType<InStationPC>();

        var entryExit = this.FindChildByName<Node3D>("Station2EntryExitPoint2");

        pc.GlobalPosition = entryExit.GlobalPosition;
    }

    public void CheatToPirateStation()
    {
        if (!ShipMode()) return;

        var pc = this.FindChildByType<ShipPC>();

        var entryExit = this.FindChildByName<Node3D>("CheatPirateStationLoc");

        pc.GlobalPosition = entryExit.GlobalPosition;
    }

    public bool IsInStationMode => this.FindChildByType<InStationPC>().FindChildByType<Camera3D>().Current;
}
