using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemQuality Quality;
    public ItemData itemData;

    public Item(ItemData data)
    {
        itemData = data;
    }

    public static ItemQuality QualityForRoll(int roll)
    {
        if(roll <= 4)
        {
            return ItemQuality.OneStar;
        }
        else if(roll >= 5 && roll <= 8)
        {
            return ItemQuality.TwoStar;
        }
        else if(roll >= 9 && roll <= 12)
        {
            return ItemQuality.ThreeStar;
        }
        else if(roll >= 13 && roll <= 16)
        {
            return ItemQuality.FourStar;
        }
        else
        {
            return ItemQuality.FiveStar;
        }
    }
}
