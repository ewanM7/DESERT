using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Item
{
    public int Quality;
    public BaseItemData BaseItemData;

    public ItemDescriptor[] DynamicDescriptors;

    public virtual string Name
    {
        get
        {
            return BaseItemData.BaseName;
        }
    }

    public ItemDescriptor[] Descriptors
    {
        get
        {
            return BaseItemData.BaseDescriptors.Concat(DynamicDescriptors).ToArray();
        }
    }

    public Item(BaseItemData data)
    {
        BaseItemData = data;
    }

    /// <summary>
    /// The value of the item when taking quality into account
    /// </summary>
    public virtual int Value
    {
        get
        {
            return Mathf.RoundToInt(GameManager.Instance._ItemDatabase.QualityValueCurve.Evaluate(Quality) * BaseItemData.BaseValue);
        }
    }

    public bool HasDescriptor(ItemDescriptor descriptor)
    {
        return Descriptors.Contains(descriptor);
    }

    public bool HasDescriptors(ItemDescriptor[] descriptors)
    {
        foreach(ItemDescriptor descriptor in descriptors)
        {
            if (!Descriptors.Contains(descriptor))
            {
                return false;
            }
        }

        return true;
    }

    public ItemQuality StarQuality
    {
        get
        {
            if (Quality == -1)
            {
                return ItemQuality.None;
            }

            if (Quality <= 4)
            {
                return ItemQuality.OneStar;
            }
            else if (Quality >= 5 && Quality <= 8)
            {
                return ItemQuality.TwoStar;
            }
            else if (Quality >= 9 && Quality <= 12)
            {
                return ItemQuality.ThreeStar;
            }
            else if (Quality >= 13 && Quality <= 16)
            {
                return ItemQuality.FourStar;
            }
            else
            {
                return ItemQuality.FiveStar;
            }
        }
    }
}
