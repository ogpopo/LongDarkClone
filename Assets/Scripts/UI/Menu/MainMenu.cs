
public class MainMenu : Menu
{
    private SceneTransition _sceneTransition; 
    public void GoToGame()
    {
        SceneTransition.SwitchToScene("Lake");
    }
}