using UnityEngine;

public class FreezEffect : INegativeable
{
    private Health _health;

    public void NegativeImpact()
    {Debug.Log("2");
        _health.TakeDamage(1);
    }

    public void Init()
    {
        _health = GetPlayerHealth();
    }

    public Health GetPlayerHealth()
    {
        return GameObject.FindWithTag("Player").GetComponent<Health>();
    }
}