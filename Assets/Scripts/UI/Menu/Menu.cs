using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Menu : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Произошел выход из игры");
        Application.Quit();
    }

    public void ChangingTab(GameObject tabOpens)
    {
        tabOpens.SetActive(true);

        var button = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        
        button.SetActive(false);
    }
}