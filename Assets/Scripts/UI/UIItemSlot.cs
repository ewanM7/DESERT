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
    public Image BackgroundImage;

    public Color BackgroundBaseColor;
    public Color BackgroundWantedColor;


    public bool IsPlayerSlot;
    private bool _IsWanted;

    public bool IsWanted
    {
        get
        {
            return _IsWanted;
        }
    }

    public void SetItem(Item item, int count)
    {
        this.item = item;
        this.count = count;

        ReflectItem();
    }

    public void SetIsWanted(bool wanted)
    {
        _IsWanted = wanted;

        if(_IsWanted)
        {
            BackgroundImage.color = BackgroundWantedColor;
        }
        else
        {
            BackgroundImage.color = BackgroundBaseColor;
        }
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

        ItemNameText.text = item.Name;

        if(count == 1)
        {
            ItemCountText.text = "";
        }
        else
        {
            ItemCountText.text = count.ToString();
        }
       
    }

    public void OnSlotClicked()
    {
        if (IsPlayerSlot)
        {
            Debug.Log("Item slot clicked");
        }
    }
}
