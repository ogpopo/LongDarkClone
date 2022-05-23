using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedSlot : MonoBehaviour
{
    private InventorySlot _slot;
    private bool _isSlotSelected;

    private Inventory _inventory;
    private Inventory _inventoryHolder;

    private void Update() // ЭТО УЖАСНО, Я ЗНАЮ, ПИСАЛ В СПЕШКЕ 
    {
        if (Input.GetMouseButtonDown(0))
        {
            InventorySlot slot = FindSlotAtPosition();

            if (slot != null)
            {
                if (slot.IsSelected)
                {
                    slot.IsSelected = false;

                    _isSlotSelected = false;
                }
                else
                {
                    if (_isSlotSelected)
                    {
                        try
                        {
                            slot.MoveAllTo(_slot);

                            _isSlotSelected = false;

                            _slot.IsSelected = false;
                        }
                        catch (FailedToMoveItemToSlotException)
                        {
                            Debug.Log("ЭЭЭЭЭЭЭксепшион");
                        }
                    }
                    else
                    {
                        slot.IsSelected = true;

                        _slot = slot;
                        _isSlotSelected = true;
                    }
                }
            }
        }
    }

    private InventorySlot FindSlotAtPosition()
    {
        InventorySlot foundSlot = null;

        PointerEventData pointerEventData = new PointerEventData(null);
        pointerEventData.position = transform.position;

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            InventorySlot slotController = null;
            try
            {
                slotController = result.gameObject.GetComponent<InventorySlotUIController>().InventorySlot;
            }
            catch (NullReferenceException)
            {
            }

            if (slotController != null)
            {
                foundSlot = slotController;
                break;
            }
        }

        return foundSlot;
    }
}