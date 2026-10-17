using System;
using Godot;

public partial class RobotFightUi : Control
{
    int YourHealth;
    int RobotHealth;

    enum Moves
    {
        Invalid,
        Uppercut,
        Feint,
        Jab,
        Laser
    }

    Moves YourMove = Moves.Invalid;
    Moves OpponentMove = Moves.Invalid;

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);


    }

    void UpdateBoxes()
    {
        if (YourMove == Moves.Invalid)
        {
            this.FindChildByName<Label>("OutcomeLabel").Text = "";
        }
        this.FindChildByName<Label>("HealthLabel").Text = $"You={YourHealth} Robot={RobotHealth}";
    }
}
