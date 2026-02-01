using UnityEngine;

public abstract class PowerUp : ScriptableObject
{
    public float duration;

    public abstract void Apply(PlayerStats stats);
    public abstract void Remove(PlayerStats stats);
}
