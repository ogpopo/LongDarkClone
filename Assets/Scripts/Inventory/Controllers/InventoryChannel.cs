using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Inventory/InventoryChannel")]
public class InventoryChannel : ScriptableObject 
{
    public delegate void InventoryItemLootCallback(InventoryItem item, uint quantity);

    public InventoryItemLootCallback OnInventoryItemLoot;
    
    public void RaiseLootItem(InventoryItem item) 
    {
        OnInventoryItemLoot?.Invoke(item, 1);
    }
    
    public void RaiseLootItem(InventoryItem item, uint quantity)
    {
        OnInventoryItemLoot?.Invoke(item, quantity);
    }
}
