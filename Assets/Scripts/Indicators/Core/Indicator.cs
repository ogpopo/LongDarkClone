using System;
using UnityEngine;

public abstract class Indicator : MonoBehaviour
{
    public abstract event Action<INegativeable> UsedUp;

    public abstract event Action<int, Indicator> ValueChanged;

    [SerializeField] protected int _currentValue;

    protected int _maxValue = 100;
    protected int _minValue = 0;

    protected Indicator _indicatorType;

    public INegativeable _negativeEffect { get; private set; }

    public int CurrentValue => _currentValue;

    protected int AcceptableValue => _maxValue - _currentValue;

    protected void AssignmentIndicatorType()
    {

        _indicatorType = this;
    }

    protected void InitNegativeEffect(INegativeable negativeable)
    {
        _negativeEffect = negativeable;
    }

    protected void OnValidate()
    {
        _currentValue = Mathf.Clamp(_currentValue, _minValue, _maxValue);
    }
}