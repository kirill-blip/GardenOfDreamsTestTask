using System;
using UnityEngine;

[Serializable]
public class IntReference
{
    [SerializeField] private bool UseConstant = true;
    [SerializeField] private int ConstantValue;
    [SerializeField] private IntVariable Variable;

    public int Value
    {
        get
        {
            return UseConstant ? ConstantValue : Variable.Value;
        }
    }
}
