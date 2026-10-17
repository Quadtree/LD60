using System;
using Godot;

public partial class PirateFighter : AnimatableBody3D
{
    [Export]
    float Health;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (this.FindParentByType<SubViewport>().FindChildByType<ShipPC>() is ShipPC pc)
        {
            var targetVector = (pc.GlobalPosition - GlobalPosition).Normalized();

            var currentVector = -GlobalTransform.Basis.Column2;

            var nv = currentVector.Slerp(targetVector, (float)(0.7 * delta));

            GD.Print($"targetVector={targetVector} nv={nv}");

            //LookAt(GlobalPosition + nv);

            GlobalTransform = GlobalTransform.LookingAt(GlobalPosition + nv);
        }
        else
        {
            GD.Print("No target!");
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            QueueFree();
        }
    }
}
