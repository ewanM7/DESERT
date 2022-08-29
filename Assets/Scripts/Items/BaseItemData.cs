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
    None = 0,

    //food
    [InspectorName("Food/Meat")]
    Meat = 1,
    [InspectorName("Food/Fruit")]
    Fruit = 2,
    [InspectorName("Food/Vegetable")]
    Vegetable = 3,
    [InspectorName("Food/Dairy")]
    Dairy = 4,
    [InspectorName("Food/Spice")]
    Spice = 5,
    [InspectorName("Food/Herb")]
    Herb = 6,
    [InspectorName("Food/Drink")]
    Drink = 7,
    [InspectorName("Food/Alcohol")]
    Alcohol = 8,
    [InspectorName("Food/Sweet")]
    Sweet = 9,
    [InspectorName("Food/Meal")]
    Meal = 10,

    [InspectorName("Food/FruitCake")]
    FruitCake = 11,
    [InspectorName("Food/TurkishDelight")]
    TurkishDelight = 12,

    [InspectorName("Food/MeatPita")]
    MeatPita = 13,
    [InspectorName("Food/VegetablePita")]
    VegetablePita = 14,

    [InspectorName("Food/Beef")]
    Beef = 15,
    [InspectorName("Food/Mutton")]
    Mutton = 16,
    [InspectorName("Food/Fish")]
    Fish = 17,

    [InspectorName("Food/Mint")]
    Mint = 18,
    [InspectorName("Food/Lavender")]
    Lavender = 19,
    [InspectorName("Food/Cinammon")]
    Cinnammon = 20,

    //plants
    Seeds = 999,

    //clothing
    Gloves = 1000,
    Headscarf = 1001,
    Robes = 1002,
    Shoe = 1003,
    Sandals = 1004,
    Bag = 1005,
    Hat = 1006,

    Dress = 1007,
    Glasses = 1008,
    Goggles = 1009,

    //jewelry
    Ring = 3000,
    Bracelet = 3001,
    Necklace = 3002,
    Earrings = 3003,
    Crown = 3004,
    Watch = 3005,

    //tools
    Sword = 4000,
    Shovel = 4001,
    Spear = 4002,
    Pickaxe = 4003,
    Axe = 4004,
    Spyglass = 4005,
    Compass = 4006,
    Knife = 4007,
    Hoe = 4008,
    Staff = 4009,
    Bowl = 4010,
    Cup = 4011,
    Bottle = 4012,
    Lantern = 4013,
    Lockbox = 4014,
    TinderBox = 4015,
    Canteen = 4016,

    //decorative
    Pottery = 4500,
    Vase = 4501,
    Urn = 4502,
    Jug = 4503,
    Rug = 4504,
    Painting = 4505,
    Flag = 4506,
    Carving = 4507,

    //dyes
    [InspectorName("Dye/White")]
    White = 4508,
    [InspectorName("Dye/Black")]
    Black = 4509,
    [InspectorName("Dye/Red")]
    Red = 4510,
    [InspectorName("Dye/Green")]
    Green = 4511,
    [InspectorName("Dye/Blue")]
    Blue = 4512,
    [InspectorName("Dye/Purple")]
    Purple = 4513,

    //raw materials
    [InspectorName("Metal/Gold")]
    Gold = 6000,
    [InspectorName("Metal/Silver")]
    Silver = 6001,
    [InspectorName("Metal/Brass")]
    Brass = 6002,
    [InspectorName("Metal/Iron")]
    Iron = 6003,
    [InspectorName("Metal/ScrapIron")]
    ScrapIron = 6004,
    Stone = 6005,
    Wood = 6006,
    Padauk = 6007,
    Mahogany = 6008,
    Acacia = 6009,
    Ivory = 6010,
    Gem = 6011,
    [InspectorName("Fabric/Cloth")]
    Cloth = 6012,
    [InspectorName("Fabric/AnimalHide")]
    AnimalHide = 6013,
    [InspectorName("Fabric/SmallHide")]
    SmallHide = 6027,
    [InspectorName("Fabric/Leather")]
    Leather = 6014,
    Ceramic = 6015,
    Clay = 6027,
    [InspectorName("Dye/Dye")]
    Dye = 6016,
    Fuel = 6017,
    [InspectorName("Fabric/String")]
    String = 6018,
    [InspectorName("Fabric/Silk")]
    Silk = 6019,

    Ruby = 6020,
    Emerald = 6021,
    Pearl = 6022,
    Sapphire = 6023,
    Amethyst = 6024,
    Amber = 6025,
    Seashell = 6026,

    //animals
    [InspectorName("Animals/Pet")]
    Pet = 6500,
    [InspectorName("Animals/Large")]
    Large = 6501,
    [InspectorName("Animals/Camel")]
    Camel = 6502,
    [InspectorName("Animals/Gazelle")]
    Gazelle = 6503,
    [InspectorName("Animals/Bustard")]
    Bustard = 6504,
    [InspectorName("Animals/Goat")]
    Goat = 6505,
    [InspectorName("Animals/Sandcat")]
    SandCat = 6507,
    [InspectorName("Animals/HornedViper")]
    HornedViper = 6508,
    [InspectorName("Animals/SpinyTailedLizard")]
    SpinyTailedLizard = 6509,
    [InspectorName("Animals/Wagtail")]
    Wagtail = 6510,
    [InspectorName("Animals/SaharaFrog")]
    SaharaFrog = 6511,

    //conditions
    Refurbished = 7000,
    Damaged = 7001,
    Stolen = 7002,
    CanStoreLiquid = 7003,
    IsDyeable = 7004,
    CanHoldGem = 7005,
    NoGem = 7006,
    UnDyed = 7007,

    LargePainting = 7008,
    MediumPainting = 7009,
    SmallPainting = 7010,

    //SPECIFIC ITEMS
    Oil = 9000,
    Coal = 9001,
    StoneItem = 9002,

    [InspectorName("Wood/PadaukLog")]
    PadaukLog = 9003,
    [InspectorName("Wood/MahoganyLog")]
    MahoganyLog = 9004,
    [InspectorName("Wood/AcaciaLog")]
    AcaciaLog = 9005,

    Tusk = 9006,
    [InspectorName("Metal/IronNugget")]
    IronNugget = 9007,
    [InspectorName("Metal/IronBar")]
    IronBar = 9008,
    [InspectorName("Metal/GoldNugget")]
    GoldNugget = 9009,
    [InspectorName("Metal/GoldBar")]
    GoldBar = 9010,
    [InspectorName("Metal/SilverBar")]
    SilverBar = 9011,
    [InspectorName("Metal/SilverNugget")]
    SilverNugget = 9012,
    [InspectorName("Metal/BrassNugget")]
    BrassNugget = 9013,
    [InspectorName("Metal/BrassBar")]
    BrassBar = 9014,

    Rope = 9015,
    StringItem = 9016,
    Seashell1 = 9017,
    Seashell2 = 9018,
    Seashell3 = 9019,
    ClothItem = 9020,
    ClothRoll = 9021,
    [InspectorName("Fabric/LeatherPiece")]
    LeatherPiece = 9022,
    [InspectorName("Fabric/Canvas")]
    Canvas = 9023,
    Lens = 9024,

    Cash = 9025,

    [InspectorName("Food/Orange")]
    Orange = 9026,
    [InspectorName("Food/Lemon")]
    Lemon = 9027,
    [InspectorName("Food/Date")]
    Date = 9028,
    [InspectorName("Food/Cacombara")]
    Cacombara = 9029,
    [InspectorName("Food/CoffeeBeans")]
    CoffeeBeans = 9030,

    [InspectorName("Food/Bread")]
    Bread = 9031,
    [InspectorName("Food/Rice")]
    Rice = 9032,
    [InspectorName("Food/Wheat")]
    Wheat = 9033,
    [InspectorName("Food/BeefItem")]
    BeefItem = 9034,
    [InspectorName("Food/MuttonItem")]
    MuttonItem = 9035,
    [InspectorName("Food/FishItem")]
    FishItem = 9036,
    [InspectorName("Food/Flour")]
    Flour = 9037,
    [InspectorName("Food/Butter")]
    Butter = 9038,
    [InspectorName("Food/Sugar")]
    Sugar = 9039,
    [InspectorName("Food/Milk")]
    Milk = 9040,
    [InspectorName("Food/Tomatoes")]
    Tomatoes = 9041,
    [InspectorName("Food/Eggplant")]
    Eggplant = 9042,
    [InspectorName("Food/Pepper")]
    Pepper = 9043,
    [InspectorName("Food/GrainSpirit")]
    GrainSpirit = 9044,

    [InspectorName("Food/Pancakes")]
    Pancakes = 9045,
    [InspectorName("Food/FruitSalad")]
    FruitSalad = 9046,

    [InspectorName("Food/CuredFish")]
    CuredFish = 9047,
    [InspectorName("Food/MintLamb")]
    MintLamb = 9048,
    [InspectorName("Food/BeefStew")]
    BeefStew = 9049,
    [InspectorName("Food/RiceBowl")]
    RiceBowl = 9050,
    [InspectorName("Food/CousCous")]
    CousCous = 9051,

    [InspectorName("Food/MintItem")]
    MintItem = 9052,
    [InspectorName("Food/LavenderItem")]
    LavenderItem = 9053,
    [InspectorName("Food/CinammonItem")]
    CinnammonItem = 9054,
    [InspectorName("Food/Sage")]
    Sage = 9055,
    [InspectorName("Food/Wormwood")]
    Wormwood = 9056,
    [InspectorName("Food/Tea")]
    Tea = 9057,

    [InspectorName("Food/TeaDrink")]
    TeaDrink = 9058,
    [InspectorName("Food/CoffeeDrink")]
    CoffeeDrink = 9059,


    [InspectorName("Food/Salt")]
    Salt = 9060,
    [InspectorName("Food/Peppercorns")]
    Peppercorns = 9061,
    [InspectorName("Food/Saffron")]
    Saffron = 9062,
    [InspectorName("Food/Ginger")]
    Ginger = 9063,
    [InspectorName("Food/Cloves")]
    Cloves = 9064,

    FulaniHat = 9065,
    WideBrimmedHat = 9066,

    [InspectorName("Books/Metalworking")]
    MetalworkingBook,
    [InspectorName("Books/FabricCraftsBook")]
    FabricCraftsBook,
    [InspectorName("Books/PotteryBook")]
    PotteryBook,
    [InspectorName("Books/BrewingBook")]
    BrewingBook,
    [InspectorName("Books/DyemakingBook")]
    DyemakingBook,
    [InspectorName("Books/RecipeBook")]
    RecipeBook,

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
    KnowledgeBook,
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