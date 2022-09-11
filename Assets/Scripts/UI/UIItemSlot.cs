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

    public bool DestroyOnEmpty;
    public bool IsPlayerSlot;
    private bool _IsWanted;

    private Coroutine CurrentTooltipWaitCoroutine;

    public bool IsWanted
    {
        get
        {
            return _IsWanted;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return item == null;
        }
    }

    public void SetItem(Item item/*, int count*/)
    {
        this.item = item;
        //this.count = count;

        if(this.item == null && DestroyOnEmpty)
        {
            Destroy(gameObject);
            return;
        }

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
        ItemCountText.text = "";

        /*
        if(count == 1)
        {
            ItemCountText.text = "";
        }
        else
        {
            ItemCountText.text = count.ToString();
        }*/

    }

    public void OnSlotClicked()
    {
        if (GameManager.Instance._MainUI._TradingUI._TradingState == TradingUI.TradingState.InitialOffer)
        {
            if (GameManager.Instance._MainUI._TradingUI.CurrentNPC.IsSelling)
            {
                if (!IsPlayerSlot)
                {
                    SetIsWanted(!_IsWanted);
                }
            }
        }
    }

    public void MouseDragBegin()
    {
        if (IsPlayerSlot && item != null)
        {
            GameManager.Instance._MainUI._Tooltip.gameObject.SetActive(false);
            GameManager.Instance._MainUI.StartItemDrag(this);
        }
    }

    public void MouseEnter()
    {
        GameManager.Instance._MainUI.CurrentHoveredItemSlot = this;

        if(!IsEmpty)
        {
            CurrentTooltipWaitCoroutine = StartCoroutine(TooltipWaitCoroutine());
        }
    }

    public void MouseExit()
    {
        GameManager.Instance._MainUI.CurrentHoveredItemSlot = null;

        if (CurrentTooltipWaitCoroutine != null)
        {
            StopCoroutine(CurrentTooltipWaitCoroutine);
        }

        GameManager.Instance._MainUI._Tooltip.gameObject.SetActive(false);
    }

    public void MouseUp() 
    {
        GameManager.Instance._MainUI.OnMouseUp();
    }

    private IEnumerator TooltipWaitCoroutine()
    {
        yield return new WaitForSeconds(MainUI.TooltipHoverTime);

        if (GameManager.Instance._MainUI.CurrentItemDragWidget == null)
        {
            GameManager.Instance._MainUI._Tooltip.ReflectItem(item);
            GameManager.Instance._MainUI._Tooltip.gameObject.SetActive(true);
        }
    }
}
