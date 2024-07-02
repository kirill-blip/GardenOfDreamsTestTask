using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Stack _stackPrefab;
    [SerializeField] private List<InventoryItem> _allItems;

    [SerializeField] private PopupPanel _popupPanel;

    [SerializeField] private List<Stack> _inventoryStacks = new();
    [SerializeField] private List<InventorySlot> _inventorySlots = new();

    private bool _isStackLoaded = false;

    private void Start()
    {
        foreach (Stack stack in _inventoryStacks)
        {
            stack.StackMovement.DraggingStopped += DraggedStoppedHandler;
            stack.StackMovement.StackClicked += StackClickedHandler;
        }

        if (!_isStackLoaded)
        {
            StartCoroutine(AddItemsToRandomSlots());
        }
    }

    private void StackClickedHandler(Stack stack)
    {
        _popupPanel.ActivatePanel(stack.InventoryItem);
    }

    private void DraggedStoppedHandler(Stack stack)
    {
        foreach (InventorySlot inventorySlot in _inventorySlots)
        {
            bool hasStackInSlot = stack == inventorySlot.GetStack();
            bool isNotParent = stack.transform.parent != inventorySlot.transform.parent;

            if (hasStackInSlot && isNotParent)
            {
                inventorySlot.RemoveStack();
            }
        }
    }

    private IEnumerator AddItemToRandomSlot(Stack stack)
    {
        while (stack.transform.parent == null)
        {
            InventorySlot inventorySlot = _inventorySlots[Random.Range(0, _inventorySlots.Count)];

            if (inventorySlot.HasItem())
            {
                yield return null;
                continue;
            }

            inventorySlot.AddItem(stack);
        }
    }

    private IEnumerator AddItemsToRandomSlots()
    {
        int countOfInventoryItems = _inventoryStacks.Count;

        while (countOfInventoryItems > 0)
        {
            InventorySlot inventorySlot = _inventorySlots[Random.Range(0, _inventorySlots.Count)];

            if (inventorySlot.HasItem())
            {
                yield return null;
                continue;
            }

            inventorySlot.AddItem(_inventoryStacks[countOfInventoryItems - 1]);
            countOfInventoryItems--;
        }
    }

    public void AddItem(InventoryItem inventoryItem, InventorySlot inventorySlot, int count = 1)
    {
        Stack createdStack = Instantiate(_stackPrefab);
        createdStack.Init(inventoryItem);

        createdStack.StackMovement.DraggingStopped += DraggedStoppedHandler;
        createdStack.StackMovement.StackClicked += StackClickedHandler;

        _inventoryStacks.Add(createdStack);
        inventorySlot.AddItem(createdStack);

        createdStack.SetCount(count);
    }

    public void RemoveItem(InventoryItem inventoryItem)
    {
        Stack stack;

        switch (inventoryItem)
        {
            case Bullets:
                Bullets bullets = inventoryItem as Bullets;

                foreach (var item in _inventoryStacks)
                {
                    if (item.InventoryItem is not null)
                    {
                        Bullets temp = item.InventoryItem as Bullets;

                        if (temp is not null && temp.WeaponType == bullets.WeaponType)
                        {
                            item.SetCountToZero();
                        }
                    }
                }
                break;
            default:
                stack = _inventoryStacks.LastOrDefault(x => x.InventoryItem.GetType() == inventoryItem.GetType());
                stack.RemoveItem();

                if (stack.IsEmpty())
                {
                    foreach (InventorySlot inventorySlot in _inventorySlots)
                    {
                        bool hasStackInSlot = stack == inventorySlot.GetStack();
                        bool isNotParent = stack.transform.parent != inventorySlot.transform.parent;

                        if (hasStackInSlot && isNotParent)
                        {
                            inventorySlot.RemoveStack();
                        }
                    }

                    _inventoryStacks.Remove(stack);
                    Destroy(stack.gameObject);
                }
                break;
        }
    }

    public void AddRandomItem()
    {
        InventoryItem randomItem = _allItems[Random.Range(0, _allItems.Count)];

        Stack stack = _inventoryStacks
            .Where(x => x.InventoryItem.GetType() == randomItem.GetType())
            .FirstOrDefault(x => !x.IsStackFull());

        Debug.Log(stack == null);

        if (stack == null)
        {
            Stack createdRandomStack = Instantiate(_stackPrefab);
            createdRandomStack.Init(randomItem);

            createdRandomStack.StackMovement.DraggingStopped += DraggedStoppedHandler;
            createdRandomStack.StackMovement.StackClicked += StackClickedHandler;

            _inventoryStacks.Add(createdRandomStack);

            StartCoroutine(AddItemToRandomSlot(createdRandomStack));
        }
        else
        {
            stack.AddItem();
        }
    }

    public bool HasBullets(WeaponData weaponType)
    {
        foreach (Stack stack in _inventoryStacks)
        {
            if (stack.InventoryItem.GetType() == typeof(Bullets))
            {
                if ((stack.InventoryItem as Bullets).WeaponType == weaponType)
                {
                    return !stack.IsEmpty();
                }
            }
        }

        return false;
    }

    public void RemoveBullet(WeaponData weaponType, int count)
    {
        foreach (Stack stack in _inventoryStacks)
        {
            if (stack.InventoryItem.GetType() == typeof(Bullets))
            {
                if ((stack.InventoryItem as Bullets).WeaponType == weaponType)
                {
                    stack.RemoveItem(count);
                }
            }
        }
    }

    public void FillBullets(InventoryItem inventoryItem)
    {
        Bullets bullets = inventoryItem as Bullets;

        foreach (Stack stack in _inventoryStacks)
        {
            if (stack.InventoryItem.GetType() == typeof(Bullets))
            {
                if ((stack.InventoryItem as Bullets).WeaponType == bullets.WeaponType)
                {
                    stack.FillToMaxCount();
                }
            }
        }
    }

    public Stack GetStack(InventoryItem inventoryItem)
    {
        return _inventoryStacks.LastOrDefault(x => x.InventoryItem.GetType() == inventoryItem.GetType());
    }

    public void Swap(InventoryItem from, InventoryItem to)
    {
        Stack stack = GetStack(from);
        InventorySlot inventorySlot = _inventorySlots.FirstOrDefault(x => x.GetStack() == stack);

        RemoveItem(from);
        AddItem(to, inventorySlot);
    }

    public void SetSlots(InventoryData inventoryData)
    {
        foreach (var item in _inventorySlots)
        {
            item.RemoveStack();
        }

        for (int i = 0; i < _inventoryStacks.Count; i++)
        {
            Destroy(_inventoryStacks[i].gameObject);
        }

        _inventoryStacks.Clear();

        List<StackData> stackData = inventoryData.StackData.ToList();

        for (int i = 0; i < stackData.Count; i++)
        {
            Debug.Log($"InventoryItem: {stackData[i].InventoryItem.Sprite.name}");
            Debug.Log($"IndexOfSlot: {stackData[i].IndexOfSlot}");
            Debug.Log($"Count: {stackData[i].Count}");

            AddItem(stackData[i].InventoryItem, _inventorySlots[stackData[i].IndexOfSlot], stackData[i].Count);
        }

        _isStackLoaded = true;
    }

    public List<StackData> GetStackData()
    {
        List<StackData> stackData = new();

        for (int i = 0; i < _inventoryStacks.Count; i++)
        {
            InventoryItem inventoryItem = _inventoryStacks[i].InventoryItem;
            int count = _inventoryStacks[i].GetCount();
            int indexOfSlot = _inventorySlots.FindIndex(x => x.GetStack() == _inventoryStacks[i]);

            StackData stack = new StackData(inventoryItem, count, indexOfSlot);
            stackData.Add(stack);
        }

        return stackData;
    }
}
