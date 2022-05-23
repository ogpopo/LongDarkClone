using UnityEngine;

public class _MenuState : MonoBehaviour
{
    public MenuController.MenuState _state { get; protected set; }

    protected MenuController _menuController;

    public virtual void InitState(MenuController menuController)
    {
        _menuController = menuController;
    }

    public void JumpBack()
    {
        _menuController.JumpBack();
    }

    public virtual void Rendering()
    {
        gameObject.SetActive(true);
    }

    public virtual void Erasing()
    {
        gameObject.SetActive(false);
    }
}