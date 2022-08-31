using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelryItem : Item
{
    public int GemValue;

    /// <summary>
    /// When creating a jewelry item, the material of the item MUST be the first dynamic descriptor
    /// </summary>
    /// <param name="data"></param>
    /// <param name="dynamicDescriptors"></param>
    public JewelryItem(BaseItemData data, ItemDescriptor[] dynamicDescriptors, Item[] gems, int quality) : base(data, quality, 0)
    {
        DynamicDescriptors = dynamicDescriptors;
        GemValue = 0;

        if (gems != null)
        {
            foreach(Item gem in gems)
            {
                GemValue += gem.Value;
            }
        }
        
    }

    public override string Name
    {
        get
        {
            if(DynamicDescriptors[1] == ItemDescriptor.NoGem)
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
            return Mathf.RoundToInt((BaseItemData.BaseValue + GemValue) * BaseItemData.MultiplierForMaterial(DynamicDescriptors[0]) * GameManager.Instance._ItemDatabase.QualityValueCurve.Evaluate(Quality));
        }
    }
}
