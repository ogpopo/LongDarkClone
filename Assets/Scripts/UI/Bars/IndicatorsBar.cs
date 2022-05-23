using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable] 
class BarType
{
    [SerializeField] private Indicator _indicator;
    [SerializeField] private Image _barFilling;

    public Indicator Indicator => _indicator;

    public Image BarFilling
    {
        get => _barFilling;

        set => _barFilling = value;
    }
}

public class IndicatorsBar : MonoBehaviour
{
    [SerializeField] private BarType[] _barType;

    private void Awake()
    {
        foreach (var barType in _barType)
        {
            barType.Indicator.ValueChanged += OnChanged;
        }
    }

    private void Start()
    {
        foreach (var barType in _barType)
        {
            barType.BarFilling.fillAmount = (float) barType.Indicator.CurrentValue / 100;
        }
    }

    private void OnDestroy()
    {
        foreach (var barType in _barType)
        {
            barType.Indicator.ValueChanged -= OnChanged;
        }
    }

    private void OnChanged(int value, Indicator indicator)
    {
        var valueAsParcantage = (float) value / 100;
        try
        {
            foreach (var barType in _barType)
            {
                if (barType.Indicator.GetType() == indicator.GetType())
                {
                    barType.BarFilling.fillAmount = valueAsParcantage;
                }
            }
        }
        catch (NullReferenceException)
        {
            Debug.Log("ЕКСЕПШИОН");
        }
    }
}