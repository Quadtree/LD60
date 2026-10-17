using System;
using Godot;

public partial class ShipLocationLabel : Label
{
    public override void _Process(double delta)
    {
        base._Process(delta);

        if (this.FindParentByType<SubViewport>().FindChildByType<ShipPC>() is ShipPC pc)
        {
            Text = $"{Mathf.RoundToInt((pc.Position.X + 100) / 10):000}{Mathf.RoundToInt((-pc.Position.Z + 100) / 10):000}";
        }

        Visible = GetViewport().GetCamera3D().GetParent() is ShipPC;
    }
}
