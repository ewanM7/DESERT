using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolItem : Item
{
    /// <summary>
    /// When creating a tool item, the material MUST be the first dynamic descriptor.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="dynamicDescriptors"></param>
    public ToolItem(BaseItemData data, ItemDescriptor[] dynamicDescriptors, int quality) : base(data, quality, 0)
    {
        DynamicDescriptors = dynamicDescriptors;
    }

    public override string Name
    {
        get
        {
            return DynamicDescriptors[0].ToString() + " " + BaseItemData.BaseName;
        }
    }

    public override int Value
    {
        get
        {
            return Mathf.RoundToInt((BaseItemData.BaseValue) * BaseItemData.MultiplierForMaterial(DynamicDescriptors[0]) * GameManager.Instance._ItemDatabase.QualityValueCurve.Evaluate(Quality));
        }
    }
}

