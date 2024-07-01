using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Stack _stackPrefab;
    [SerializeField] private List<InventoryItem> _allItems;

    [SerializeField] private PopupPanel _popupPanel;

    [SerializeField] private List<Stack> _inventoryStacks;
    private List<InventorySlot> _inventorySlots;

    private void Start()
    {
        //_inventoryStacks = FindObjectsOfType<Stack>().ToList();
        _inventorySlots = FindObjectsOfType<InventorySlot>().ToList();

        foreach (Stack stack in _inventoryStacks)
        {
            stack.StackMovement.DraggingStopped += DraggedStoppedHandler;
            stack.StackMovement.StackClicked += StackClickedHandler;
        }

        StartCoroutine(AddItemsToRandomSlots());
    }

    private void StackClickedHandler(Stack stack)
    {
        _popupPanel.ActivatePanel(stack.InventoryItem);
    }

    private void DraggedStoppedHandler(Stack stack)
    {
        foreach (InventorySlot inventorySlot in _inventorySlots)
        {
            bool hasStackInSlot = stack.transform.parent == inventorySlot.GetStack();
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

    public void RemoveItem(InventoryItem inventoryItem)
    {
        Stack stack;
        switch (inventoryItem)
        {
            case AidKit:
                stack = _inventoryStacks.LastOrDefault(x => x.InventoryItem.GetType() == inventoryItem.GetType());
                stack.RemoveItem();

                if (stack.IsEmpty())
                {
                    foreach (InventorySlot inventorySlot in _inventorySlots)
                    {
                        inventorySlot.RemoveStack();
                    }

                    Destroy(stack.gameObject);

                    _inventoryStacks.Remove(stack);
                }
                break;
            case Bullets:
                Bullets bullets = inventoryItem as Bullets;
                stack = _inventoryStacks.Find(x => (x.InventoryItem as Bullets).WeaponType == bullets.WeaponType);
                stack.SetCountToZero();
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

    public bool HasBullets(WeaponType weaponType)
    {
        Stack stack = _inventoryStacks.Find(x => (x.InventoryItem as Bullets).WeaponType == weaponType);
        return !stack.IsEmpty();
    }

    public void RemoveBullet(WeaponType weaponType, int count)
    {
        Stack stack = _inventoryStacks.Find(x => (x.InventoryItem as Bullets).WeaponType == weaponType);
        stack.RemoveItem(count);
    }

    public void FillBullets(InventoryItem inventoryItem)
    {
        Bullets bullets = inventoryItem as Bullets;
        Stack stack = _inventoryStacks.Find(x => (x.InventoryItem as Bullets).WeaponType == bullets.WeaponType);
        stack.FillToMaxCount();
    }
}
