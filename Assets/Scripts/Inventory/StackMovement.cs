using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StackMovement : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image _image;
    private RectTransform _rectTransform;
    private Stack _stack;

    public event UnityAction<Stack> DraggingStopped;
    public event UnityAction<Stack> StackClicked;

    private void Awake()
    {
        _image = GetComponentInChildren<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _stack = GetComponent<Stack>();
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
        DraggingStopped?.Invoke(_stack);

        _image.raycastTarget = true;
        _rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StackClicked?.Invoke(_stack);
    }
}
