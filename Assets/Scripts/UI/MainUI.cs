using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI DayCountText;
    public TextMeshProUGUI WeekdayText;
    public TextMeshProUGUI TimeOfDayText;

    public TradingUI _TradingUI;

    public GameObject ItemDragWidgetPrefab;
    public ItemDragWidget CurrentItemDragWidget;

    public UIItemSlot CurrentHoveredItemSlot;
    public UIItemSlot CurrentDraggedItemSlot;

    // Update is called once per frame
    void Update()
    {
        TimeOfDayText.text = GameManager.Instance.TimeOfDay.ToString("F1");
    }

    public void StartItemDrag(UIItemSlot draggedFrom)
    {
        CurrentDraggedItemSlot = draggedFrom;

        CurrentItemDragWidget = Instantiate(ItemDragWidgetPrefab).GetComponent<ItemDragWidget>();
    }

    public void OnMouseUp()
    {
        if (CurrentItemDragWidget != null)
        {
            if (CurrentHoveredItemSlot != null)
            {
                //move item into slot if its empty, otherwise let go
                if(CurrentHoveredItemSlot.IsEmpty)
                {
                    CurrentHoveredItemSlot.SetItem(CurrentDraggedItemSlot.item);
                    CurrentDraggedItemSlot.SetItem(null);
                }
                else
                {
                    Destroy(CurrentItemDragWidget);
                }
            }
            else
            {
                
            }
        }
    }
}
