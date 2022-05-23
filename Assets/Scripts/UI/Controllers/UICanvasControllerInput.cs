using System;
using UnityEngine;

public class UICanvasControllerInput : MonoBehaviour
{
    public event Action<bool> OnSprint;
    
    [Header("Output")] 
    [SerializeField] 
    private InputSystem _inputSystem;
    [SerializeField] 
    private InventoryUIChannel _inventoryUIChannel;

    private void Awake()
    {
        _inventoryUIChannel.OnInventoryToggle += OnControllerElementsToggle;
    }

    private void OnDestroy()
    {
        _inventoryUIChannel.OnInventoryToggle -=  OnControllerElementsToggle;
    }

    private void OnControllerElementsToggle(InventoryHolder inventoryHolder)
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void VirtualMobileInput(Vector2 virtualMobileDirection)
    {
         _inputSystem.MoveInput(virtualMobileDirection);
    }

    public void SprintMobileInput(bool virtualMobileState)
    {
        _inputSystem.SprintInput(virtualMobileState);
        
        OnSprint?.Invoke(virtualMobileState);
    }
}
