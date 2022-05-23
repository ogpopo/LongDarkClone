using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private _MenuState[] _allMenus;

    public enum MenuState
    {
        Game,
        Inventory,
        Extraction,
        Settings,
        Death
    }

    private Dictionary<MenuState, _MenuState> _menuDictionary = new Dictionary<MenuState, _MenuState>();

    private _MenuState _activeState;

    private Stack<MenuState> _stateHistory = new Stack<MenuState>();

    void Start()
    {
        foreach (_MenuState menu in _allMenus)
        {
            if (menu == null)
                continue;

            menu.InitState(menuController: this);

            if (_menuDictionary.ContainsKey(menu._state))
                continue;

            _menuDictionary.Add(menu._state, menu);
        }

        foreach (MenuState state in _menuDictionary.Keys)
        {
            _menuDictionary[state].gameObject.SetActive(false);
        }

        SetActiveState(MenuState.Game);
    }

    public void JumpBack()
    {
        if (_stateHistory.Count <= 1)
        {
            SetActiveState(MenuState.Settings);
        }
        else
        {
            _stateHistory.Pop();

            SetActiveState(_stateHistory.Peek(), isJumpingBack: true);
        }
    }

    public void SetActiveState(MenuState newState, bool isJumpingBack = false)
    {
        if (!_menuDictionary.ContainsKey(newState))
            return;

        if (_activeState != null)
            _activeState.Erasing();

        _activeState = _menuDictionary[newState];

        _activeState.Rendering();


        if (!isJumpingBack)
        {
            _stateHistory.Push(newState);
        }
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Ты выгнан из ОРДЫЫЫЫЫЫ");
    }
}