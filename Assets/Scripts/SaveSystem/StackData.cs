using System;

[Serializable]
public class StackData
{
    public InventoryItem InventoryItem;
    public int Count;
    public int IndexOfSlot;

    public StackData(InventoryItem inventoryItem, int count, int indexOfSlot)
    {
        InventoryItem = inventoryItem;
        Count = count;
        IndexOfSlot = indexOfSlot;
    }
}
