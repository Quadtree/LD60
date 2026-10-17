using System;
using Godot;

public partial class RobotFightUi : Control
{
    int YourHealth;
    int RobotHealth;

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);


    }

    void UpdateBoxes()
    {
        this.FindChildByName<Label>("OutcomeLabel").Text = "";
        this.FindChildByName<Label>("HealthLabel").Text = $"You={YourHealth} Robot={RobotHealth}";
    }
}
