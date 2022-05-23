using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    [SerializeField]
    private InventoryChannel _inventoryChannel;
    [SerializeField] 
    private uint _defaultSlotCount = 0;
    [SerializeField] 
    private bool _canCreateSlots = false;
    
    private Inventory _inventory = new Inventory();
    
    public Inventory Inventory => _inventory;

    private void Awake()
    {
        _inventoryChannel.OnInventoryItemLoot += OnInventoryItemLoot;
    }

    private void Start()
    {
        for (var i = 0; i < _defaultSlotCount; i++)
        {
            _inventory.CreateSlot();
        }
    }

    private void OnDestroy()
    {
        _inventoryChannel.OnInventoryItemLoot -= OnInventoryItemLoot;
    }
    
    private void OnInventoryItemLoot(InventoryItem item, uint quantity)// ДОБАВЛЯЕТ АЙТЕМ ЛУТ ЧЕРЕЗ ДЕЛЕГАТ 
    {
        InventorySlot slotToUse = _inventory.FindFirst(slot => slot.Item == item);// ИЩЕТ ТАКОЙ ЖЕ АЙТЕМ ДОБАВЛЕНИЯ В ИНВЕНАРЕ ПО СЛОТАМ
        if (slotToUse == null)
        {
            slotToUse = _inventory.FindFirst(slot => slot.Item == null);// ЕСЛИ НЕТ СВОБОДНОЙ ЯЧЕЙКИ ТО ПРИСВАЕВАЕТ НУЛЛ
        }

        if (slotToUse == null && _canCreateSlots)
        {
            slotToUse = _inventory.CreateSlot(); // ЕСЛИ НУЛЛ И ФЛАГ "РАЗРЕШЕНИЯ СОЗДАТЬ" СТОИТ В ТРУ ТО СОЗДАЕТ НОВЫЙ СЛОТ
        }

        slotToUse?.StoreItem(item, quantity); //ЕСЛИ НЕ НУЛЛ КЛАДЫВАЕТ В СТАК
    }
}
