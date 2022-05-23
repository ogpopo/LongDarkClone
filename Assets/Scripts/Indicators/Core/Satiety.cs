using System;
using System.Collections;
using UnityEngine;

public class Satiety : Indicator
{
    public override event Action<INegativeable> UsedUp;

    public override event Action<int, Indicator> ValueChanged;

    [SerializeField] private int _speedHunger;

    private void Awake()
    {
        InitNegativeEffect(new StarvationEffect());
    }

    private void Start()
    {
        AssignmentIndicatorType();

        StartCoroutine(Hunger());
    }

    public void Eat(int value)
    {
        if (value <= AcceptableValue)
        {
            _currentValue += value;
        }
        else if (value > AcceptableValue)
        {
            _currentValue += AcceptableValue;
        }

        ValueChanged?.Invoke(_currentValue, _indicatorType);
    }

    private IEnumerator Hunger()
    {
        while (true)
        {
            while (_currentValue > _minValue)
            {
                yield return new WaitForSeconds(_speedHunger);

                _currentValue--;

                ValueChanged?.Invoke(_currentValue, _indicatorType);
            }

            while (_currentValue == _minValue)
            {
                yield return new WaitForSeconds(1);
                UsedUp?.Invoke(_negativeEffect);
            }
        }
    }
}