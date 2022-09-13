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

    public GameObject AcceptButton;
    public GameObject OfferButton;
    public TextMeshProUGUI OfferButtonText;

    public RectTransform PlayerInventoryContentRT;
    public UIItemSlot ItemSlotPrefab;

    public Dialogue NPCWantsMoreDialogue;
    public Dialogue NPCAcceptDialogue;
    public Dialogue NPCInitialBuyDialogue;
    public Dialogue NPCInitialSellDialogue;

    public DialogueBox _DialogueBox;

    public TradingState _TradingState;

    public enum TradingState
    {
        InitialOffer,
        MainTrading,
        TradeClosed
    }

    private void OnEnable()
    {
        if(CurrentNPC != null)
        {
            _TradingState = TradingState.InitialOffer;

            PlayerPriceText.text = "0";
            NPCPriceText.text = "0";
            PlayerPriceWidget.SetActive(false);
            NPCPriceWidget.SetActive(false);

            AcceptButton.SetActive(false);
            OfferButton.SetActive(true);

            ReflectNPCTradeOffer();
            ReflectPlayerInventory();
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

            if(PlayerItemSlots[i].IsWanted)
            {
                CurrentPlayerTradeOffer.WantedItemIndexes[i] = true;
            }
            else
            {
                CurrentPlayerTradeOffer.WantedItemIndexes[i] = false;
            }
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

    public void ReflectNPCTradeOffer()
    {
        TradeOffer npcTradeOffer = CurrentNPC.CurrentTradeOffer;

        if (npcTradeOffer != null)
        {
            for(int i = 0; i < npcTradeOffer.Items.Length; i++)
            {
                NPCItemSlots[i].SetItem(npcTradeOffer.Items[i]);
            }

            NPCPriceText.text = npcTradeOffer.WantedValue.ToString();
        }
    }

    public void ReflectPlayerInventory()
    {

        int i = 1;

        UIItemSlot cashSlot = Instantiate(ItemSlotPrefab, PlayerInventoryContentRT).GetComponent<UIItemSlot>();
        cashSlot.SetItem(new Item(GameManager.Instance._ItemDatabase.Cash, GameManager.Instance._Player.Money));
        i++;

        foreach (ItemStack stack in GameManager.Instance._Player.inventory.ItemStacks)
        {
            if(stack.item != null)
            {
                UIItemSlot slot = Instantiate(ItemSlotPrefab, PlayerInventoryContentRT).GetComponent<UIItemSlot>();
                slot.SetItem(stack.item);
                slot.DestroyOnEmpty = true;
                i++;
            }
        }

        foreach(ItemStack stack in GameManager.Instance._House._Inventory.ItemStacks)
        {
            if (stack.item != null)
            {
                UIItemSlot slot = Instantiate(ItemSlotPrefab, PlayerInventoryContentRT).GetComponent<UIItemSlot>();
                slot.SetItem(stack.item);
                slot.DestroyOnEmpty = true;
                i++;
            }
        }

        PlayerInventoryContentRT.sizeDelta = new Vector2(PlayerInventoryContentRT.sizeDelta.x , 15 + (i * 95));
    }

    public void OnAcceptButton()
    {

        if (_TradingState == TradingState.MainTrading)
        {
            CurrentPlayerTradeOffer.Accepted = true;
            _TradingState = TradingState.TradeClosed;
        }
    }

    public void OnOfferButton()
    {
        SetPlayerTradeOffer();
        CurrentNPC.SetResponseForTradeOffer(CurrentPlayerTradeOffer);


        if(CurrentNPC.CurrentTradeOffer.Accepted)
        {
            foreach (Item item in CurrentPlayerTradeOffer.Items)
            {
                GameManager.Instance._House._Inventory.RemoveItem(item);
            }

            foreach (Item item in CurrentNPC.CurrentTradeOffer.Items)
            {
                GameManager.Instance._House._Inventory.AddItem(item);
            }
        }
        else
        {
            ReflectNPCTradeOffer();
            _TradingState = TradingState.MainTrading;

            if (CurrentNPC.IsSelling)
            {
                OfferButtonText.text = "Offer";

                NPCPriceWidget.SetActive(true);
                NPCPriceText.text = CurrentNPC.CurrentTradeOffer.WantedValue.ToString();
            }
            else
            {
                OfferButtonText.text = "Counter-offer";
                PlayerPriceWidget.SetActive(true);
                AcceptButton.SetActive(true);
            }
        }
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
