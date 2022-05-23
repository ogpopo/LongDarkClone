using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PersonalGravity : MonoBehaviour
{
    [SerializeField] private float _groundedOffset;
    [SerializeField] private float _groundedRadius;
    [SerializeField] private float _farceGravity;
    [SerializeField] private LayerMask _groundLayers;

    private CharacterController _characterController;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;
    private bool Grounded;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GroundedCheck();
        ApplyingForce();
    }

    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _groundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, _groundedRadius, _groundLayers, QueryTriggerInteraction.Ignore);
    }

    private void ApplyingForce()
    {
        if (!Grounded)
        {
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += _farceGravity * Time.deltaTime;
            }
        }

        _characterController.Move(new Vector3(0, _verticalVelocity, 0));
    }
    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (Grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - _groundedOffset, transform.position.z), _groundedRadius);
    }
}