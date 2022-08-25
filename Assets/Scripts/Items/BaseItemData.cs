using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Base Item Data")]
public class BaseItemData : ScriptableObject
{
    public ItemCategory Category;
    public ItemDescriptor[] BaseDescriptors;

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

    public ItemDescriptor[] SubDescriptors;

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

public enum ItemDescriptor
{
    None,

    //food
    [InspectorName("Food/Meat")]
    Meat = 1,
    [InspectorName("Food/Fruit")]
    Fruit,
    [InspectorName("Food/Vegetable")]
    Vegetable,
    [InspectorName("Food/Dairy")]
    Dairy,
    [InspectorName("Food/Spice")]
    Spice,
    [InspectorName("Food/Herb")]
    Herb,
    [InspectorName("Food/Drink")]
    Drink,
    [InspectorName("Food/Alcohol")]
    Alcohol,
    [InspectorName("Food/Sweet")]
    Sweet,
    [InspectorName("Food/Meal")]
    Meal,

    [InspectorName("Food/FruitCake")]
    FruitCake,
    [InspectorName("Food/TurkishDelight")]
    TurkishDelight,

    [InspectorName("Food/MeatPita")]
    MeatPita,
    [InspectorName("Food/VegetablePita")]
    VegetablePita,

    [InspectorName("Food/Beef")]
    Beef,
    [InspectorName("Food/Mutton")]
    Mutton,
    [InspectorName("Food/Fish")]
    Fish,

    [InspectorName("Food/Mint")]
    Mint,
    [InspectorName("Food/Lavender")]
    Lavender,
    [InspectorName("Food/Cinammon")]
    Cinnammon,

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
    Padouk,
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
    Camel,
    Gazelle,
    Bustard,
    Goat,
    Horse,
    SandCat,
    HornedViper,
    SpinyTailedLizard,
    Wagtail,
    SaharaFrog,

    //conditions
    Refurbished = 7000,
    Damaged,
    Stolen,
    CanStoreLiquid,
    IsDyeable,
    CanHoldGem,
    NoGem,
    UnDyed,

    LargePainting,
    MediumPainting,
    SmallPainting,

    //SPECIFIC ITEMS
    Oil = 9000,
    Coal,
    StoneItem,
    PadoukLog,
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
    AnimalHideItem,
    Lens,

    Cash,

    [InspectorName("Food/Orange")]
    Orange,
    [InspectorName("Food/Lemon")]
    Lemon,
    [InspectorName("Food/Date")]
    Date,
    [InspectorName("Food/Cacombara")]
    Cacombara,
    [InspectorName("Food/CoffeeBeans")]
    CoffeeBeans,

    [InspectorName("Food/Bread")]
    Bread,
    [InspectorName("Food/Rice")]
    Rice,
    [InspectorName("Food/Wheat")]
    Wheat,
    [InspectorName("Food/BeefItem")]
    BeefItem,
    [InspectorName("Food/MuttonItem")]
    MuttonItem,
    [InspectorName("Food/FishItem")]
    FishItem,
    [InspectorName("Food/Flour")]
    Flour,
    [InspectorName("Food/Butter")]
    Butter,
    [InspectorName("Food/Sugar")]
    Sugar,
    [InspectorName("Food/Milk")]
    Milk,
    [InspectorName("Food/Tomatoes")]
    Tomatoes,
    [InspectorName("Food/Eggplant")]
    Eggplant,
    [InspectorName("Food/Pepper")]
    Pepper,
    [InspectorName("Food/GrainSpirit")]
    GrainSpirit,

    [InspectorName("Food/Pancakes")]
    Pancakes,
    [InspectorName("Food/FruitSalad")]
    FruitSalad,

    [InspectorName("Food/CuredFish")]
    CuredFish,
    [InspectorName("Food/MintLamb")]
    MintLamb,
    [InspectorName("Food/BeefStew")]
    BeefStew,
    [InspectorName("Food/RiceBowl")]
    RiceBowl,
    [InspectorName("Food/CousCous")]
    CousCous,

    [InspectorName("Food/MintItem")]
    MintItem,
    [InspectorName("Food/LavenderItem")]
    LavenderItem,
    [InspectorName("Food/CinammonItem")]
    CinnammonItem,
    [InspectorName("Food/Sage")]
    Sage,
    [InspectorName("Food/Wormwood")]
    Wormwood,
    [InspectorName("Food/Tea")]
    Tea,

    [InspectorName("Food/TeaDrink")]
    TeaDrink,
    [InspectorName("Food/CoffeeDrink")]
    CoffeeDrink,


    [InspectorName("Food/Salt")]
    Salt,
    [InspectorName("Food/Peppercorns")]
    Peppercorns,
    [InspectorName("Food/Saffron")]
    Saffron,
    [InspectorName("Food/Ginger")]
    Ginger,
    [InspectorName("Food/Cloves")]
    Cloves,

    FulaniHat,
    WideBrimmedHat,

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