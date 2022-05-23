public class DehydrationEffect : INegativeable
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