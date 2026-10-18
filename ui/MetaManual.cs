using System;
using Godot;

public partial class MetaManual : Label
{
    public override void _Ready()
    {
        base._Ready();

        Text = $"{UIUtil.GetActionKey("retreat_manual")} {UIUtil.GetActionKey("advance_manual")} to turn pages, {UIUtil.GetActionKey("toggle_manual_visibility")} to put it down";
    }
}
