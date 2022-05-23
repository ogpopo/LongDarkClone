using System;
using LongDark;
using UnityEngine;

public class ExtractionState : _MenuState
{
    public static event Action<InventoryHolder> OnExtractionToggle;
    public static event Action<InventoryHolder> OnInventoryToggle;

    [SerializeField] private InventoryHolder _playerHolder;

    private InventoryHolder _extractionHilder;

    private void Awake()
    {
        ViewLootingButton.OnLooting += SetingExtractionHolder;
    }

    private void OnDestroy()
    {
        ViewLootingButton.OnLooting -= SetingExtractionHolder;
    } 

    public override void InitState(MenuController menuController)
    {
        base.InitState(menuController);

        _state = MenuController.MenuState.Extraction;
    }

    public override void Rendering()
    {
        base.Rendering();

        OnExtractionToggle?.Invoke(_extractionHilder);
        OnInventoryToggle?.Invoke(_playerHolder);
    }

    public override void Erasing()
    {
        base.Erasing();

        OnExtractionToggle?.Invoke(_extractionHilder);
        OnInventoryToggle?.Invoke(_playerHolder);
    }

    private void SetingExtractionHolder(InventoryHolder extractionHolder)
    {
        _extractionHilder = extractionHolder;
    }
}