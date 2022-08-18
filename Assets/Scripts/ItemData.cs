using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemCategory Category;
    public ItemType Type;
    public ItemID ID;

    public int BaseValue;
    public string ItemName;

    public int HealthRestored;
    public int HungerRestored;
    public int ThirstRestored;

    public bool IsStackable;

    public Sprite UISprite;

}

public enum ItemID
{
    Salt,
    GoldNugget,
    Shovel,
    SilverRubyRing,
    GoldEmeraldRing,
    SilverEmeraldRing,
    LeatherGloves,

    //ALWAYS ADD IDs TO THE END OF THE ENUM, OR IT WILL MOVE ALL OTHER ITEM IDs
}

public enum ItemType
{
    Meat = 0,
    Fruit,
    Vegetable,
    Spice,
    Drink,
    Alcohol,

    Gloves = 1000,
    Scarf,


    Ring = 3000,
    Bracelet,

    Gold = 6000,
    Silver,
    Wood,

    None,
}

public enum ItemCategory
{
    Food,
    Clothing,
    Animal,
    Jewelry,
    Rarity,
    Tool,
    RawMaterial,

    None,
}

public enum ItemQuality
{
    None,
    OneStar,
    TwoStar,
    ThreeStar,
    FourStar,
    FiveStar,
}

public enum GemType
{
    Ruby,
    Emerald,
}