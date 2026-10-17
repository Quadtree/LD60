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

    public override void _Ready()
    {
        base._Ready();

        UpdateBoxes();
    }

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

        this.FindChildByName<Label>("KeyLabel").Text = $"U={InputMap.ActionGetEvents("fight_1")[0].AsText()} F={InputMap.ActionGetEvents("fight_2")[0].AsText()} J={InputMap.ActionGetEvents("fight_3")[0].AsText()} L={InputMap.ActionGetEvents("fight_4")[0].AsText()}";
    }
}
