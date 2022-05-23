using System;
using System.Collections.Generic;

public class Inventory
{
    private readonly List<InventorySlot> _slots = new List<InventorySlot>();
    
    public uint SlotCount => (uint) _slots.Count;

    public delegate void SlotUpdateCallback(InventorySlot slot);

    public SlotUpdateCallback OnSlotAdded;
    public SlotUpdateCallback OnSlotRemoved;
    
    public InventorySlot SelectSlot()
    {
        foreach (var inventory in _slots)
        {
            if (inventory.IsSelected)
            {
                return inventory;
            }
        }
        return null;
    }

    public bool IsSlotSelected()
    {
        foreach (var inventory in _slots)
        {
            if (inventory.IsSelected)
                return inventory.IsSelected;
        }

        return false;
    }

    public void ResetAllSelectSlot()
    {
        foreach (var slots in _slots)
        {
            slots.IsSelected = false;
        }
    }
    
    public InventorySlot CreateSlot()
    {
        InventorySlot newSlot = new InventorySlot();
        _slots.Add(newSlot);

        OnSlotAdded?.Invoke(newSlot);
        return newSlot;
    }
    
    public void DestroySlot(InventorySlot slot)
    {
        _slots.Remove(slot);
        OnSlotRemoved?.Invoke(slot);
    }
    
    public void Clear()
    {
        _slots.ForEach(slot => slot.Clear());
    }
    
    public void ForEach(Action<InventorySlot> action)
    {
        _slots.ForEach(slot => action(slot));
    }
    
    public InventorySlot FindFirst(Predicate<InventorySlot> predicate)
    {
        return _slots.Find(predicate);
    }

    public List<InventorySlot> FindAll(Predicate<InventorySlot> predicate)
    {
        return _slots.FindAll(predicate);
    }
}