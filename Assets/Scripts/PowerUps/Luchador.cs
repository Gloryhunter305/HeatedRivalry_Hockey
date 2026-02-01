using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/KnockbackImmunity")]
public class KnockbackImmunity : PowerUp
{
    //Luchador: Grants immunity to knockback
    public override void Apply(PlayerStats stats)
    {
        stats.applyPlayerKnockback = false;
        stats.KnockbackForce /= 2.5f;
        Debug.Log("Knockback immunity applied.");
    }
    public override void Remove(PlayerStats stats)
    {
        stats.applyPlayerKnockback = true;
        stats.KnockbackForce *= 2.5f;
        Debug.Log("Knockback immunity has worn off.");
    }
}