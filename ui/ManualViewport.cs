using System;
using Godot;

public partial class ManualViewport : SubViewportContainer
{
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event.IsActionPressed("toggle_manual_visibility"))
        {
            Visible = !Visible;
        }
    }
}
