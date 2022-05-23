using System.Collections;
using UnityEngine;

public class FatigueEffect : INegativeable //todo МНЕ ПОНЯТНО ЧТО ЛУЧШЕ РЕАЛИЗОВАТЬ ЭТО ЧЕРЕЗ АБСТРАКТНЫЙ КЛАСС, НО Я ЗАХОТЕЛ ЧЕРЕЗ ИНТЕРФЕЙС
{
    [SerializeField] private int _fatigueDamage = 1;

    [SerializeField] private Health _health;
    [SerializeField] private Stamina _stamina;

    public void NegativeImpact()
    {
        FatigueHealthDamage();
        FatigueStaminaDamage();
    }

    public void Init()
    {
        _health = new FreezEffect().GetPlayerHealth();
        _stamina = GameObject.FindWithTag("Player").GetComponent<Stamina>();
    }

    private void FatigueStaminaDamage()
    {
        _stamina.RecoverySpeed = 2;
    }

    private void FatigueHealthDamage()
    {
        _health.TakeDamage(_fatigueDamage);
    }
}