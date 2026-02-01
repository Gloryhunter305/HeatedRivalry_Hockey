using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpController : MonoBehaviour
{
    public PlayerStats Stats { get; private set; }
    private List<PowerUpInstance> activePowerUps = new List<PowerUpInstance>();

    public SpriteRenderer mask;
    public Sprite empty;
    public Sprite hockey;
    public Sprite luchador;
    public Sprite oni;

    public bool HasActivePowerUp => activePowerUps.Count > 0;

    private void Awake()
    {
        Stats = GetComponent<PlayerStats>();
        if (Stats == null)
        {
            Debug.LogWarning("PlayerStats component not found on player.", this);
        }
    }

    private void Update()
    {
        for (int i = activePowerUps.Count - 1; i >= 0; i--)
        {
            PowerUpInstance instance = activePowerUps[i];
            instance.remainingTime -= Time.deltaTime;
            if (instance.remainingTime <= 0f)
            {
                if (instance.ability != null && Stats != null)
                {
                    instance.ability.Remove(Stats);
                    mask.sprite = empty;
                }

                activePowerUps.RemoveAt(i);
            }
        }
    }

    // Returns true if the powerup was applied, false if rejected (e.g. already have a powerup)
    public bool AddPowerUp(PowerUp newPowerUp)
    {
        if (newPowerUp == null || Stats == null)
            return false;

        // Prevent stealing/picking up another powerup if player already has one
        if (HasActivePowerUp)
            return false;

        var instance = new PowerUpInstance(newPowerUp);
        activePowerUps.Add(instance);
        newPowerUp.Apply(Stats);
        Debug.Log(newPowerUp.name);
        if (newPowerUp.name == "BigTanky")
            mask.sprite = hockey;
        if (newPowerUp.name == "FasterStronger")
            mask.sprite = oni;
        if (newPowerUp.name == "ImmuneKnockback")
            mask.sprite = luchador;
        return true;
    }
}

public class PowerUpInstance
{
    public PowerUp ability;
    public float remainingTime;

    public PowerUpInstance(PowerUp data)
    {
        ability = data;
        remainingTime = data.duration;
    }
}
