using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Inventory/InventoryUIChannel")]
public class InventoryUIChannel : ScriptableObject
{
    public delegate void InventoryToggleCallback(InventoryHolder inventoryHolder);
    public InventoryToggleCallback OnInventoryToggle;

    public void RaiseToggle(InventoryHolder inventoryHolder)
    { 
        OnInventoryToggle?.Invoke(inventoryHolder);
    }
}