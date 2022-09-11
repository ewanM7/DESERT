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

    public const float TooltipHoverTime = 0.8f;

    public Tooltip _Tooltip;

    // Update is called once per frame
    void Update()
    {
        TimeOfDayText.text = GameManager.Instance.TimeOfDay.ToString("F1");
    }

    public void StartItemDrag(UIItemSlot draggedFrom)
    {
        CurrentDraggedItemSlot = draggedFrom;

        CurrentItemDragWidget = Instantiate(ItemDragWidgetPrefab, transform).GetComponent<ItemDragWidget>();
        CurrentItemDragWidget.ReflectItem(draggedFrom.item);
    }

    public void OnMouseUp()
    {
        if (CurrentItemDragWidget != null)
        {
            if (CurrentHoveredItemSlot != null)
            {
                //move item into slot if its empty, otherwise let go
                if(CurrentHoveredItemSlot.IsPlayerSlot && CurrentHoveredItemSlot.IsEmpty)
                {
                    CurrentHoveredItemSlot.SetItem(CurrentDraggedItemSlot.item);
                    CurrentDraggedItemSlot.SetItem(null);
                }
            }

            Destroy(CurrentItemDragWidget.gameObject);
        }
    }
}
