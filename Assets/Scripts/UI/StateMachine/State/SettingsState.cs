public class SettingsState : _MenuState
{
    public override void InitState(MenuController menuController)
    {
        base.InitState(menuController);

        _state = MenuController.MenuState.Settings;
    }
}