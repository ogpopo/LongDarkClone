using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InputSystem))]
public class PlayerController : MonoBehaviour
{
    [Header("Настройки персонажа")]
    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField] private float _sprintSpeed = 1.1f;
    [SerializeField] private float _accelerationSpeed = 2.0f;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private Terrain _terrain;

    private float speed;
    private float _animationBlend;

    public int _animationIDSpeed;
    private bool _hasAnimator;

    private InputSystem _input;
    private CharacterController _characterController;
    private Animator _animator;

    public bool IsLive { get; set; } = true;//todo перенести это поле в клас хелф 

    void Start()
    {
        _hasAnimator = TryGetComponent(out _animator);

        _input = GetComponent<InputSystem>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        AssignAnimationIDs();
    }

    void Update()
    {
        _hasAnimator = TryGetComponent(out _animator);

        Move();
        Turn();
    }

    private void AssignAnimationIDs()
    {
        _animationIDSpeed = Animator.StringToHash("Speed_f");
    }

    private void Move()
    {
        if (IsLive)
        {
            var targetSpeed = _input.Sprint ? _sprintSpeed : _speed;

            if (_input.Move == Vector2.zero)
            {
                targetSpeed = 0;
            }

            var currentSpeed = new Vector2(_characterController.velocity.x, _characterController.velocity.y).magnitude;

            if (currentSpeed > targetSpeed || currentSpeed > targetSpeed)
            {
                speed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * _accelerationSpeed);

                speed = Mathf.Round(_speed / 1000) * 1000;
            }
            else
            {
                speed = targetSpeed;
            }

            var inputDirection = new Vector3(_input.Move.x, 0, _input.Move.y);

            _characterController.Move(inputDirection * targetSpeed * Time.deltaTime);

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * _accelerationSpeed);

            _animator.SetFloat(_animationIDSpeed, _animationBlend);
        }
    }

    private void Turn()
    {
        if (IsLive)
        {
            if (_input.Move != Vector2.zero)
            {
                Quaternion toTurn = Quaternion.LookRotation(new Vector3(_input.Move.x, 0, _input.Move.y), Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toTurn, _turnSpeed * Time.deltaTime);
            }
        }
    }
}