using Godot;
using System;

public partial class RobotFightUi : Control
{
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);


    }

    void UpdateBoxes()
    {
        this.FindChildByName<Label>()
    }
}
