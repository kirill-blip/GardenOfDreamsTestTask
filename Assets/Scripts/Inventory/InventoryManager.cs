using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<InventoryItem> _inventoryItems;
    private List<InventorySlot> _inventorySlot;

    private void Start()
    {
        _inventoryItems = FindObjectsOfType<InventoryItem>().ToList();
        _inventorySlot = FindObjectsOfType<InventorySlot>().ToList();

        foreach (InventoryItem inventoryItem in _inventoryItems)
        {
            inventoryItem.DraggedStopped += DraggedStoppedHandler;
        }

        StartCoroutine(AddItemsToRandomSlots());
    }

    private void DraggedStoppedHandler(InventoryItem inventoryItem)
    {
        foreach (InventorySlot inventorySlot in _inventorySlot)
        {
            inventorySlot.RemoveItem(inventoryItem);
        }
    }

    private IEnumerator AddItemsToRandomSlots()
    {
        int countOfInventoryItems = _inventoryItems.Count;

        while (countOfInventoryItems > 0)
        {
            InventorySlot inventorySlot = _inventorySlot[Random.Range(0, _inventorySlot.Count)];

            if (inventorySlot.HasItem())
            {
                yield return null;
                continue;
            }

            inventorySlot.AddItem(_inventoryItems[countOfInventoryItems - 1]);
            countOfInventoryItems--;
        }
    }

    public void RemoveItem()
    {

    }
}
