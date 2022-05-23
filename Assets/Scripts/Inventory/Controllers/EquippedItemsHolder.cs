using UnityEngine;

public class EquippedItemsHolder : MonoBehaviour
{
    [SerializeField]
    InventoryItemType[] m_SlotItemTypes;

    private Inventory m_EquippedItemsInventory = new Inventory();
    public Inventory EquippedItemsInventory => m_EquippedItemsInventory;

    private void Awake()
    {
        foreach (InventoryItemType itemType in m_SlotItemTypes)
        {
            InventorySlot newSlot = m_EquippedItemsInventory.CreateSlot();
            newSlot.MaxQuantity = 1;
            newSlot.AddAllowedItemType(itemType);
        }
    }
}
