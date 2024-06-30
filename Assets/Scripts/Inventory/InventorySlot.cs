using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private InventoryItem _inventoryItem;
    [SerializeField] private RectTransform _inventoryItemTransform;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag is not null && !HasItem())
        {
            AddItem(eventData.pointerDrag.GetComponent<InventoryItem>());
        }
    }

    public bool HasItem()
    {
        return _inventoryItem is not null;
    }

    public void AddItem(InventoryItem inventoryItem)
    {
        _inventoryItem = inventoryItem;
        _inventoryItemTransform = inventoryItem.GetComponent<RectTransform>();

        _inventoryItemTransform.SetParent(_rectTransform);
        _inventoryItemTransform.anchoredPosition = Vector2.zero;
    }

    public void RemoveItem(InventoryItem inventoryItem)
    {
        if (_inventoryItem == inventoryItem && _inventoryItemTransform.parent != _rectTransform)
        {
            _inventoryItem = null;
            _inventoryItemTransform = null;
        }
    }
}
