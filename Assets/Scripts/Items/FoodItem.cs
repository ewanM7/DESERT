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
            else
            {
                return BaseItemData.BaseName + " " + DynamicDescriptors[0].ToString();
            }
        }
    }


    public override int Value
    {
        get
        {
            return Mathf.RoundToInt(BaseItemData.BaseValue * GameManager.Instance._ItemDatabase.QualityValueCurve.Evaluate(Quality));
        }
    }
}
