using System;
using Godot;

public partial class CrawlScreen : Control
{
    ulong Cooldown;

    Action Next;

    public override void _Ready()
    {
        base._Ready();

        ProcessMode = ProcessModeEnum.Always;

        Cooldown = Time.GetTicksMsec() + 500;
    }

    public static void PutUpCrawlScreen(Node ctx, string text, Action next)
    {
        var ns = GD.Load<PackedScene>("res://ui/CrawlScreen.tscn").Instantiate<CrawlScreen>();

        var parent = ctx.GetTree().CurrentScene.FindChildByName<Node>("InGameViewport");
        parent.AddChild(ns);

        ns.FindChildByType<Label>().Text = text;
        ns.Next = next;
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event is InputEventKey && Time.GetTicksMsec() > Cooldown)
        {
            QueueFree();
            Next();
        }
    }
}
