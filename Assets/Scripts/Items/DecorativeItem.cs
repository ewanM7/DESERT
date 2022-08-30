using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativeItem : Item
{
    public int DyeValue;

    /// <summary>
    /// When creating a decorative item, the material MUST be the first dynamic descriptor. the dye is the second descriptor. For paintings, the size is the first descriptor.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="dynamicDescriptors"></param>
    public DecorativeItem(BaseItemData data, ItemDescriptor[] dynamicDescriptors, Item[] dyes) : base(data)
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
            if(BaseItemData.BaseDescriptors[0] == ItemDescriptor.Painting)
            {
                switch (DynamicDescriptors[0])
                {
                    case ItemDescriptor.LargePainting:
                        return "Large Painting";

                    case ItemDescriptor.MediumPainting:
                        return "Painting";

                    case ItemDescriptor.SmallPainting:
                        return "Small Painting";
                }
            }

            if (DynamicDescriptors == null)
            {
                return BaseItemData.BaseName;
            }
            else if(DyeValue > 0)
            {
                return DynamicDescriptors[0].ToString() + " " + BaseItemData.BaseName + " " + DynamicDescriptors[1].ToString();
            }
            else
            {
                return DynamicDescriptors[0].ToString() + " " + BaseItemData.BaseName;
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
