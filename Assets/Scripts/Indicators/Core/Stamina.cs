using System;
using System.Collections;
using UnityEngine;

public class Stamina : Indicator
{
    public override event Action<INegativeable> UsedUp;

    public override event Action<int, Indicator> ValueChanged;

    public delegate void StaminaCollback();

    public StaminaCollback StaminaIsOver;

    [Space(10)]
    [SerializeField]
    private float _recoverySpeed;

    [SerializeField] private float _costsSpeed;

    private InputSystem _inputSystem;

    private Coroutine _currentCoroutine;

    private bool IsStamina => _currentValue > _minValue;

    public float RecoverySpeed
    {
        get { return _recoverySpeed; }
        set
        {
            if (value > 10 || value <= 0)
                new ArgumentOutOfRangeException();
            else
                _recoverySpeed = value;
        }
    }

    private void Awake()
    {

        _inputSystem = GetComponent<InputSystem>();

        _inputSystem.OnSprinting += Expenditure;
    }

    private void OnDestroy()
    {
        _inputSystem.OnSprinting -= Expenditure;
    }

    private void Start()
    {
        AssignmentIndicatorType();
    }

    public void SetRecoverySpeedToDefaultValue()
    {
        _recoverySpeed = 0.07f;
    }

    private void Expenditure(bool state)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(ChangeCurentValue(state));
    }

    IEnumerator ChangeCurentValue(bool state)
    {
        if (state)
        {
            while (IsStamina)
            {
                yield return new WaitForSeconds(_costsSpeed);

                _currentValue--;

                if (!IsStamina)
                {
                    StaminaIsOver?.Invoke();
                }

                ValueChanged?.Invoke(_currentValue, _indicatorType);
            }
        }
        else
        {
            while (_currentValue < _maxValue)
            {
                yield return new WaitForSeconds(_recoverySpeed);

                _currentValue++;

                ValueChanged?.Invoke(_currentValue, _indicatorType);
            }
        }
    }
}