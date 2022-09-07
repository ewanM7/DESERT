using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TradingUI : MonoBehaviour
{
    public NPC CurrentNPC;

    public TradeOffer CurrentPlayerTradeOffer;

    public const int TRADING_SLOTS_COUNT = 9;

    public UIItemSlot[] PlayerItemSlots;
    public UIItemSlot[] NPCItemSlots;

    public GameObject PlayerPriceWidget;
    public TextMeshProUGUI PlayerPriceText;

    public GameObject NPCPriceWidget;
    public TextMeshProUGUI NPCPriceText;

    public GameObject DeclineTradeButton;
    public GameObject AcceptTradeButton;
    public GameObject OfferTradeButton;

    private void OnEnable()
    {
        if(CurrentNPC != null)
        {
            if(CurrentNPC.IsSelling)
            {
                PlayerPriceWidget.SetActive(false);
                NPCPriceWidget.SetActive(true);

                DeclineTradeButton.SetActive(false);
                AcceptTradeButton.SetActive(false);
                OfferTradeButton.SetActive(true);
            }
            else
            {
                PlayerPriceText.text = "0";
                PlayerPriceWidget.SetActive(true);
                NPCPriceWidget.SetActive(false);

                DeclineTradeButton.SetActive(true);
                AcceptTradeButton.SetActive(true);
                OfferTradeButton.SetActive(false);
            }

            ReflectCurrentTradeOffer();
        }
    }

    /// <summary>
    /// Set the current player trade offer using the contents of the trading window
    /// </summary>
    private void SetPlayerTradeOffer()
    {
        CurrentPlayerTradeOffer = new TradeOffer();

        for(int i = 0; i < TRADING_SLOTS_COUNT; i++)
        {
            CurrentPlayerTradeOffer.Items[i] = PlayerItemSlots[i].item;
        }

        if(!CurrentNPC.IsSelling)
        {
            //player is buying, so set trade wanted value
            CurrentPlayerTradeOffer.WantedValue = int.Parse(PlayerPriceText.text);
        }
        else
        {
            CurrentPlayerTradeOffer.WantedValue = -1;
        }

        CurrentPlayerTradeOffer.Accepted = false;

        for(int i = 0; i < TRADING_SLOTS_COUNT; i++)
        {
            CurrentPlayerTradeOffer.WantedItemIndexes[i] = NPCItemSlots[i].IsWanted;
        }

    }

    public void ReflectCurrentTradeOffer()
    {

    }

    public void OnAcceptButton()
    {
        SetPlayerTradeOffer();
        CurrentPlayerTradeOffer.Accepted = true;
    }

    public void OnDeclineButton()
    {

    }

    public void OnOfferButton()
    {

    }

    public void OnPriceIncreaseSmallButton()
    {

    }

    public void OnPriceIncreaseMediumButton()
    {

    }

    public void OnPriceIncreaseLargeButton()
    {

    }

    public void OnPriceDecreaseSmallButton()
    {

    }

    public void OnPriceDecreaseMediumButton()
    {

    }

    public void OnPriceDecreaseLargeButton()
    {

    }
}
