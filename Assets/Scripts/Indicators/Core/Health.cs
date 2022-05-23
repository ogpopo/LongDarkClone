using System;
using System.Collections;
using UnityEngine;

public class Health : Indicator, IDamageable
{
    public delegate void Died();

    public Died OnDied;

    public override event Action<INegativeable> UsedUp;

    public override event Action<int, Indicator> ValueChanged;

    [SerializeField] private int _speedHealing;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(20);
        }
    }

    private void Start()
    {
        AssignmentIndicatorType();
    }

    public void TakeDamage(int damage)
    {
        _currentValue -= damage;

        if (_currentValue > _minValue)
        {
            ValueChanged?.Invoke(_currentValue, _indicatorType);
        }
        else
        {
            _currentValue = 0;
            
            Death();
        }
    }

    public void Healing(int healValue)
    {
        if (healValue <= AcceptableValue)
        {
            _currentValue += healValue;
        }
        else if (healValue > AcceptableValue)
        {
            _currentValue += AcceptableValue;
        }
        
        ValueChanged?.Invoke(_currentValue, _indicatorType);
    }

    public IEnumerator GradualHealing(int healValue)
    {
        var curedValue = 0;

        while (curedValue < healValue)
        {
            yield return new WaitForSeconds(_speedHealing);

            curedValue++;

            _currentValue++;
            
            ValueChanged?.Invoke(_currentValue, _indicatorType);
        }
    }

    private void Death()
    {
        OnDied?.Invoke();

        ValueChanged?.Invoke(0, _indicatorType);
    }
}