using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameManager game;
    int timer;


    [Header("Player Components")]
    PlayerStats Player;
    private Rigidbody2D _rigidBody2D;
    public funnyFace funnyFace;
    //[SerializeField] private float DirectionalMultipler = 3f;
    public KeyCode upKey, downKey, leftKey, rightKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        Player = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        if (game.gameStart == true)
        {
            // Move in the direction the player is facing (transform.up)
            _rigidBody2D.AddForce((Vector2)transform.up * Player.MoveSpeed);
        }

        if (Input.GetKey(downKey))        //Charging up and dashing forwards
        {
            //_rigidBody2D.AddForce((Vector2)(-transform.up) * moveSpeed);

            //Dashing Mechanic (Charing dash is linear to the amount of speed gained from dash)
            if (Player.canDash && !Player.isDashing)
            timer++;
            if (Input.GetKey(upKey))
            {
                // Move in the direction the player is facing (transform.up)
                _rigidBody2D.AddForce((Vector2)transform.up * Player.MoveSpeed);
            }

            if (Input.GetKey(leftKey))
            {
                transform.Rotate(Vector3.forward * Player.TurnMultipler);
            }

            if (Input.GetKey(rightKey))
            {
                transform.Rotate(Vector3.back * Player.TurnMultipler);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Puck"))
        {
            ApplyKnockback(collision);
        }

        if (timer > 300)
        {
            funnyFace.StartCoroutine(funnyFace.Hurt());
            timer = 0;
        }
    }

    private void ApplyKnockback(Collision2D collision)
    {
        // Use the puck's Rigidbody2D
        Rigidbody2D puckRigidBody2D = collision.rigidbody;
        if (puckRigidBody2D == null)
            return;

        // Where the puck is going to be knocked towards
        Vector2 direction = (puckRigidBody2D.position - _rigidBody2D.position).normalized;

        // Base force, amplified if player is dashing
        float forceMagnitude = Player.KnockbackForce;
        if (Player.isDashing)
            forceMagnitude *= Player.DashKnockbackMultiplier;

        // Player's velocity transfer
        Vector2 playerVelocityContribution = _rigidBody2D.linearVelocity * 0.5f;

        Vector2 finalForce = direction * forceMagnitude + playerVelocityContribution;

        // Apply impulse to puck
        puckRigidBody2D.AddForce(finalForce, ForceMode2D.Impulse);

        //Apply recoil to the player
        if (Player.applyPlayerKnockback)
        {
            Vector2 recoil = -direction * (forceMagnitude * Player.RecoilMultiplier);
            _rigidBody2D.AddForce(recoil, ForceMode2D.Impulse);
            if (Input.GetKey(downKey))        //Charging up and dashing forwards
            {
                //_rigidBody2D.AddForce((Vector2)(-transform.up) * moveSpeed);

                //Dashing Mechanic (Charing dash is linear to the amount of speed gained from dash)
                if (Player.canDash && Player.isDashing)
                {
                    StartCoroutine(Dash());
                }
            }

            if (Input.GetKey(leftKey))
            {
                transform.Rotate(Vector3.forward * Player.TurnMultipler);
            }

            if (Input.GetKey(rightKey))
            {
                transform.Rotate(Vector3.back * Player.TurnMultipler);
            }
        }
    }

    private IEnumerator Dash()
    {
        Player.canDash = false;
        Player.isDashing = true;

        //Stop momentum before dash (prevent sideways momentum)
        _rigidBody2D.linearVelocity = Vector2.zero;
        _rigidBody2D.AddForce((Vector2)transform.up * Player.DashForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(Player.DashDuration);
        Player.isDashing = false;

        yield return new WaitForSeconds(Player.DashCooldown);
        Player.canDash = true;
    }
}
