using TMPro;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] private InventoryItem _inventoryItem;

    [Space(10f)]
    [SerializeField] private TextMeshProUGUI _countText;

    private int _currentCount = 1;
    private int _maxCount = 6;

    private StackMovement _stackMovement;

    public StackMovement StackMovement => _stackMovement;
    public InventoryItem InventoryItem => _inventoryItem;

    private void Awake()
    {
        _stackMovement = GetComponent<StackMovement>();

        Init();
        UpdateText();
    }

    private void UpdateText()
    {
        string text = string.Empty;

        if (_maxCount > 1)
        {
            text = _currentCount.ToString();
        }

        _countText.text = text;
    }

    public bool IsStackFull()
    {
        return _currentCount == _maxCount;
    }

    public bool IsEmpty()
    {
        return _currentCount == 0;
    }

    public void AddItem()
    {
        if (_currentCount < _maxCount)
        {
            _currentCount++;

            UpdateText();
        }
    }

    public void RemoveItem()
    {
        if (_currentCount > 0)
        {
            _currentCount--;

            UpdateText();
        }
    }

    public void RemoveItem(int count)
    {
        for (int i = 0; i < count; i++)
        {
            RemoveItem();
        }
    }

    public void Init()
    {
        if (_inventoryItem != null)
        {
            _currentCount = _inventoryItem.Count;
            _maxCount = _inventoryItem.MaxCount;
        }
    }

    public void Init(InventoryItem randomItem)
    {
        _inventoryItem = randomItem;

        Init();
    }

    public void FillToMaxCount()
    {
        _currentCount = _maxCount;
        UpdateText();
    }

    public void SetCountToZero()
    {
        _currentCount = 0;
        UpdateText();
    }
}
