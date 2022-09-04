using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TradingUI : MonoBehaviour
{
    public NPC CurrentNPC;

    public const int TRADING_SLOTS_COUNT = 9;

    public UIItemSlot[] PlayerItemSlots;
    public UIItemSlot[] NPCItemSlots;

    public GameObject PlayerPriceWidget;
    public TextMeshProUGUI PlayerPriceText;

    public GameObject NPCPriceWidget;
    public TextMeshProUGUI NPCPriceText;

    private void OnEnable()
    {
        if(CurrentNPC != null)
        {
            if(CurrentNPC.IsSelling)
            {
                PlayerPriceWidget.SetActive(false);
                NPCPriceWidget.SetActive(true);
            }
            else
            {
                PlayerPriceText.text = "0";
                PlayerPriceWidget.SetActive(true);
                NPCPriceWidget.SetActive(false);
            }

            
        }
    }
}
