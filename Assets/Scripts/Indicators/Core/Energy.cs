using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Energy : Indicator
{
    [Space(10)]
    [SerializeField] private float fatigueRate;

    public override event Action<INegativeable> UsedUp;

    public override event Action<int, Indicator> ValueChanged;

    private void Awake()
    {
        InitNegativeEffect(new FatigueEffect());
    }

    private void Start()
    {
        AssignmentIndicatorType();

        StartCoroutine(Fatigue());
    }

    public void Replenishment(int value)
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

    IEnumerator Fatigue()
    {
        while (true)
        {
            while (_currentValue > _minValue)
            {
                yield return new WaitForSeconds(fatigueRate);
                _currentValue--;

                ValueChanged?.Invoke(_currentValue, _indicatorType);
            }

            while (_currentValue == _minValue)
            {
                yield return new WaitForSeconds(2);
                UsedUp?.Invoke(_negativeEffect);
            }
        }
    } 
}