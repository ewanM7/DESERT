using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public ItemDescriptor[] AllDescriptors
    {
        get
        {
            return MaterialDescriptors.Concat(SubDescriptors).ToArray();
        }
    }

    public ItemDescriptor[] MaterialDescriptors;
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
    [InspectorName("Clothing/Gloves")]
    Gloves = 1000,
    [InspectorName("Clothing/Headscarf")]
    Headscarf = 1001,
    [InspectorName("Clothing/Robes")]
    Robes = 1002,
    [InspectorName("Clothing/Shoe")]
    Shoe = 1003,
    [InspectorName("Clothing/Sandals")]
    Sandals = 1004,
    [InspectorName("Clothing/Bag")]
    Bag = 1005,
    [InspectorName("Clothing/Hat")]
    Hat = 1006,
    [InspectorName("Clothing/Dress")]
    Dress = 1007,
    [InspectorName("Clothing/Glasses")]
    Glasses = 1008,
    [InspectorName("Clothing/Goggles")]
    Goggles = 1009,

    //jewelry
    [InspectorName("Jewelry/Ring")]
    Ring = 3000,
    [InspectorName("Jewelry/Bracelet")]
    Bracelet = 3001,
    [InspectorName("Jewelry/Necklace")]
    Necklace = 3002,
    [InspectorName("Jewelry/Earrings")]
    Earrings = 3003,
    [InspectorName("Jewelry/Crown")]
    Crown = 3004,
    [InspectorName("Jewelry/Watch")]
    Watch = 3005,

    //tools
    [InspectorName("Tools/Sword")]
    Sword = 4000,
    [InspectorName("Tools/Shovel")]
    Shovel = 4001,
    [InspectorName("Tools/Spear")]
    Spear = 4002,
    [InspectorName("Tools/Axe")]
    Axe = 4004,
    [InspectorName("Tools/Spyglass")]
    Spyglass = 4005,
    [InspectorName("Tools/Compass")]
    Compass = 4006,
    [InspectorName("Tools/Staff")]
    Staff = 4009,
    [InspectorName("Tools/Bowl")]
    Bowl = 4010,
    [InspectorName("Tools/Cup")]
    Cup = 4011,
    [InspectorName("Tools/Bottle")]
    Bottle = 4012,
    [InspectorName("Tools/Lantern")]
    Lantern = 4013,
    [InspectorName("Tools/Lockbox")]
    Lockbox = 4014,
    [InspectorName("Tools/Canteen")]
    Canteen = 4016,

    //decorative
    [InspectorName("Decorative/Pottery")]
    Pottery = 4500,
    [InspectorName("Decorative/Vase")]
    Vase = 4501,
    [InspectorName("Decorative/Urn")]
    Urn = 4502,
    [InspectorName("Decorative/Jug")]
    Jug = 4503,
    [InspectorName("Decorative/Rug")]
    Rug = 4504,
    [InspectorName("Decorative/Painting")]
    Painting = 4505,
    [InspectorName("Decorative/Flag")]
    Flag = 4506,
    [InspectorName("Decorative/Carving")]
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
    [InspectorName("Metal/Metal")]
    Metal = 6029,
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
    Bone = 6010,
    [InspectorName("Gem/Gem")]
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

    [InspectorName("Gem/Ruby")]
    Ruby = 6020,
    [InspectorName("Gem/Emerald")]
    Emerald = 6021,
    [InspectorName("Gem/Pearl")]
    Pearl = 6022,
    [InspectorName("Gem/Sapphire")]
    Sapphire = 6023,
    [InspectorName("Gem/Amethyst")]
    Amethyst = 6024,
    [InspectorName("Gem/Amber")]
    Amber = 6025,
    [InspectorName("Gem/Seashell")]
    Seashell = 6026,

    Glass = 6028,

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
    [InspectorName("Gem/No Gem")]
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

    [InspectorName("Food/Water")]
    Water = 9067,

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
    None = 0,
    Food = 1,
    Clothing = 2,
    Animal = 3,
    Jewelry = 4,
    Tool = 6,
    RawMaterial = 7,
    Decorative = 8,
    KnowledgeBook = 9,
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