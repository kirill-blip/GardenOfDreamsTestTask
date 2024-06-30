using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image _image;
    private RectTransform _rectTransform;

    public event UnityAction<InventoryItem> DraggedStopped;

    private void Awake()
    {
        _image = GetComponentInChildren<Image>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggedStopped?.Invoke(this);
        _image.raycastTarget = true;
        _rectTransform.anchoredPosition = Vector2.zero;
    }
}
