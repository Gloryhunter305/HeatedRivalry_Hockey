using UnityEngine;

[CreateAssetMenu (menuName = "PowerUps/HardBigFastStronger")]
public class Oni : PowerUp
{
    //Oni: Makes you stronger and faster, also dash cooldown is faster
    public override void Apply (PlayerStats stats)
    {
        stats.MoveSpeed *= 1.5f;
        stats.KnockbackForce *= 1.5f;
        stats.DashForce *= 1.5f;
        stats.DashCooldown *= 0.5f;
    }

    public override void Remove (PlayerStats stats)
    {
        stats.MoveSpeed /= 1.5f;
        stats.KnockbackForce /= 1.5f;
        stats.DashForce /= 1.5f;
        stats.DashCooldown /= 0.5f;
    }
}
