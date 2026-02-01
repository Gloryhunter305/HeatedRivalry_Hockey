using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/KnockbackImmunity")]
public class KnockbackImmunity : PowerUp
{
    //Luchador: Grants immunity to knockback
    public override void Apply(PlayerStats stats)
    {
        stats.applyPlayerKnockback = false;
    }
    public override void Remove(PlayerStats stats)
    {
        stats.applyPlayerKnockback = true;
    }
}