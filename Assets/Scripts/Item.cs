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
}
