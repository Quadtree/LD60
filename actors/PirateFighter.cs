using System;
using Godot;

public partial class PirateFighter : AnimatableBody3D
{
    [Export]
    float Health;

    float FireCharge;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (this.FindParentByType<Default>().IsInStationMode) return;

        if (this.FindParentByType<SubViewport>().FindChildByType<ShipPC>() is ShipPC pc)
        {
            if ((pc.GlobalPosition - GlobalPosition).Length() < 25)
            {

                var targetVector = (pc.GlobalPosition - GlobalPosition).Normalized();

                var currentVector = (-GlobalTransform.Basis.Column2).Normalized();

                var rotation = currentVector.SignedAngleTo(targetVector, Vector3.Up);

                //GD.Print($"rotation={rotation} {rotation / Mathf.Abs(rotation)}");

                float rotRate = 4 * (float)delta;

                if (Mathf.Abs(rotation) < rotRate)
                {
                    LookAt(pc.GlobalPosition);
                }
                else
                {
                    RotateY(rotation / Mathf.Abs(rotation) * rotRate);
                }




                // var distToTargetVector = targetVector.DistanceTo(currentVector);

                // //GD.Print($"distToTargetVector={distToTargetVector}");

                // var nv = currentVector.Slerp(targetVector, (float)(20 * delta));

                // GD.Print($"targetVector={targetVector} nv={nv}");

                // //LookAt(GlobalPosition + nv);

                // GlobalTransform = GlobalTransform.LookingAt(GlobalPosition + nv);

                if (Mathf.Abs(rotation) < 0.05f)
                {
                    Position += -Transform.Basis.Column2 * (float)delta * 4;
                }

                if (FireCharge >= 0.35f && Mathf.Abs(rotation) < 0.2f)
                {
                    foreach (var sp in new float[] { 0 })
                    {
                        var bolt = GD.Load<PackedScene>("res://actors/EnergyBoltEnemy.tscn").Instantiate<Node3D>();
                        this.FindParentByType<SubViewport>().AddChild(bolt);
                        bolt.GlobalPosition = GlobalPosition + GlobalTransform.Basis.Column0 * sp;
                        bolt.GlobalRotation = GlobalRotation;
                    }

                    FireCharge = 0;
                }

                FireCharge += (float)delta;
            }
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
