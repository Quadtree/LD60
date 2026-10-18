using System;
using Godot;

public partial class ShipHealthBar : TextureRect
{
    float PlayerMaxHP;

    public override void _Process(double delta)
    {
        base._Process(delta);

        Visible = !this.FindParentByType<Default>().IsInStationMode;

        if (PlayerMaxHP == 0) PlayerMaxHP = GetTree().CurrentScene.FindChildByType<ShipPC>().Health;

        Size = new Vector2(Mathf.Max(GetTree().CurrentScene.FindChildByType<ShipPC>().Health / PlayerMaxHP * 85, 0), Size.Y);
    }
}
