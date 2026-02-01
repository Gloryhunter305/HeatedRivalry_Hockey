using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpController : MonoBehaviour
{
    public PlayerStats Stats { get; private set; }
    private List<PowerUpInstance> activePowerUps = new List<PowerUpInstance>();

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
                }

                activePowerUps.RemoveAt(i);
            }
        }
    }

    public void AddPowerUp(PowerUp newPowerUp)
    {
        if (newPowerUp == null || Stats == null)
            return;

        var instance = new PowerUpInstance(newPowerUp);
        activePowerUps.Add(instance);
        newPowerUp.Apply(Stats);
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
