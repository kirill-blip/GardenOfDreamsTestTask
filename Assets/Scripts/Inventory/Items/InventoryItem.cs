using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public InventoryItemData InventoryItemData;

    [HideInInspector]
    public Sprite Sprite => InventoryItemData.Sprite;
    
    [HideInInspector]
    public float Weight => InventoryItemData.Weight;

    [HideInInspector]
    public int Count => InventoryItemData.Count;

    [HideInInspector]
    public int MaxCount => InventoryItemData.MaxCount;
}
