using System;
using UnityEngine;

namespace LongDark
{
    public class ExtractionInventoryUIController : MonoBehaviour
    {
        [SerializeField] private InventorySlotUIController _slotControllerPrefab;

        [SerializeField] private GameObject _canvas;

        private Inventory _displayedInventory;

        public Inventory DisplayedInventory => _displayedInventory;

        private void Awake()
        {
            ExtractionState.OnExtractionToggle += OnExtractionToggle;
        }

        private void OnDestroy()
        {
            ExtractionState.OnExtractionToggle -= OnExtractionToggle;
        }

        private void OnExtractionToggle(InventoryHolder inventoryHolder)
        {
            if (_displayedInventory == null)
            {
                gameObject.SetActive(true);
                _displayedInventory = inventoryHolder.Inventory;
                _displayedInventory.ForEach(CreateSlotController);
            }
            else if (_displayedInventory != null)
            {
                gameObject.SetActive(false);
                _displayedInventory = null;
                Array.ForEach(GetComponentsInChildren<InventorySlotUIController>(), slot => Destroy(slot.gameObject));
            }
        }

        private void CreateSlotController(InventorySlot slot)
        {
            InventorySlotUIController newSlotController = Instantiate(_slotControllerPrefab, transform);
            newSlotController.InventorySlot = slot;
        }
    }
}