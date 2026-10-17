using System;
using Godot;

public partial class RobotFightStart : Area3D
{
    public override void _Ready()
    {
        base._Ready();

        GD.Print("Robot area ready");

        //AreaEntered += AreaEnteredHandler;
        BodyEntered += AreaEnteredHandler;
    }

    void AreaEnteredHandler(Node3D other)
    {
        GD.Print($"{other} has entered the area");

        if (other is InStationPC pc)
        {
            GD.Print("FIGHT START!");
            pc.Frozen = true;

            this.FindParentByType<Viewport>().FindChildByType<RobotFightUi>().Visible = true;
        }
    }
}
