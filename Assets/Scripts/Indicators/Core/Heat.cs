using System;
using System.Collections;
using UnityEngine;

public class Heat : Indicator
{
    public override event Action<INegativeable> UsedUp;

    public override event Action<int, Indicator> ValueChanged;

    private Coroutine _currentCoroutine;
    
    private ThermalZone _lastZone;

    private void Awake()
    {
        InitNegativeEffect(new FreezEffect());
    }

    private void Start()
    {
        AssignmentIndicatorType();

        if (_lastZone != null)
        {
            
        }
        //todo тута нужно подумать с реализацией сохронения последне тепловой зоны зоны
    }

    public void ChangeCurentValue(int rateDeterioration, ThermalZone thermalZone)
    {
        _lastZone = thermalZone;
        
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        if (thermalZone.GetType() == new SubzeroZone().GetType())
        {
            _currentCoroutine = StartCoroutine(Freezing(rateDeterioration));
        }
        else if(thermalZone.GetType() == new HeatZone().GetType())
        {
            _currentCoroutine = StartCoroutine(Heating(rateDeterioration));
        }
    }

    private IEnumerator Heating(int rateDeterioration)
    {
        while (_currentValue < _maxValue)
        {
            yield return new WaitForSeconds(rateDeterioration);

            _currentValue++;
            
            ValueChanged?.Invoke(_currentValue, _indicatorType);
        }
    }

    private IEnumerator Freezing(int rateDeterioration)
    {
        while (_currentValue > _minValue)
        {
            yield return new WaitForSeconds(rateDeterioration);

            _currentValue--;
            
            ValueChanged?.Invoke(_currentValue, _indicatorType);
        }

        while (_currentValue == _minValue)
        {
            Debug.Log("1");
            yield return new WaitForSeconds(2);
            UsedUp?.Invoke(_negativeEffect);
        }
    }
}