using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Item
{
    public int Quality;
    public BaseItemData BaseItemData;

    public ItemDescriptor[] DynamicDescriptors;

    public const float DAMAGED_MULTIPLIER = 0.5f;

    /// <summary>
    /// Used for value when the item is cash. This is -1 if the item is not cash
    /// </summary>
    public int CashValue;

    public System.Guid UniqueID;

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
            if(DynamicDescriptors == null)
            {
                return BaseItemData.BaseDescriptors;
            }
            else
            {
                return BaseItemData.BaseDescriptors.Concat(DynamicDescriptors).ToArray();
            }
        }
    }

    public Item(BaseItemData data, int quality, int dummyParam)
    {
        UniqueID = System.Guid.NewGuid();
        BaseItemData = data;
        Quality = quality;
        CashValue = -1;
    }

    /// <summary>
    /// Constructor used to create cash items
    /// </summary>
    /// <param name="data"></param>
    /// <param name="CashValue"></param>
    public Item(BaseItemData data, int CashValue)
    {
        UniqueID = System.Guid.NewGuid();
        BaseItemData = data;
        Quality = -1;
        this.CashValue = CashValue;
    }

    /// <summary>
    /// The value of the item, taking quality into account
    /// </summary>
    public virtual int Value
    {
        get
        {
            if(CashValue != -1)
            {
                return CashValue;
            }

            if(BaseItemData == null)
            {
                return 0;
            }

            int value = Mathf.RoundToInt(GameManager.Instance._ItemDatabase.QualityValueCurve.Evaluate(Quality) * BaseItemData.BaseValue);

            if (HasDescriptor(ItemDescriptor.Damaged))
            {
                return Mathf.RoundToInt(value * DAMAGED_MULTIPLIER);
            }

            return value;
        }
    }

    public static bool operator ==(Item a, Item b)
    {
        return a.UniqueID == b.UniqueID;
    }

    public static bool operator !=(Item a, Item b)
    {
        return a.UniqueID != b.UniqueID;
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
            if (Quality < 1)
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
