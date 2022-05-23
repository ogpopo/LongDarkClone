using UnityEngine;

public class Door : Environment
{
    [SerializeField] private float _degreeDoorOpening;
    [SerializeField] private Transform _playerTransform;

    private Animator _doorAnimator;
    private Transform _doorTransform;

    private bool PlayerIsClose => (_doorTransform.position - _playerTransform.position).magnitude < new Vector2((float) 0.8,(float)0.8).magnitude;

    private void Start()
    {
        _doorAnimator = GetComponent<Animator>();
        _doorTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (PlayerIsClose)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        _doorAnimator.SetBool("isOpen", true);
    }

    private void CloseDoor()
    {
        _doorAnimator.SetBool("isOpen", false);
    }
}