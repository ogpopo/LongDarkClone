using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameState : _MenuState
{
    [SerializeField] Health _playerHealth;

    private void Awake()
    {
        _playerHealth.OnDied += JampToScreenDeathState;
    }

    private void OnDestroy()
    {
        _playerHealth.OnDied -= JampToScreenDeathState;
    }

    public override void InitState(MenuController menuController)
    {
        base.InitState(menuController);

        _state = MenuController.MenuState.Game;
    }

    public void JampToInventoryState()
    {
        _menuController.SetActiveState(MenuController.MenuState.Inventory);
    }

    public void JampToExtraxtionState()
    {
        _menuController.SetActiveState(MenuController.MenuState.Extraction);
    }

    private void JampToScreenDeathState()
    {
        _menuController.SetActiveState(MenuController.MenuState.Death);
    }
}