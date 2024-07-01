using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Stack _stack;
    [SerializeField] private RectTransform _stackTransform;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag is not null && !HasItem())
        {
            AddItem(eventData.pointerDrag.GetComponent<Stack>());
        }
    }

    public bool HasItem()
    {
        return _stack is not null;
    }

    public void AddItem(Stack stack)
    {
        Debug.LogError("I'm here");

        _stack = stack;
        _stackTransform = stack.GetComponent<RectTransform>();

        _stackTransform.SetParent(_rectTransform);
        _stackTransform.anchoredPosition = Vector2.zero;
    }

    public void RemoveStack()
    {
        _stack = null;
        _stackTransform = null;
    }

    public Stack GetStack()
    {
        return _stack;
    }
}
