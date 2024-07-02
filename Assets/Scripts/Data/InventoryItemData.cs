using UnityEngine;

[CreateAssetMenu()]
public class InventoryItemData : ScriptableObject
{
    public string Name;

    [Space(10f)]
    public Sprite Sprite;

    [Space(10f)]
    public float Weight;

    [Space(10f)]
    public int Count;
    public int MaxCount;
}
