using System;
using Godot;

public partial class RobotFightUi : Control
{
    int YourHealth = 6;
    int RobotHealth = 9;
    bool Dishonor = false;

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

    Moves[] OPPO_PATTERN = new Moves[]
    {
        Moves.Jab,          // 2
        Moves.Uppercut,     // 3
        Moves.Feint,        // 1
    };

    int oppoPatPos;

    enum Outcome
    {
        You_win,
        Robot_wins,
        Nobody_wins
    }

    Outcome TheOutcome;

    public override void _Ready()
    {
        base._Ready();

        oppoPatPos = Util.RandInt(0, OPPO_PATTERN.Length);

        UpdateBoxes();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event.IsActionPressed("fight_1")) MakeFightMove(Moves.Uppercut);
        if (@event.IsActionPressed("fight_2")) MakeFightMove(Moves.Feint);
        if (@event.IsActionPressed("fight_3")) MakeFightMove(Moves.Jab);
        if (@event.IsActionPressed("fight_4")) MakeFightMove(Moves.Laser);

        if (OS.IsDebugBuild() && @event.IsActionPressed("cheat_win_fight"))
        {

        }
    }

    void MakeFightMove(Moves move)
    {
        OpponentMove = OPPO_PATTERN[oppoPatPos];
        oppoPatPos = (oppoPatPos + 1) % OPPO_PATTERN.Length;

        YourMove = move;

        if (YourMove == Moves.Laser)
        {
            Dishonor = true;
            RobotHealth -= 6;
        }
        else if (YourMove == Moves.Uppercut && OpponentMove == Moves.Jab)
        {
            TheOutcome = Outcome.You_win;
        }
        else if (YourMove == Moves.Jab && OpponentMove == Moves.Feint)
        {
            TheOutcome = Outcome.You_win;
        }
        else if (YourMove == Moves.Feint && OpponentMove == Moves.Uppercut)
        {
            TheOutcome = Outcome.You_win;
        }
        else if (OpponentMove == Moves.Uppercut && YourMove == Moves.Jab)
        {
            TheOutcome = Outcome.Robot_wins;
        }
        else if (OpponentMove == Moves.Jab && YourMove == Moves.Feint)
        {
            TheOutcome = Outcome.Robot_wins;
        }
        else if (OpponentMove == Moves.Feint && YourMove == Moves.Uppercut)
        {
            TheOutcome = Outcome.Robot_wins;
        }
        else
        {
            TheOutcome = Outcome.Nobody_wins;
        }

        if (TheOutcome == Outcome.You_win) RobotHealth--;
        else if (TheOutcome == Outcome.Robot_wins) YourHealth--;

        UpdateBoxes();
    }

    void PlayerWinsFight()
    {

    }

    void UpdateBoxes()
    {
        if (YourMove == Moves.Invalid)
        {
            this.FindChildByName<Label>("OutcomeLabel").Text = "";
        }
        else
        {
            this.FindChildByName<Label>("OutcomeLabel").Text = $"{YourMove} vs {OpponentMove}\n{TheOutcome.ToString().Replace("_", " ")}";
        }
        this.FindChildByName<Label>("HealthLabel").Text = $"You={YourHealth} Robot={RobotHealth}";

        this.FindChildByName<Label>("KeyLabel").Text = $"U={UIUtil.GetActionKey("fight_1")} F={UIUtil.GetActionKey("fight_2")} J={UIUtil.GetActionKey("fight_3")} L={UIUtil.GetActionKey("fight_4")}";
    }
}
