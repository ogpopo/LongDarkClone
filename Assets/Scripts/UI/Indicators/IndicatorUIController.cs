using System;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorUIController : MonoBehaviour
{
    [Serializable]
    private class IndicatorType
    {
        public Text ValueText;
        public Indicator Indicator;
    }

    [SerializeField] private IndicatorType[] _indicatorTypes;

    private void Awake()
    {
        foreach (var indicatorType in _indicatorTypes)
        {
            indicatorType.Indicator.ValueChanged += OnChanged;
        }
    }

    private void Start()
    {
        foreach (var indicatorType in _indicatorTypes)
        {
            indicatorType.ValueText.text = indicatorType.Indicator.CurrentValue.ToString();
        }
    }

    private void OnDestroy()
    {
        foreach (var indicatorType in _indicatorTypes)
        {
            indicatorType.Indicator.ValueChanged -= OnChanged;
        }
    }

    private void OnChanged(int valueAsParcantage, Indicator indicator)
    {
        try
        {
            foreach (var indicatorType in _indicatorTypes)
            {
                if (indicatorType.Indicator.GetType() == indicator.GetType())
                {
                    indicatorType.ValueText.text = valueAsParcantage.ToString();
                }
            }
        }
        catch (NullReferenceException)
        {
            Debug.Log("ЕКСЕПШИОН");
        }
    }
    //
    // private void OnExtractionToggle(InventoryHolder inventoryHolder)
    // {
    //     
    // }
    //todo Можно сделать в одном стиле со всеми но пока нет.
    //todo Да и думаю это не так уж и надо ведь при каждом открытии инвентаря будет перебор массива и это может быть плохо
}