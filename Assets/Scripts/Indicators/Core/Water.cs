using System;
using System.Collections;
using UnityEngine;

public class Water : Indicator
{
    public override event Action<INegativeable> UsedUp;

    public override event Action<int, Indicator> ValueChanged;

    [SerializeField] private int _speedThirst;

    private void Awake()
    {
        InitNegativeEffect(new DehydrationEffect());
    }

    private void Start()
    {
        AssignmentIndicatorType();
        
        StartCoroutine(Thirst());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Quenching(5);
        }
    }

    public void Quenching(int value)
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

    IEnumerator Thirst()
    {
        while (true)
        {
            while (_currentValue > _minValue)
            {
                yield return new WaitForSeconds(_speedThirst);

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