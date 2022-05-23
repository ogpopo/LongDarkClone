using System;
using System.Linq;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private InventoryUIChannel _inventoryUIChannel;
    [SerializeField] private InventorySlotUIController _slotControllerPrefab;
    [SerializeField] private GameObject _inventoryCanvas;

    private Inventory _displayedInventory;
    public Inventory DisplayedInventory => _displayedInventory;

    private void Awake()
    {
        InventoryState.OnInventoryToggle += OnInventoryToggle;
        ExtractionState.OnInventoryToggle += OnInventoryToggle;
    }

    private void OnDestroy()
    {
        InventoryState.OnInventoryToggle -= OnInventoryToggle;
        ExtractionState.OnInventoryToggle -= OnInventoryToggle;
    }

    private void OnInventoryToggle(InventoryHolder inventoryHolder)
    {
        if (_displayedInventory == null)
        {
            _displayedInventory = inventoryHolder.Inventory;
            _displayedInventory.OnSlotAdded += CreateSlotController;
            _displayedInventory.OnSlotRemoved += DestroyInventorySlot;
            _displayedInventory.ForEach(CreateSlotController);
        }
        else if (_displayedInventory == inventoryHolder.Inventory)
        {
            Array.ForEach(GetComponentsInChildren<InventorySlotUIController>(), slot => Destroy(slot.gameObject));
            _displayedInventory.OnSlotRemoved -= DestroyInventorySlot;
            _displayedInventory.OnSlotAdded -= CreateSlotController;
            _displayedInventory = null;
        }
    }

    private void CreateSlotController(InventorySlot slot)
    {
        InventorySlotUIController newSlotController = Instantiate(_slotControllerPrefab, transform);
        newSlotController.InventorySlot = slot;
    }

    private void DestroyInventorySlot(InventorySlot slot)
    {
        InventorySlotUIController foundController = GetComponentsInChildren<InventorySlotUIController>()
            .FirstOrDefault(slotController => slotController.InventorySlot == slot);
        if (foundController != null)
        {
            Destroy(foundController.gameObject);
        }
    }
}