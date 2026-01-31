using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    private Rigidbody2D _rigidBody2D;
    [SerializeField] private float DirectionalMultipler = 3f;
    public KeyCode upKey, downKey, leftKey, rightKey;

    [Header("Dash Settings")]
    [SerializeField] float _dashForce = 10f;
    [SerializeField] float _dashDuration = 1f;
    [SerializeField] float _dashCooldown = 2f;
    private bool isDashing = false, canDash = true;

    //Dummy will add to Player Stats
    public float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(upKey))
        {
            // Move in the direction the player is facing (transform.up)
            _rigidBody2D.AddForce((Vector2)transform.up * moveSpeed);
        }

        if (Input.GetKey(downKey))        //Charging up and dashing forwards
        {
            //_rigidBody2D.AddForce((Vector2)(-transform.up) * moveSpeed);

            //Dashing Mechanic (Charing dash is linear to the amount of speed gained from dash)
            if (canDash && !isDashing)
            {
                StartCoroutine(Dash());
            }
        }

        if (Input.GetKey(leftKey))
        {
            transform.Rotate(Vector3.forward * DirectionalMultipler);
        }

        if (Input.GetKey(rightKey))
        {
            transform.Rotate(Vector3.back * DirectionalMultipler);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        _rigidBody2D.AddForce((Vector2)transform.up * _dashForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(_dashCooldown);
        canDash = true;
    }
}
