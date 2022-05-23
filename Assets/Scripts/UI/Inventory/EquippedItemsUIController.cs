using System;
using UnityEngine;

[Serializable]
public class EquippedItemsUISlot
{
    public InventorySlotUIController SlotController;
    public InventoryItemType ItemType;
}

public class EquippedItemsUIController : MonoBehaviour
{

    [SerializeField] private EquippedItemsUISlot[] m_Slots;

    private Inventory m_DisplayedEquippedItemsInventory;

    private void Awake()
    {
        InventoryState.OnInventoryToggle += OnInventoryToggle;
    }

    private void OnDestroy()
    {
        InventoryState.OnInventoryToggle -= OnInventoryToggle;
    }

    private void OnInventoryToggle(InventoryHolder inventoryHolder)
    {
        Inventory equippedItemInventory = inventoryHolder.GetComponent<EquippedItemsHolder>().EquippedItemsInventory;

        if (m_DisplayedEquippedItemsInventory == null)
        {

            m_DisplayedEquippedItemsInventory = equippedItemInventory;

            if (m_DisplayedEquippedItemsInventory != null)
            {
                foreach (EquippedItemsUISlot slot in m_Slots)
                {
                    slot.SlotController.InventorySlot =
                        m_DisplayedEquippedItemsInventory.FindFirst(x => x.CanSlotContainItemType(slot.ItemType));
                }
            }
        }
        else if (m_DisplayedEquippedItemsInventory == equippedItemInventory)
        {
            m_DisplayedEquippedItemsInventory = null;
            Array.ForEach(m_Slots, x => x.SlotController.InventorySlot = null);
        }
    }
}