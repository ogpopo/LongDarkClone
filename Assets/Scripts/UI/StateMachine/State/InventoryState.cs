using System;
using UnityEngine;

public class InventoryState : _MenuState
{
    public static event Action<InventoryHolder> OnInventoryToggle;

    [SerializeField] private InventoryHolder _playerInventoryHolder;

    public override void InitState(MenuController menuController)
    {
        base.InitState(menuController);

        _state = MenuController.MenuState.Inventory;
    }

    public override void Rendering()
    {
        base.Rendering();

        OnInventoryToggle?.Invoke(_playerInventoryHolder);
    }

    public override void Erasing()
    {
        base.Erasing();

        OnInventoryToggle?.Invoke(_playerInventoryHolder);
    }
}