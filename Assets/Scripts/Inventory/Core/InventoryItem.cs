using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Inventory/InventoryItem")]
public class InventoryItem : CompositeScriptableObject 
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private InventoryItemType _itemType;

    public string Name => _name;
    public Sprite Sprite => _sprite;
    public InventoryItemType ItemType => _itemType;
}