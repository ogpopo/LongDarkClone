using UnityEngine;

public class StarvationEffect : INegativeable
{
    private Health _health;

    public void NegativeImpact()
    {
        _health.TakeDamage(1);
    }

    public void Init()
    {
        _health = new FreezEffect().GetPlayerHealth();
    }
}