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
    public bool IsSelling;
    public SellerType SalesType;
    private TradeOffer CurrentTradeOffer;

    public int MaxNumberOfBuyItems;

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

        //set price multipliers
        PriceTolerance = Random.Range(0f, 0.25f);
        BuyMultiplier = Random.Range(0.8f, 1.2f);
        SellMultiplier = Random.Range(0.8f, 1.2f);

        //set max number of items the npc will buy
        MaxNumberOfBuyItems = GameManager.Instance._NPCGenerationData.GetMaxBuyItemNumber();

        //set traits
        int traitCount = Random.Range(1, 5);

        Traits = new Trait[traitCount];

        for(int i = 0; i < traitCount; i++)
        {
            Trait newTrait = (Trait)Random.Range(1, (int)Trait.TRAIT_MAX);
            bool addTrait = true;

            //make sure mutually exclusive traits aren't added
            foreach(Trait trait in Traits)
            {
                if (trait.IsMutuallyExclusive(newTrait))
                {
                    addTrait = false;
                    break;
                }
            }

            if (addTrait)
            {
                Traits[i] = newTrait;
            }
        }

        //set up inventory items
        inventory = new Inventory(TradingUI.TradingSlotsCount);


        IsSelling = Random.Range(0, 100f) < 33f;



        if (IsSelling)
        {
            SalesType = (SellerType)Random.Range(1, (int)SellerType.Buyer);

            BaseItemData[] itemData = GameManager.Instance._NPCGenerationData.ItemsForSellerType(SalesType);

            foreach (BaseItemData item in itemData)
            {
                inventory.AddItem(new Item(item, -1, 0));
            }
        }
        else
        {
            SalesType = SellerType.Buyer;

            Item[] inventoryItems = GameManager.Instance._NPCGenerationData.RandomItemsForBuyer();

            foreach(Item item in inventoryItems)
            {
                inventory.AddItem(item);
            }
        }

        


        //set items the npc is looking for
        float seed = Random.Range(0f, 100f);

        //to do - make it slightly more likely that the npc is looking for categories that are in the player's inventories. make people looking for animals quite rare.
        ItemCategory typeCategory = GameManager.Instance._ItemDatabase.NPCBuyCategories[Random.Range(0, GameManager.Instance._ItemDatabase.NPCBuyCategories.Count)];
        ItemCategory typeCategory2;

        do
        {
            typeCategory2 = GameManager.Instance._ItemDatabase.NPCBuyCategories[Random.Range(0, GameManager.Instance._ItemDatabase.NPCBuyCategories.Count)];
        } while (typeCategory == typeCategory2 || typeCategory2 == ItemCategory.Animal);

        if (seed < 10f)
        {
            //10% chance to just want a single category

            WantsToBuy = new ItemSpecifier[1];
            WantsToBuy[0].Category = typeCategory;
            WantsToBuy[0].Descriptors = new ItemDescriptor[0];
        }
        else if(seed < 20f && typeCategory != ItemCategory.Tool)
        {
            //10% chance to want a single category with a random subDescriptor restriction, doesnt include tools

            WantsToBuy = new ItemSpecifier[1];
            WantsToBuy[0].Category = typeCategory;
            WantsToBuy[0].Descriptors = new ItemDescriptor[1];
            WantsToBuy[0].Descriptors[0] = GameManager.Instance._ItemDatabase.RandomSubDescriptorInCategory(typeCategory);
        }
        else if(seed < 80f)
        {
            //60% chance to want a few types of items

            int typesCount = 0;

            float typeSeed = Random.Range(0f, 100f);

            if (typeCategory == ItemCategory.Animal)
            {
                typeSeed = 1f;
            }

            if (typeSeed < 60f)
            {
                //60% chance to want a single type
                WantsToBuy = new ItemSpecifier[1];
                typesCount = 1;
            }
            else if(typeSeed < 90f)
            {
                //30% chance to want 2 types
                WantsToBuy = new ItemSpecifier[2];
                typesCount = 2;
            }
            else
            {
                //10% chance to want 3 types
                WantsToBuy = new ItemSpecifier[3];
                typesCount = 3;
            }

            ItemDescriptor[] descriptors = GameManager.Instance._ItemDatabase.RandomDescriptorsInCategory(typeCategory, typesCount);
            int index = Random.Range(0, descriptors.Length);

            if (typesCount > 1 && Random.Range(0, 100f) < 10f && typeCategory != ItemCategory.Animal)
            {
                //10% chance for one of the types to be replaced by another from a random category (not including animals)
                descriptors[index] = GameManager.Instance._ItemDatabase.RandomDescriptorInCategory(typeCategory2);
            }

            for (int i = 0; i < WantsToBuy.Length; i++)
            {
                if(i == index)
                {
                    WantsToBuy[i].Category = typeCategory2;
                }
                else
                {
                    WantsToBuy[i].Category = typeCategory;
                }
                
                WantsToBuy[i].Descriptors = new ItemDescriptor[1];
                WantsToBuy[i].Descriptors[0] = descriptors[i];
            }
            
        }
        else
        {
            //20% chance to want a few specific items
            int itemCount = Random.Range(1, 3);

            WantsToBuy = new ItemSpecifier[itemCount];
            ItemDescriptor[] descriptors = GameManager.Instance._ItemDatabase.RandomDescriptorsInCategory(typeCategory, itemCount);

            int replaceIndex = Random.Range(0, descriptors.Length);

            if (itemCount > 1 && Random.Range(0, 100f) < 5f && typeCategory != ItemCategory.Animal)
            {
                //5% chance for one of the types to be replaced by another from a random category (not including animals)

                descriptors[replaceIndex] = GameManager.Instance._ItemDatabase.RandomDescriptorInCategory(typeCategory2);
            }

            for (int i = 0; i < WantsToBuy.Length; i++)
            {
                ItemDescriptor[] subDescriptors = GameManager.Instance._ItemDatabase.SubDescriptorsForItem(descriptors[i]);

                if(i == replaceIndex)
                {
                    WantsToBuy[i].Category = typeCategory2;
                }
                else
                {
                    WantsToBuy[i].Category = typeCategory;
                }
                

                if (subDescriptors.Length > 0)
                {
                    
                    WantsToBuy[i].Descriptors = new ItemDescriptor[2];

                    WantsToBuy[i].Descriptors[0] = descriptors[i];
                    WantsToBuy[i].Descriptors[1] = subDescriptors[Random.Range(0, subDescriptors.Length)];
                }
                else
                {
                    WantsToBuy[i].Descriptors = new ItemDescriptor[1];
                    WantsToBuy[i].Descriptors[0] = descriptors[i];
                }
            }

        }
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
        if(item == null || item.HasDescriptor(ItemDescriptor.None))
        {
            return false;
        }

        if(item.CashValue != -1)
        {
            return true;
        }

        bool rightCategory = false;
        bool hasRightDescriptors = false;

        foreach (ItemSpecifier itemSpecifier in WantsToBuy)
        {

            
            if(item.BaseItemData.Category != ItemCategory.None && item.BaseItemData.Category == itemSpecifier.Category)
            {
                rightCategory = true;
            }

            if (item.HasDescriptors(itemSpecifier.Descriptors))
            {
                hasRightDescriptors = true;
            }
        }

        return rightCategory && hasRightDescriptors;
    }

    public void OnPlayerInteract()
    {

    }

    private int PerceivedValueForItem(Item item)
    {
        //to do - add multipliers for npc traits to the value
        float perceivedValueMultiplier = 1f;

        if (item.CashValue != -1)
        {
            return item.CashValue;
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

    public void SetResponseForTradeOffer(TradeOffer PlayerOffer)
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

            //to do - use MaxNumberOfBuyItems field to limit the number of items the npc is interested in

            foreach (Item item in PlayerOffer.Items)
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
    }

}

public class TradeOffer
{
    public TradeOffer()
    {
        Items = new Item[16];
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
    public ItemDescriptor[] Descriptors;
    public ItemCategory Category;
}

public enum Trait
{
    None,
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

    TRAIT_MAX,
    
}

public enum SellerType
{
    None,
    HerbsAndSpices,
    Food,
    Mining,
    Animals,
    Fabrics,
    Naturals,
    Scholar,
    Buyer,
}

static class TraitExtensions
{
    public static bool IsMutuallyExclusive(this Trait trait, Trait otherTrait)
    {
        switch (trait)
        {
            case Trait.Moral:
                return otherTrait == Trait.Shady;

            case Trait.Shady:
                return otherTrait == Trait.Moral;

            case Trait.Haggler:
                return otherTrait == Trait.Impatient;

            case Trait.Impatient:
                return otherTrait == Trait.Haggler;

            case Trait.Modest:
                return otherTrait == Trait.Vain;

            case Trait.Vain:
                return otherTrait == Trait.Modest;

            case Trait.Cynical:
                return otherTrait == Trait.Naive;

            case Trait.Naive:
                return otherTrait == Trait.Cynical;

            case Trait.LocalTrader:
                return otherTrait == Trait.Tourist;

            case Trait.Tourist:
                return otherTrait == Trait.LocalTrader;

            default:
                return false;
        }
    }
}

public enum NPCState
{
    Traveling,
    Trading,
    ClosingTrade,
    Speaking,
}