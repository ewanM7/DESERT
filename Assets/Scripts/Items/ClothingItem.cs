using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingItem : Item
{
    public int DyeValue;

    /// <summary>
    /// When creating a clothing item, the material of the item MUST be the first dynamic descriptor
    /// </summary>
    /// <param name="data"></param>
    /// <param name="dynamicDescriptors"></param>
    public ClothingItem(BaseItemData data, ItemDescriptor[] dynamicDescriptors, Item[] dyes, int quality) : base(data, quality, 0)
    {
        DynamicDescriptors = dynamicDescriptors;
        DyeValue = 0;

        if (dyes != null)
        {
            foreach (Item dye in dyes)
            {
                DyeValue += dye.Value;
            }
        }

    }

    public override string Name
    {
        get
        {
            if (DynamicDescriptors.Length == 1)
            {
                return DynamicDescriptors[0].ToString() + " " + BaseItemData.BaseName;
            }
            else
            {
                return DynamicDescriptors[0].ToString() + " " + BaseItemData.BaseName + " (" + DynamicDescriptors[1] + ")";
            }
        }
    }

    public override int Value
    {
        get
        {
            return Mathf.RoundToInt((BaseItemData.BaseValue + DyeValue) * BaseItemData.MultiplierForMaterial(DynamicDescriptors[0]) * GameManager.Instance._ItemDatabase.QualityValueCurve.Evaluate(Quality));
        }
    }
}
