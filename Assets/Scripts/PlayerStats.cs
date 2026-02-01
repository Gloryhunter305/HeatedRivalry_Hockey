using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Movement")]
    public float MoveSpeed = 5f;
    public float TurnMultipler = 3f;

    [Header("Dash Settings")]
    public float DashForce = 10f;
    public float DashDuration = 0.5f;
    public float DashCooldown = 2f;
    public bool isDashing = false;
    public bool canDash = true;


    [Header("Physics Settings")]
    public float Mass = 1f;
    public float KnockbackForce = 5f;
    public float DashKnockbackMultiplier = 2f;
    public float RecoilMultiplier = 0.5f;
    public bool applyPlayerKnockback = true;

}
