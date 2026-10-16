using System;
using Godot;

public partial class PixelatedScreenSubViewportContainer : SubViewportContainer
{
    public override void _Process(double delta)
    {
        base._Process(delta);

        StretchShrink = (int)(Size.Y / 160);
    }
}
