using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUIController : MonoBehaviour
{
    [SerializeField] private Text _textField;
    [SerializeField] private Text _quantityField;
    [SerializeField] private Image _imageField;
    [SerializeField] private Sprite _emptySlotSprite;
    [SerializeField] private Color _highlightColor;
    [SerializeField] private Color _selectedColor;

    private Image _image;

    private InventorySlot _inventorySlot;

    private Color _defaultColor;
    private bool _isHighlighted = false;

    public InventorySlot InventorySlot
    {
        get { return _inventorySlot; }
        set
        {
            if (_inventorySlot != null)
            {
                _inventorySlot.OnItemChange -= UpdateSlot;
            }

            _inventorySlot = value;
            UpdateSlot(_inventorySlot);

            if (_inventorySlot != null)
            {
                _inventorySlot.OnItemChange += UpdateSlot;
            }
        }
    }

    public bool IsHighlighted
    {
        get { return _isHighlighted; }
        set
        {
            _isHighlighted = value;

            if (_image != null)
            {
                _image.color = (_isHighlighted ? _highlightColor : _defaultColor);
            }
        }
    }

    private void Update()
    {
        if (_inventorySlot != null)
        {
            _image.color = InventorySlot.IsSelected ? _selectedColor : _defaultColor;
        }
    }

    private void Start()
    {
        _image = GetComponent<Image>();

        if (_image != null)
        {
            _defaultColor = _image.color;
        }

        _selectedColor.a = 1;
    }

    private void OnDestroy()
    {
        InventorySlot = null;
    }

    public void DestroySlot()
    {
        InventoryUIController inventory = GetComponentInParent<InventoryUIController>();
        if (inventory != null)
        {
            inventory.DisplayedInventory?.DestroySlot(_inventorySlot);
        }
    }

    private void UpdateSlot(InventorySlot slot)
    {
        bool displaySlot = _inventorySlot != null && _inventorySlot.Item != null;

        if (_textField != null)
        {
            _textField.gameObject.SetActive(displaySlot);
            _textField.text = (displaySlot ? _inventorySlot.Item.Name : "");
        }

        if (_quantityField != null)
        {
            _quantityField.gameObject.SetActive(displaySlot);
            _quantityField.text = (displaySlot ? _inventorySlot.Quantity.ToString() : "");
        }

        if (_imageField != null)
        {
            _imageField.gameObject.SetActive(displaySlot || _emptySlotSprite != null);
            _imageField.sprite = (displaySlot ? _inventorySlot.Item.Sprite : _emptySlotSprite);
        }
    }
}