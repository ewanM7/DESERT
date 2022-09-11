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
    public TradeOffer CurrentTradeOffer;

    public int MaxNumberOfBuyItems;

    /// <summary>
    /// Percentage of how much the npc is willing to offer over for items they're buying, or accept offers under for items they're selling
    /// </summary>
    public float PriceTolerance;

    /// <summary>
    /// Initial NPC buy offers are multiplied by this value
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
    public float LookingForSpecificItemsMultiplier;

    /// <summary>
    /// Interest in continuing the trade on a scale from 1 to 100 - trade ends if the interest drops to 0
    /// </summary>
    public float TradeInterest;

    /// <summary>
    /// Decrease in interest is multiplied by this value
    /// </summary>
    public float InterestMultiplier;


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
        PriceTolerance = Random.Range(0.02f, 0.15f);
        BuyMultiplier = Random.Range(0.9f, 1.1f);
        SellMultiplier = Random.Range(0.9f, 1.15f);
        OfferMultiplier = Random.Range(1.03f, 1.07f);

        TradeInterest = 100f;
        InterestMultiplier = Random.Range(0.95f, 1.05f);

        //set max number of items the npc will buy
        MaxNumberOfBuyItems = GameManager.Instance._NPCGenerationData.GetMaxBuyItemNumber();

        //set traits
        int traitCount = 0;

        float roll = Random.Range(0, 100f);

        if(roll < 40f)
        {
            //40% chance for 1 trait
            traitCount = 1;
        }
        else if (roll < 70f)
        {
            //30% chance for 2 traits
            traitCount = 2;
        }
        else if(roll < 90f)
        {
            //20% chance for 3 traits
            traitCount = 3;
        }
        else
        {
            //10% chance for 4 traits
            traitCount = 4;
        }

        List<Trait> newTraits = new List<Trait>();

        for(int i = 0; i < traitCount; i++)
        {
            Trait newTrait = (Trait)Random.Range(1, (int)Trait.TRAIT_MAX);
            bool addTrait = true;

            //make sure mutually exclusive traits aren't added
            foreach(Trait trait in Traits)
            {
                if (trait.IsMutuallyExclusive(newTrait) || newTraits.Contains(newTrait))
                {
                    addTrait = false;
                    break;
                }
            }

            if (addTrait)
            {
                newTraits.Add(newTrait);
            }
            else
            {
                //roll for another trait if the new one cant be added
                i--;
            }
        }

        Traits = newTraits.ToArray();

        //set up inventory items
        inventory = new Inventory(TradingUI.TRADING_SLOTS_COUNT);


        IsSelling = Random.Range(0, 100f) < 33f;



        if (IsSelling)
        {
            //generate a salesman type and add the appropriate selling items
            SalesType = (SellerType)Random.Range(1, (int)SellerType.Buyer);

            BaseItemData[] itemData = GameManager.Instance._NPCGenerationData.ItemsForSellerType(SalesType);

            foreach (BaseItemData item in itemData)
            {
                inventory.AddItem(new Item(item, -1, 0));
            }
        }
        else
        {
            //add a few random items if the npc is a buyer
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
            LookingForSpecificItemsMultiplier = 1f;
        }
        else if(seed < 20f && typeCategory != ItemCategory.Tool)
        {
            //10% chance to want a single category with a random subDescriptor restriction, doesnt include tools

            WantsToBuy = new ItemSpecifier[1];
            WantsToBuy[0].Category = typeCategory;
            WantsToBuy[0].Descriptors = new ItemDescriptor[1];
            WantsToBuy[0].Descriptors[0] = GameManager.Instance._ItemDatabase.RandomSubDescriptorInCategory(typeCategory);
            LookingForSpecificItemsMultiplier = 1f;
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

            LookingForSpecificItemsMultiplier = 1f;
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

            LookingForSpecificItemsMultiplier = 1.05f;
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

        //to do - include traits like sweet tooth here, so they always want to buy the item

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

        if (offer != null)
        {
            foreach (Item item in offer.Items)
            {
                if (item != null)
                {
                    value += PerceivedValueForItem(item);
                }
            }
        }

        return value;
    }

    public void SetResponseForTradeOffer(TradeOffer PlayerOffer)
    {
        if(TradeInterest <= 0f)
        {
            //ran out of interest, close the trade
            State = NPCState.ClosingTrade;
        }

        float InterestDecreaseForThisOffer = 0f;
        InterestDecreaseForThisOffer += Random.Range(0.15f, 0.2f);

        int perceivedOfferValue = PerceivedValueForOffer(PlayerOffer);

        if(CurrentTradeOffer == null)
        {
            CurrentTradeOffer = new TradeOffer();
        }

        //to do - affect NPC interest based on the difference between the offer and the wanted price. interest should also decline when offered items they aren't looking for
        
        if (IsSelling)
        {
            //npc is selling to the player

            if (PlayerOffer == null)
            {
                //trading has just started - add all selling items to the offer

                for(int i = 0; i < CurrentTradeOffer.Items.Length; i++)
                {
                    CurrentTradeOffer.Items[i] = inventory.ItemStacks[i].item;
                }

                CurrentTradeOffer.WantedValue = 0;

                foreach (Item item in CurrentTradeOffer.Items)
                {
                    if (item != null)
                    {
                        CurrentTradeOffer.WantedValue += item.Value;
                    }
                }

                CurrentTradeOffer.WantedValue = Mathf.RoundToInt(CurrentTradeOffer.WantedValue * SellMultiplier);
            }
            else
            {
                bool discardedItem = false;
                for (int i = 0; i < CurrentTradeOffer.Items.Length; i++)
                {
                    //discard items that the player is not interested in buying
                    if (CurrentTradeOffer.Items[i] != null && !PlayerOffer.WantedItemIndexes[i])
                    {
                        discardedItem = true;
                        CurrentTradeOffer.Items[i] = null;
                    }
                }

                if(discardedItem)
                {
                    InterestDecreaseForThisOffer += Random.Range(0.05f, 0.1f);
                }

                //set wanted indexes for items the NPC is interested in
                //to do - limit number of items wanted by the max buy items value
                for(int i = 0; i < PlayerOffer.Items.Length; i++)
                {
                    if (PlayerOffer.Items[i] == null || !IsLookingForItem(PlayerOffer.Items[i]))
                    {
                        CurrentTradeOffer.WantedItemIndexes[i] = false;
                    }
                    else
                    {
                        CurrentTradeOffer.WantedItemIndexes[i] = true;
                    }
                }

                CurrentTradeOffer.WantedValue = 0;

                foreach (Item item in CurrentTradeOffer.Items)
                {
                    if (item != null)
                    {
                        CurrentTradeOffer.WantedValue += item.Value;
                    }
                }

                CurrentTradeOffer.WantedValue = Mathf.RoundToInt(CurrentTradeOffer.WantedValue * SellMultiplier);

                if (perceivedOfferValue >= CurrentTradeOffer.WantedValue * (1f - PriceTolerance))
                {
                    //player offer value is above or equal to the wanted value including the price tolerance - accept the trade
                    CurrentTradeOffer.Accepted = true;
                }
                else if((CurrentTradeOffer.WantedValue - perceivedOfferValue) / CurrentTradeOffer.WantedValue >  (PriceTolerance + 0.15f))
                {
                    //player offer is lower than 15% down from the price tolerance - decline the trade and decrease interest
                    InterestDecreaseForThisOffer += Random.Range(0.08f, 0.15f);
                }
                else
                {
                    //player offer is too low, but is within acceptable range - decline the trade and decrease interest slightly
                    InterestDecreaseForThisOffer += Random.Range(0.03f, 0.08f);
                }
            }
        }
        else
        {
            //npc is buying from the player
            int z = 0;
            bool makeOffer = true;

            List<int> wantedIndexes = new List<int>();
            //CurrentTradeOffer.WantedItemIndexes[z] = true;

            foreach (Item item in PlayerOffer.Items)
            {
                if(item != null)
                {
                    if (IsLookingForItem(item))
                    {
                        wantedIndexes.Add(z);
                        //to do - take traits into account here to check if they want more of the same item, or more of similar items.
                    }
                    else
                    {
                        makeOffer = false;
                    }
                }

                z++;
            }

            //mark a random assortment of items as "wanted", up to the max number of buy items
            for(int i = 0; i < MaxNumberOfBuyItems; i++)
            {
                int randomIndex = Random.Range(0, wantedIndexes.Count);
                CurrentTradeOffer.WantedItemIndexes[wantedIndexes[randomIndex]] = true;
                wantedIndexes.RemoveAt(randomIndex);
            }

            if(!makeOffer)
            {
                InterestDecreaseForThisOffer += Random.Range(0.05f, 0.1f);
            }

            if (makeOffer)
            {
                if (PlayerOffer.WantedValue <= perceivedOfferValue)
                {
                    //value the player wants is below or equal to the perceived value - make an offer at or below the wanted value
                    if(PreviousOfferValue == 0)
                    {
                        //first offer, start lowest

                        int targetOfferValue = Mathf.RoundToInt(PlayerOffer.WantedValue * Random.Range(0.8f, 0.9f) * BuyMultiplier * LookingForSpecificItemsMultiplier);
                        int currentOfferValue = 0;

                        //add items from the NPC inventory, up to the target offer value
                        int nextFreeIndex = 0;
                        for (int i = 0; i < TradingUI.TRADING_SLOTS_COUNT; i++)
                        {
                            Item currentItem = inventory.ItemStacks[i].item;
                            if (currentItem != null)
                            {
                                if(currentItem.Value + currentOfferValue < targetOfferValue)
                                {
                                    CurrentTradeOffer.Items[i] = currentItem;
                                    currentOfferValue += currentItem.Value;
                                    nextFreeIndex = i + 1;
                                }
                            }
                        }

                        //add cash to fill out the offer up to the target value
                        if(currentOfferValue < targetOfferValue && nextFreeIndex < 9)
                        {
                            CurrentTradeOffer.Items[nextFreeIndex] = new Item(GameManager.Instance._ItemDatabase.Cash, targetOfferValue - currentOfferValue);
                        }

                        //calculate total offer value
                        foreach (Item item in CurrentTradeOffer.Items)
                        {
                            if (item != null)
                            {
                                if(item.CashValue > 0)
                                {
                                    PreviousOfferValue += item.CashValue;
                                }
                                else
                                {
                                    PreviousOfferValue += item.Value;
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        bool discardedItem = false;
                        //remove items from the offer the player is not interested in
                        for (int i = 0; i < PlayerOffer.WantedItemIndexes.Length; i++)
                        {
                            if(!PlayerOffer.WantedItemIndexes[i])
                            {
                                discardedItem = true;
                                CurrentTradeOffer.Items[i] = null;
                            }
                        }

                        if(discardedItem)
                        {
                            InterestDecreaseForThisOffer += Random.Range(0.05f, 0.1f);
                        }

                        int currentItemValue = 0;
                        int cashIndex = -1;
                        int freeIndex = -1;

                        //calculate the current value of all items in the offer
                        for(int i = 0; i < CurrentTradeOffer.Items.Length; i++)
                        {
                            if(CurrentTradeOffer.Items[i] != null)
                            {
                                if(CurrentTradeOffer.Items[i].CashValue > 0)
                                {
                                    cashIndex = i;
                                }
                                else
                                {
                                    currentItemValue += CurrentTradeOffer.Items[i].Value;
                                }
                            }
                            else
                            {
                                freeIndex = i;
                            }
                        }

                        //player wants more value - new offer should be higher than the previous, or close the trade if interest is too low
                        int newTargetOfferValue = Mathf.Clamp(Mathf.RoundToInt(PreviousOfferValue * OfferMultiplier), 0, PlayerOffer.WantedValue);
                        PreviousOfferValue = newTargetOfferValue;

                        //add cash to the offer. use the same slot that cash is already in, or the first free slot if there is not cash offered yet
                        if (cashIndex > -1)
                        {
                            CurrentTradeOffer.Items[cashIndex] = new Item(GameManager.Instance._ItemDatabase.Cash, newTargetOfferValue - currentItemValue);
                        }
                        else
                        {
                            CurrentTradeOffer.Items[freeIndex] = new Item(GameManager.Instance._ItemDatabase.Cash, newTargetOfferValue - currentItemValue);
                        }

                    }
                }
                else
                {
                    //the player wants more than the perceived value - offer around the perceived value up to the player's wanted value

                    if (PreviousOfferValue == 0)
                    {
                        //first offer, start lowest
                        int targetOfferValue = Mathf.RoundToInt(Mathf.Clamp(perceivedOfferValue * BuyMultiplier * LookingForSpecificItemsMultiplier, 0, PlayerOffer.WantedValue));

                        int currentOfferValue = 0;

                        //add items from the NPC inventory, up to the target offer value
                        int nextFreeIndex = 0;
                        for (int i = 0; i < TradingUI.TRADING_SLOTS_COUNT; i++)
                        {
                            Item currentItem = inventory.ItemStacks[i].item;
                            if (currentItem != null)
                            {
                                if (currentItem.Value + currentOfferValue < targetOfferValue)
                                {
                                    CurrentTradeOffer.Items[i] = currentItem;
                                    currentOfferValue += currentItem.Value;
                                    nextFreeIndex = i + 1;
                                }
                            }
                        }

                        //add cash to fill out the offer up to the target value
                        if (currentOfferValue < targetOfferValue && nextFreeIndex < 9)
                        {
                            CurrentTradeOffer.Items[nextFreeIndex] = new Item(GameManager.Instance._ItemDatabase.Cash, targetOfferValue - currentOfferValue);
                        }

                        //calculate total offer value
                        foreach (Item item in CurrentTradeOffer.Items)
                        {
                            if (item != null)
                            {
                                if (item.CashValue > 0)
                                {
                                    PreviousOfferValue += item.CashValue;
                                }
                                else
                                {
                                    PreviousOfferValue += item.Value;
                                }

                            }
                        }

                        FirstOfferValue = PreviousOfferValue;
                    }
                    else
                    {
                        bool discardedItem = false;

                        //remove items from the offer the player is not interested in
                        for (int i = 0; i < PlayerOffer.WantedItemIndexes.Length; i++)
                        {
                            if (!PlayerOffer.WantedItemIndexes[i])
                            {
                                discardedItem = true;
                                CurrentTradeOffer.Items[i] = null;
                            }
                        }

                        if(discardedItem)
                        {
                            InterestDecreaseForThisOffer += Random.Range(0.05f, 0.1f);
                        }

                        int currentItemValue = 0;
                        int cashIndex = -1;
                        int freeIndex = -1;

                        //calculate the current value of all items in the offer
                        for (int i = 0; i < CurrentTradeOffer.Items.Length; i++)
                        {
                            if (CurrentTradeOffer.Items[i] != null)
                            {
                                if (CurrentTradeOffer.Items[i].CashValue > 0)
                                {
                                    cashIndex = i;
                                }
                                else
                                {
                                    currentItemValue += CurrentTradeOffer.Items[i].Value;
                                }
                            }
                            else
                            {
                                freeIndex = i;
                            }
                        }

                        if((PlayerOffer.WantedValue - perceivedOfferValue) / perceivedOfferValue > (PriceTolerance + 0.15f))
                        {
                            InterestDecreaseForThisOffer += Random.Range(0.08f, 0.15f);
                        }

                        //player wants more value - new offer should be higher than the previous, up to the price tolerance or the player's wanted value. or close the trade if interest is too low
                        int newTargetOfferValue = Mathf.RoundToInt(Mathf.Clamp(PreviousOfferValue * OfferMultiplier, 0, Mathf.Min((1f + PriceTolerance) * FirstOfferValue, PlayerOffer.WantedValue)));
                        PreviousOfferValue = newTargetOfferValue;

                        //add cash to the offer. use the same slot that cash is already in, or the first free slot if there is not cash offered yet
                        if (cashIndex > -1)
                        {
                            CurrentTradeOffer.Items[cashIndex] = new Item(GameManager.Instance._ItemDatabase.Cash, newTargetOfferValue - currentItemValue);
                        }
                        else
                        {
                            CurrentTradeOffer.Items[freeIndex] = new Item(GameManager.Instance._ItemDatabase.Cash, newTargetOfferValue - currentItemValue);
                        }

                    }
                }
            }
        }

        TradeInterest -= InterestDecreaseForThisOffer * InterestMultiplier;
    }

}

public class TradeOffer
{
    public TradeOffer()
    {
        Items = new Item[TradingUI.TRADING_SLOTS_COUNT];
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
    None = 0,
    Alcoholic = 1,
    SweetTooth = 2,
    BulkBuyer = 5,
    Collector = 6,
    Haggler = 7,
    Impatient = 8,
    Trusting = 9,
    AnimalLover = 10,
    Gambler = 11,
    HighStandards = 12,
    Modest = 13,
    Vain = 14,
    Cynical = 15,
    Naive = 16,
    LocalTrader = 17,
    Tourist = 18,

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
    Travelling,
    Trading,
    ClosingTrade,
    Speaking,
}