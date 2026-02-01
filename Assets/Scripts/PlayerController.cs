using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager game;
    int timer;

    [Header("Player Components")]
    private PlayerStats Player;
    private Rigidbody2D _rigidBody2D;
    public funnyFace funnyFace;

    public KeyCode upKey, downKey, leftKey, rightKey;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        Player = GetComponent<PlayerStats>();

        if (_rigidBody2D == null)
            Debug.LogWarning("Rigidbody2D component missing on player.", this);
        if (Player == null)
            Debug.LogWarning("PlayerStats component missing on player.", this);
        if (game == null)
            Debug.LogWarning("GameManager reference not set on PlayerController.", this);
    }

    void Update()
    {
        // Keep Update empty (physics in FixedUpdate), but still useful to validate start state if needed.
    }

    private void FixedUpdate()
    {
        if (game == null || Player == null)
            return;

        if (!game.gameStart)
            return;

        // Forward movement when pressing upKey
        if (Input.GetKey(upKey))
        {
            _rigidBody2D.AddForce((Vector2)transform.up * Player.MoveSpeed);
        }

        // Charge / dash when holding downKey
        if (Input.GetKey(downKey))
        {
            if (Player.canDash && !Player.isDashing)
            {
                StartCoroutine(Dash());
            }
        }

        // Turn left / right independently of holding downKey
        if (Input.GetKey(leftKey))
        {
            transform.Rotate(Vector3.forward * Player.TurnMultipler * Time.fixedDeltaTime);
        }

        if (Input.GetKey(rightKey))
        {
            transform.Rotate(Vector3.back * Player.TurnMultipler * Time.fixedDeltaTime);
        }
        
        timer++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Puck"))
        {
            ApplyKnockback(collision);
        }

        if (timer > 300 && funnyFace != null)
        {
            StartCoroutine(funnyFace.Hurt());
            timer = 0;
        }
    }

    private void ApplyKnockback(Collision2D collision)
    {
        Rigidbody2D puckRigidBody2D = collision.rigidbody;
        if (puckRigidBody2D == null || _rigidBody2D == null || Player == null)
            return;

        Vector2 direction = (puckRigidBody2D.position - _rigidBody2D.position).normalized;

        float forceMagnitude = Player.KnockbackForce;
        if (Player.isDashing)
            forceMagnitude *= Player.DashKnockbackMultiplier;

        // Use velocity (Rigidbody2D.velocity) -- not linearVelocity
        Vector2 playerVelocityContribution = _rigidBody2D.linearVelocity * 0.5f;

        Vector2 finalForce = direction * forceMagnitude + playerVelocityContribution;

        puckRigidBody2D.AddForce(finalForce, ForceMode2D.Impulse);

        if (Player.applyPlayerKnockback)
        {
            Vector2 recoil = -direction * (forceMagnitude * Player.RecoilMultiplier);
            _rigidBody2D.AddForce(recoil, ForceMode2D.Impulse);
        }
    }

    private IEnumerator Dash()
    {
        Player.canDash = false;
        Player.isDashing = true;

        // Stop lateral momentum before dash
        _rigidBody2D.linearVelocity = Vector2.zero;
        _rigidBody2D.AddForce((Vector2)transform.up * Player.DashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(Player.DashDuration);
        Player.isDashing = false;

        yield return new WaitForSeconds(Player.DashCooldown);
        Player.canDash = true;
    }
}
