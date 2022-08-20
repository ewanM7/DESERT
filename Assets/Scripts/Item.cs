using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int Quality;
    public ItemData itemData;

    public Item(ItemData data)
    {
        itemData = data;
    }

    /// <summary>
    /// The value of the item when taking quality into account
    /// </summary>
    public int Value
    {
        get
        {
            return Mathf.RoundToInt(GameManager.Instance._ItemDatabase.QualityValueCurve.Evaluate(Quality) * itemData.BaseValue);
        }
    }

    public ItemQuality StarQuality()
    {
        if(Quality == -1)
        {
            return ItemQuality.None;
        }

        if(Quality <= 4)
        {
            return ItemQuality.OneStar;
        }
        else if(Quality >= 5 && Quality <= 8)
        {
            return ItemQuality.TwoStar;
        }
        else if(Quality >= 9 && Quality <= 12)
        {
            return ItemQuality.ThreeStar;
        }
        else if(Quality >= 13 && Quality <= 16)
        {
            return ItemQuality.FourStar;
        }
        else
        {
            return ItemQuality.FiveStar;
        }
    }
}
