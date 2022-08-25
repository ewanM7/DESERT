using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TradingUI : MonoBehaviour
{
    public const int TradingSlotsCount = 9;

    public UIItemSlot[] PlayerItemSlots;
    public UIItemSlot[] NPCItemSlots;

    public TextMeshProUGUI PlayerTotalValueText;
    public TextMeshProUGUI NPCTotalValueText;


}
