using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    public delegate void SprintCollback(bool state);

    public SprintCollback OnSprinting;

    private Stamina _stamina;

    private Vector2 _move;
    [SerializeField] private bool _sprint;

    public bool OnMovement => _move.magnitude > new Vector2(0, 0).magnitude;

    public Vector2 Move
    {
        get { return _move; }
    }

    public bool Sprint
    {
        get { return _sprint; }
    }

    private void Awake()
    {
        _stamina = GetComponent<Stamina>();
        _stamina.StaminaIsOver += StopSprint;
    }

    private void OnDestroy()
    {
        _stamina.StaminaIsOver -= StopSprint;
    }

    private void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    private void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        _move = newMoveDirection;
    }

    public void SprintInput(bool newSprintState)
    {
        if (OnMovement)
        {
            _sprint = newSprintState;

            OnSprinting?.Invoke(newSprintState);
        }
    }

    private void StopSprint()
    {
        _sprint = false;
        OnSprinting?.Invoke(false);
    }
}