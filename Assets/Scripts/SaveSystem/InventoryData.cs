using System;
using System.Collections.Generic;

[Serializable]
public class InventoryData
{
    public StackData[] StackData;

    public InventoryData(List<StackData> stackData)
    {
        StackData = stackData.ToArray();
    }
}
