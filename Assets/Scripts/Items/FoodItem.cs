using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : Item
{
    /// <summary>
    /// When creating a food item, the main ingredient of the item MUST be the first dynamic descriptor
    /// </summary>
    /// <param name="data"></param>
    /// <param name="dynamicDescriptors"></param>
    public FoodItem(BaseItemData data, ItemDescriptor[] dynamicDescriptors) : base(data)
    {
        DynamicDescriptors = dynamicDescriptors;
    }

    public override string Name
    {
        get
        {
            if(DynamicDescriptors == null)
            {
                return BaseItemData.BaseName;
            }
            else if (DynamicDescriptors.Length == 1)
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
            return Mathf.RoundToInt((BaseItemData.BaseValue) * BaseItemData.MultiplierForMaterial(DynamicDescriptors[0]) * GameManager.Instance._ItemDatabase.QualityValueCurve.Evaluate(Quality));
        }
    }
}
