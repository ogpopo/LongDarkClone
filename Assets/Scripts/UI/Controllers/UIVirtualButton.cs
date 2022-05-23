using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class UIVirtualButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Выход")] 
    [SerializeField] private UnityEvent<bool> _buttonStateOutputEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        OutputButtonStateValue(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OutputButtonStateValue(false);
    }

    void OutputButtonStateValue(bool value)
    {
        _buttonStateOutputEvent?.Invoke(value);
    }
}