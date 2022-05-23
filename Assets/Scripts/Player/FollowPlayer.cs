using System;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _translateSpeed;

    [SerializeField]
    private RenderSettings _lightingSettings;

    private Vector3 _focusPoint;

    private void Start()
    {
        RenderSettings.ambientGroundColor = Color.red;
    }

    void Update()
    {
        UpdateFocusPoint();

        MovingFocusPoint();
    }

    private void UpdateFocusPoint()
    {
        _focusPoint = _player.transform.position + _offset;
    }

    private void MovingFocusPoint()
    {
        transform.position = Vector3.Lerp(transform.position, _focusPoint, _translateSpeed * Time.deltaTime);
    }
}