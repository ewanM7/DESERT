using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    public Item item;
    public int count;

    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemCountText;


    public void SetItem(Item item, int count)
    {
        this.item = item;
        this.count = count;

        ReflectItem();
    }

    public void ClearItem()
    {
        item = null;
        count = 0;

        ReflectItem();
    }

    public void ReflectItem()
    {
        if(item == null)
        {
            ItemNameText.text = "";
            ItemCountText.text = "";
            return;
        }

        ItemNameText.text = item.itemData.Name;

        if(count == 1)
        {
            ItemCountText.text = "";
        }
        else
        {
            ItemCountText.text = count.ToString();
        }
       
    }
}
