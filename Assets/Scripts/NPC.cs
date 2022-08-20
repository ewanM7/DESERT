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

    public int Interest;
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
        foreach(ItemSpecifier itemSpecifier in WantsToBuy)
        {
            if(item.itemData.Category == itemSpecifier.Category)
            {
                return true;
            }

            if(item.itemData.Type == itemSpecifier.Type)
            {
                return true;
            }

            if(item.itemData.ID == itemSpecifier.ID)
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

        if (Traits.Contains(Trait.Alcoholic) && item.itemData.Type == ItemType.Alcohol)
        {
            //add to multiplier
        }

        if (Traits.Contains(Trait.SweetTooth) && item.itemData.Type == ItemType.Sweet)
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


        TradeOffer response = new TradeOffer();

        if (IsSelling)
        {
            //npc is selling to the player

            
            if(perceivedOfferValue >= CurrentTradeOffer.WantedValue * (1 - PriceTolerance))
            {
                //player offer is above or equal to wanted value - accept the trade
                response.Items = CurrentTradeOffer.Items;
                response.Accepted = true;
            }

        }
        else
        {
            //npc is buying from the player

            
            if (PlayerOffer.WantedValue <= perceivedOfferValue)
            {
                //value the player wants is below or equal to the perceived value - make an offer at or below the wanted value

            }
            else
            {
                //the player wants more than the perceived value - offer around the perceived value up the the price tolerance max

            }
        }



        CurrentTradeOffer = response;
        return response;
    }

}

public struct TradeOffer
{
    public Item[] Items;
    public int WantedValue;
    public bool Accepted;
}

public struct ItemSpecifier
{
    public ItemID ID;
    public ItemType Type;
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
    Speaking,
}