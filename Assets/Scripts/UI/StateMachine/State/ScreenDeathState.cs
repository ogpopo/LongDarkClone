public class ScreenDeathState : _MenuState
{
    public override void InitState(MenuController menuController)
    {
        base.InitState(menuController);

        _state = MenuController.MenuState.Death;
    }

    public void ReturnToGame()
    {
        _menuController.SetActiveState(MenuController.MenuState.Game);
        //todo туту должно быть маленько по другому, ПЕРЕДЕЛАТЬ
    }

    public void QuitGame()
    {
        _menuController.QuitGame();
    }
}