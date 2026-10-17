using System;
using Godot;

public partial class ShipLocationLabel : Label
{
    public override void _Process(double delta)
    {
        base._Process(delta);

        if (this.FindParentByType<SubViewport>().FindChildByType<ShipPC>() is ShipPC pc)
        {
            Text = LocToSector(pc.Position);
        }

        Visible = GetViewport().GetCamera3D().GetParent() is ShipPC;
    }

    public static string LocToSector(Vector3 loc)
    {
        return $"{Mathf.RoundToInt((loc.X + 100) / 10):000}{Mathf.RoundToInt((-loc.Z + 100) / 10):000}";
    }
}
