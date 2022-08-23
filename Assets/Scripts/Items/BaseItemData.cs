using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Base Item Data")]
public class BaseItemData : ScriptableObject
{
    public ItemCategory Category;
    public ItemDescriptor[] BaseDescriptors;
    public ItemID ID;

    public string BaseName;

    public int BaseValue;

    public int HealthRestored;
    public int HungerRestored;
    public int ThirstRestored;

    public Sprite _UISprite;

    public bool IsStackable;

    public virtual Sprite UISprite
    {
        get
        {
            return _UISprite;
        }
    }

    [SerializeField]
    public MaterialMultiplier[] MaterialMultipliers;

    public float MultiplierForMaterial(ItemDescriptor material)
    {
        for(int i = 0; i < MaterialMultipliers.Length; i++)
        {
            if(MaterialMultipliers[i].Material == material)
            {
                return MaterialMultipliers[i].Multiplier;
            }
        }

        return 1f;
    }
}

public enum ItemID
{
    None,

    

    //ALWAYS ADD IDs TO THE END OF THE ENUM, OR IT WILL MOVE ALL OTHER ITEM IDs
}

public enum ItemDescriptor
{
    None,

    //food
    Meat = 1,
    Fruit,
    Vegetable,
    Dairy,
    Spice,
    Herb,
    Drink,
    Alcohol,
    Sweet,
    Meal,

    FruitCake,
    TurkishDelight,

    MeatPita,
    VegetablePita,

    Beef,
    Mutton,
    Fish,

    Mint,
    Lavender,
    Cinammon,

    //plants
    Seeds = 999,

    //clothing
    Gloves = 1000,
    Headscarf,
    Robes,
    Shoe,
    Sandals,
    Bag,
    Hat,

    Dress,
    Glasses,
    Goggles,

    //jewelry
    Ring = 3000,
    Bracelet,
    Necklace,
    Earrings,
    Crown,
    Watch,

    //tools
    Sword = 4000,
    Shovel,
    Spear,
    Pickaxe,
    Axe,
    Spyglass,
    Compass,
    Knife,
    Hoe,
    Staff,
    Bowl,
    Cup,
    Bottle,
    Lantern,
    Lockbox,
    TinderBox,
    Canteen,

    //decorative
    Pottery = 4500,
    Vase,
    Urn,
    Jug,
    Rug,
    Painting,
    Flag,
    Carving,

    //dyes
    White,
    Black,
    Red,
    Green,
    Blue,
    Purple,
    Yellow,

    //raw materials
    Gold = 6000,
    Silver,
    Brass,
    Iron,
    ScrapIron,
    Stone,
    Wood,
    Pine,
    Mahogany,
    Acacia,
    Ivory,
    Gem,
    Cloth,
    AnimalHide,
    Leather,
    Ceramic,
    Dye,
    Fuel,
    String,
    Silk,

    Ruby,
    Emerald,
    Pearl,
    Sapphire,
    Amethyst,
    Amber,
    Seashell,

    //animals
    Pet = 6500,
    Large,

    //conditions
    Refurbished = 7000,
    Damaged,
    Stolen,
    CanStoreLiquid,
    IsDyeable,
    CanHoldGem,

    //SPECIFIC ITEMS
    Oil = 9000,
    Coal,
    StoneItem,
    PineLog,
    MahoganyLog,
    AcaciaLog,
    Tusk,
    ScrapIronItem,
    IronBar,
    GoldNugget,
    GoldBar,
    Rope,
    StringItem,
    Seashell1,
    Seashell2,
    Seashell3,
    ClothItem,
    ClothRoll,
    LeatherItem,
    TigerHide,
    SheepHide,
    Lens,

    Cash,

    Orange,
    Lemon,
    Date,
    PricklyPear,
    CoffeeBeans,

    Bread,
    Rice,
    Wheat,
    BeefItem,
    MuttonItem,
    FishItem,
    Flour,
    Butter,
    Sugar,
    Milk,
    Tomatoes,
    Eggplant,
    Pepper,
    GrainSpirit,

    Murakkaba,
    FruitSalad,

    CuredFish,
    MintLamb,
    BeefStew,
    RiceBowl,
    BatteredFish,
    CousCous,

    MintItem,
    LavenderItem,
    Sage,
    Wormwood,
    Tea,

    Salt,
    Peppercorns,
    Cinmamon,
    Saffron,
    Ginger,
    Cloves,

    

}

public enum ItemCategory
{
    None,
    Food,
    Clothing,
    Animal,
    Jewelry,
    Rarity,
    Tool,
    RawMaterial,
    Plant,
    Decorative,
    
    CATEGORY_MAX,
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