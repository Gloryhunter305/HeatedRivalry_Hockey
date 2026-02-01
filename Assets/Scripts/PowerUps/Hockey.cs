using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/BigAndTanky")]
public class Hockey : PowerUp
{
    //Hockey: Increase size and mass, reduce knockback effects
    public override void Apply(PlayerStats stats)
    {
        stats.PlayerSize *= 1.5f;
        stats.Mass *= 2f;
        stats.KnockbackForce *= 0.5f;
        stats.DashKnockbackMultiplier *= 0.5f;
    }
    public override void Remove(PlayerStats stats)
    {
        stats.PlayerSize /= 1.5f;
        stats.Mass /= 2f;
        stats.KnockbackForce /= 0.5f;
        stats.DashKnockbackMultiplier /= 0.5f;
    }
}
