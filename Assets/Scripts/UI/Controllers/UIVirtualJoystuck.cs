using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIVirtualJoystuck : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("Прямые Ссылки")]
    [SerializeField] private RectTransform _containerRect;
    [SerializeField] private RectTransform _handleRect;

    [Header("Настройки")]
    [SerializeField] private float _joystickRange = 50f;
    [SerializeField] private float _magnitudeMultiplier = 1f;

    [Header("Выход")]
    [SerializeField] private UnityEvent<Vector2> _joystickOutputEvent;

    private Vector2 position;
    
    void Start()
    {
        SetupHandle();
    }
    
    private void SetupHandle()
    {
        if(_handleRect)
        {
            UpdateHandleRectPosition(Vector2.zero);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_containerRect, eventData.position, eventData.pressEventCamera, out position);
        
        position = ApplySizeDelta(position);

        Vector2 clampedPosition = ClampValuesToMagnitude(position);
        
        OutputPointerEventValue(clampedPosition * _magnitudeMultiplier);

        if(_handleRect)
        {
            UpdateHandleRectPosition(clampedPosition * _joystickRange);
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OutputPointerEventValue(Vector2.zero);

        if(_handleRect)
        {
            UpdateHandleRectPosition(Vector2.zero);
        }

    }

    private void OutputPointerEventValue(Vector2 pointerPosition)
    {
        _joystickOutputEvent.Invoke(pointerPosition);
    }

    private void UpdateHandleRectPosition(Vector2 newPosition)
    {
        _handleRect.anchoredPosition = newPosition;
    }

    Vector2 ApplySizeDelta(Vector2 position)
    {
        float x = (position.x/_containerRect.sizeDelta.x) * 2.5f;
        float y = (position.y/_containerRect.sizeDelta.y) * 2.5f;
        
        return new Vector2(x, y);
    }

    Vector2 ClampValuesToMagnitude(Vector2 position)
    {
        return Vector2.ClampMagnitude(position, 1);
    }
}
