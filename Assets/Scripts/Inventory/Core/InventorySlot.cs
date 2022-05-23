using System;
using System.Collections.Generic;

public class FailedToMoveItemToSlotException : Exception 
{
}

public class InventorySlot 
{
    public delegate void ItemChangeCallback(InventorySlot slot);

    public ItemChangeCallback OnItemChange;

    private InventoryItem _item;
    private uint _quantity;
    private uint _maxQuantity = uint.MaxValue;
    private List<InventoryItemType> _allowedItemTypes = new List<InventoryItemType>();

    public InventoryItem Item => _item;
    public uint Quantity => _quantity;

    private bool _isSelected = false;

    public bool IsSelected
    {
        get => _isSelected;
        set => _isSelected = value;
    }
    
    public uint MaxQuantity
    {
        get => _maxQuantity;
        set => _maxQuantity = value;
    }


    public void AddAllowedItemType(InventoryItemType itemType)
    {
        _allowedItemTypes.Add(itemType);
    }

    public void StoreItem(InventoryItem item, uint quantity)
    {
        if ((_item == null || _item == item) && CanSlotContainItem(item) && CanAddItemsToSlot(quantity))
        {
            _item = item;
            _quantity += quantity;
            OnItemChange?.Invoke(this);
        }
        else
        {
            throw new FailedToMoveItemToSlotException();
        }
    }

    public void Clear()
    {
        _item = null;
        _quantity = 0;
        OnItemChange?.Invoke(this);
    }

    public void MoveAllTo(InventorySlot slotDestination)
    {
        MoveTo(slotDestination, _quantity);
    }

    public void MoveTo(InventorySlot slotDestination, uint quantity) 
    {
        if (slotDestination == null || quantity > _quantity || !CanSlotContainItem(slotDestination._item) || !slotDestination.CanSlotContainItem(_item))
        {
            throw new FailedToMoveItemToSlotException();
        }
        else
        {
            if (slotDestination._item == _item || slotDestination._item == null)
            {
                uint movableQuantity = Math.Min(quantity, slotDestination.MaxQuantity - slotDestination.Quantity);
                slotDestination._item = _item;
                slotDestination._quantity += movableQuantity;
                _quantity -= movableQuantity;

                if (_quantity == 0)
                {
                    Clear();
                }
            }
            else if (_quantity == quantity)
            {
                if (CanSlotHoldItems(slotDestination._quantity) && slotDestination.CanSlotHoldItems(_quantity))
                {
                    Utils.Swap(ref slotDestination._item, ref _item);
                    Utils.Swap(ref slotDestination._quantity, ref _quantity);
                }
                else
                {
                    throw new FailedToMoveItemToSlotException();
                }
            }
            else
            {
                throw new FailedToMoveItemToSlotException();
            }
        }
        
        OnItemChange?.Invoke(this);
        slotDestination.OnItemChange?.Invoke(slotDestination);
     }

    private bool CanSlotContainItem(InventoryItem item) //Может Ли Слот Содержать Элемент
    {
        return item == null || CanSlotContainItemType(item.ItemType);
    }

    public bool CanSlotContainItemType(InventoryItemType itemType)
    {
        return _allowedItemTypes.Count == 0 || _allowedItemTypes.Contains(itemType);
    }

    private bool CanAddItemsToSlot(uint quantity)
    {
        return CanSlotHoldItems(_quantity + quantity);
    }

    private bool CanSlotHoldItems(uint quantity)
    {
        return quantity <= _maxQuantity;
    }
}