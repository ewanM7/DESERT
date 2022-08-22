using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class NPC : MonoBehaviour
{
    public Trait[] Traits;
    public Inventory inventory;
    public ItemSpecifier[] WantsToBuy;
    public ItemSpecifier[] WantsToSell;
    public bool IsSelling;
    private TradeOffer CurrentTradeOffer;

    /// <summary>
    /// Percentage of how much the npc is willing to offer over for items they're buying, or accept offers under for items they're selling
    /// </summary>
    public float PriceTolerance;

    /// <summary>
    /// NPC buy offers are multiplied by this value
    /// </summary>
    public float BuyMultiplier;

    /// <summary>
    /// This is the muliplier of how much an NPC increases its buy offer after a rejection
    /// </summary>
    public float OfferMultiplier;

    /// <summary>
    /// NPC sale prices are multiplied by this value
    /// </summary>
    public float SellMultiplier;

    public int FirstOfferValue;
    public int PreviousOfferValue;

    /// <summary>
    /// Interest in continuing the trade on a scale from 1 to 10 - trade ends if the interest drops to 0
    /// </summary>
    public int TradeInterest;


    public NPCState State;

    public GameObject SpeechCanvas;
    public TextMeshProUGUI SpeechText;

    public float SpeechCanvasHorizontalOffset;
    public Vector3 SpeechCanvasVerticalOffset;

    public void SetItems(Item[] items)
    {
        foreach(Item item in items)
        {
            inventory.AddItem(item);
        }
    }

    private void Start()
    {
        PreviousOfferValue = 0;
    }

    private void Update()
    {
        if(State == NPCState.Speaking)
        {
            //set world position and rotation of the speech bubble

            Vector3 FromCameraToNPC = SpeechCanvas.transform.position - GameManager.Instance._CameraManager.MainCamera.transform.position;
            FromCameraToNPC.y = 0;
            Vector3 Perpendicular = Vector3.Cross(FromCameraToNPC, Vector3.up);
            Perpendicular.Normalize();

            SpeechCanvas.transform.position = transform.position + SpeechCanvasVerticalOffset + (Perpendicular * SpeechCanvasHorizontalOffset);

            SpeechCanvas.transform.LookAt(GameManager.Instance._CameraManager.MainCamera.transform);
            SpeechCanvas.transform.Rotate(0, 180f, 0);


        }
    }

    public bool IsLookingForItem(Item item)
    {
        if(item == null)
        {
            return false;
        }

        foreach(ItemSpecifier itemSpecifier in WantsToBuy)
        {
            if(item.BaseItemData.Category != ItemCategory.None && item.BaseItemData.Category == itemSpecifier.Category)
            {
                return true;
            }

            if (item.BaseItemData.ID != ItemID.None && item.BaseItemData.ID == itemSpecifier.ID)
            {
                return true;
            }

            if (item.HasDescriptors(itemSpecifier.Descriptors))
            {
                return true;
            }
        }

        return false;
    }

    public void OnPlayerInteract()
    {

    }

    private int PerceivedValueForItem(Item item)
    {
        //to do - add multipliers for npc traits to the value
        float perceivedValueMultiplier = 1f;

        if(item.BaseItemData.ID == ItemID.Cash)
        {
            return 1;
        }

        if (Traits.Contains(Trait.Alcoholic) && item.HasDescriptor(ItemDescriptor.Alcohol))
        {
            //add to multiplier
        }

        if (Traits.Contains(Trait.SweetTooth) && item.HasDescriptor(ItemDescriptor.Sweet))
        {
            //add to multiplier
        }

        if (!IsLookingForItem(item))
        {
            perceivedValueMultiplier = 0f;
        }

        return Mathf.RoundToInt(item.Value * perceivedValueMultiplier);
    }

    private int PerceivedValueForOffer(TradeOffer offer)
    {
        int value = 0;

        foreach(Item item in offer.Items)
        {
            value += PerceivedValueForItem(item);
            
        }

        return value;
    }

    public TradeOffer ResponseForTradeOffer(TradeOffer PlayerOffer)
    {
        int perceivedOfferValue = PerceivedValueForOffer(PlayerOffer);

        if(CurrentTradeOffer == null)
        {
            CurrentTradeOffer = new TradeOffer();
        }

        //to do - affect NPC interest based on the difference between the offer and the wanted price. interest should also decline when offered items they aren't looking for

        if (IsSelling)
        {
            //npc is selling to the player
            for(int i = 0; i < CurrentTradeOffer.Items.Length; i++)
            {
                //discard items that the player is not interested in buying
                if (CurrentTradeOffer.Items[i] != null && !PlayerOffer.WantedItemIndexes[i])
                {
                    CurrentTradeOffer.Items[i] = null;
                }
            }

            CurrentTradeOffer.WantedValue = 0;

            foreach(Item item in CurrentTradeOffer.Items)
            {
                if (item != null)
                {
                    CurrentTradeOffer.WantedValue += item.Value;
                }
            }

            CurrentTradeOffer.WantedValue = Mathf.RoundToInt(CurrentTradeOffer.WantedValue * SellMultiplier);

            if (perceivedOfferValue >= CurrentTradeOffer.WantedValue * (1 - PriceTolerance))
            {
                //player offer is above or equal to the wanted value including the price tolerance - accept the trade
                CurrentTradeOffer.Accepted = true;
            }
        }
        else
        {
            //npc is buying from the player
            int z = 0;
            bool makeOffer = true;

            foreach(Item item in PlayerOffer.Items)
            {
                if(item != null)
                {
                    if (IsLookingForItem(item))
                    {
                        CurrentTradeOffer.WantedItemIndexes[z] = true;

                        //to do - take traits into account here to check if they want more of the same item, or more of similar items. NPCs should also have a chance to want multiple of the same item, depening on which item it is
                    }
                    else
                    {
                        makeOffer = false;
                    }
                }

                z++;
            }

            if (makeOffer)
            {
                if (PlayerOffer.WantedValue <= perceivedOfferValue)
                {
                    //value the player wants is below or equal to the perceived value - make an offer at or below the wanted value
                    if(PreviousOfferValue == 0)
                    {
                        //first offer, start lowest
                        CurrentTradeOffer.Items[0] = new Item(GameManager.Instance._ItemDatabase.Cash, Mathf.Clamp(Mathf.RoundToInt(PlayerOffer.WantedValue * Random.Range(0.7f, 0.9f) * BuyMultiplier), 0, PlayerOffer.WantedValue));

                        //to do - include items in the offer from the NPCs inventory


                        foreach (Item item in CurrentTradeOffer.Items)
                        {
                            if (item != null)
                            {
                                PreviousOfferValue += item.CashValue;
                            }
                        }
                    }
                    else
                    {
                        //player wants more value - new offer should be higher than the previous, or close the trade
                        PreviousOfferValue = Mathf.Clamp(Mathf.RoundToInt(PreviousOfferValue * OfferMultiplier), 0, PlayerOffer.WantedValue);
                        CurrentTradeOffer.Items[0] = new Item(GameManager.Instance._ItemDatabase.Cash, PreviousOfferValue);

                        
                        //to do - remove items from the offer the player is not interested in, and increase cash value appropriately

                    }
                }
                else
                {
                    //the player wants more than the perceived value - offer around the perceived value up the the price tolerance max

                    if(PreviousOfferValue == 0)
                    {
                        //first offer, start lowest
                        CurrentTradeOffer.Items[0] = new Item(GameManager.Instance._ItemDatabase.Cash, Mathf.Clamp(Mathf.RoundToInt(perceivedOfferValue * BuyMultiplier), 0, PlayerOffer.WantedValue));

                        //to do - include items in the offer from the NPCs inventory

                        foreach (Item item in CurrentTradeOffer.Items)
                        {
                            if (item != null)
                            {
                                PreviousOfferValue += item.CashValue;
                            }
                        }
                    }
                    else
                    {
                        //player wants more value - new offer should be higher than the previous, up to the price tolerance or the player's wanted value. or close the trade
                        PreviousOfferValue = (int)Mathf.Clamp(PreviousOfferValue * OfferMultiplier, 0, Mathf.Min((1 + PriceTolerance) * FirstOfferValue, PlayerOffer.WantedValue));
                        CurrentTradeOffer.Items[0] = new Item(GameManager.Instance._ItemDatabase.Cash, PreviousOfferValue);
                    }
                }
            }
        }



        return CurrentTradeOffer;
    }

}

public class TradeOffer
{
    public TradeOffer()
    {
        Items = new Item[9];
        WantedValue = 0;
        Accepted = false;
        WantedItemIndexes = new bool[] { false, false, false, false, false, false, false, false, false };
    }

    public Item[] Items;
    public int WantedValue;
    public bool Accepted;

    public bool[] WantedItemIndexes;
}

public struct ItemSpecifier
{
    public ItemID ID;
    public ItemDescriptor[] Descriptors;
    public ItemCategory Category;
}

public enum Trait
{
    Alcoholic,
    SweetTooth,
    Moral,
    Shady,
    BulkBuyer,
    Collector,
    Haggler,
    Impatient,
    Trusting,
    AnimalLover,
    Gambler,
    HighStandards,
    Modest,
    Vain,
    Cynical,
    Naive,
    LocalTrader,
    Tourist,
}

public enum NPCState
{
    Traveling,
    Trading,
    ClosingTrade,
    Speaking,
}