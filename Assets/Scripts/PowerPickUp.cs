using UnityEngine;

public class PowerPickUp : MonoBehaviour
{
    public PowerUp powerUp;

    // Use Collider2D for 2D physics callbacks.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null)
            return;

        // Be more robust: check for the controller on the collider or its parents.
        PlayerPowerUpController powerUpController = other.GetComponent<PlayerPowerUpController>() 
                                                  ?? other.GetComponentInParent<PlayerPowerUpController>();

        if (powerUpController == null || powerUp == null)
            return;

        powerUpController.AddPowerUp(powerUp);
        Destroy(gameObject);
    }
}
