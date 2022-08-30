using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public BaseItemData Cash;

    public AnimationCurve QualityValueCurve;

    public BaseItemData BraceletData;
    public BaseItemData CrownData;
    public BaseItemData EarringsData;
    public BaseItemData NecklaceData;
    public BaseItemData RingData;

    public BaseItemData GlovesData;
    public BaseItemData HeadscarfData;
    public BaseItemData RobesData;
    public BaseItemData BagData;
    public BaseItemData DressData;

    public BaseItemData CarvingData;

    public BaseItemData[] AnimalsData;
    public BaseItemData[] RawMaterialData;

    public List<ItemCategory> NPCBuyCategories;

    public List<ItemDescriptor> FoodDescriptors;
    public List<ItemDescriptor> ClothingDescriptors;
    public List<ItemDescriptor> JewelryDescriptors;
    public List<ItemDescriptor> ToolDescriptors;
    public List<ItemDescriptor> DecorativeDescriptors;

    public List<ItemDescriptor> DyeDescriptors;
    public List<ItemDescriptor> HerbItems;
    public List<ItemDescriptor> SpiceItems;
    public List<ItemDescriptor> MealItems;
    public List<ItemDescriptor> SweetItems;
    public List<ItemDescriptor> AlcoholDescriptors;
    public List<ItemDescriptor> SpecificAnimalItems;

    public List<ItemDescriptor> FoodSubDescriptors;
    public List<ItemDescriptor> ClothingSubDescriptors;
    public List<ItemDescriptor> JewelrySubDescriptors;
    public List<ItemDescriptor> AnimalSubDescriptors;

    public ItemDescriptor RandomSubDescriptorInCategory(ItemCategory category)
    {
        switch (category)
        {
            case ItemCategory.Animal:
                return AnimalSubDescriptors[Random.Range(0, AnimalSubDescriptors.Count)];

            case ItemCategory.Food:
                return FoodSubDescriptors[Random.Range(0, FoodSubDescriptors.Count)];

            case ItemCategory.Clothing:
                ItemDescriptor[] final = ClothingSubDescriptors.Concat(DyeDescriptors).ToArray();
                return final[Random.Range(0, final.Length)];

            case ItemCategory.Decorative:
                return DyeDescriptors[Random.Range(0, DyeDescriptors.Count)];

            case ItemCategory.Jewelry:
                return JewelrySubDescriptors[Random.Range(0, JewelrySubDescriptors.Count)];

            default:
                return ItemDescriptor.None;
        }
    }

    public Item RandomItemInCategory(ItemCategory category)
    {
        Item item;

        if(category == ItemCategory.RawMaterial)
        {
            return new Item(RawMaterialData[Random.Range(0, RawMaterialData.Length)]);
        }
        else if(category == ItemCategory.Animal)
        {
            return new Item(AnimalsData[Random.Range(0, AnimalsData.Length)]);
        }

        ItemDescriptor descriptor = RandomDescriptorInCategory(category);

        return null;
    }

    public ItemDescriptor RandomDescriptorInCategory(ItemCategory category)
    {
        List<ItemDescriptor> descriptorList = DescriptorListForCategory(category);

        return descriptorList[Random.Range(0, descriptorList.Count)];
    }

    public ItemDescriptor[] RandomDescriptorsInCategory(ItemCategory category, int count)
    {
        ItemDescriptor[] descriptors = new ItemDescriptor[count];

        List<ItemDescriptor> descriptorList = DescriptorListForCategory(category);

        if(category == ItemCategory.Animal)
        {
            if (Random.Range(0, 2) == 1)
            {
                descriptors[0] = ItemDescriptor.Pet;
                return descriptors;
            }
            else
            {
                descriptors[0] = ItemDescriptor.Large;
                return descriptors;
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                ItemDescriptor toAdd;

                do
                {
                    toAdd = descriptorList[Random.Range(0, descriptorList.Count)];
                } while (descriptors.Contains(toAdd));

                descriptors[i] = toAdd;

            }
        }

        return descriptors;
    }

    private List<ItemDescriptor> DescriptorListForCategory(ItemCategory category)
    {
        switch (category)
        {
            case ItemCategory.Food:
                return FoodDescriptors;

            case ItemCategory.Clothing:
                return ClothingDescriptors;

            case ItemCategory.Jewelry:
                return JewelryDescriptors;

            case ItemCategory.Tool:
                return ToolDescriptors;

            case ItemCategory.Decorative:
                return DecorativeDescriptors;

            default:
                return FoodDescriptors;
        }
    }

    public ItemDescriptor[] SubDescriptorsForItem(ItemDescriptor descriptor)
    {

        switch (descriptor)
        {
            //Jewelry
            case ItemDescriptor.Bracelet:
                return BraceletData.AllDescriptors;

            case ItemDescriptor.Crown:
                return CrownData.AllDescriptors;

            case ItemDescriptor.Earrings:
                return EarringsData.AllDescriptors;

            case ItemDescriptor.Necklace:
                return NecklaceData.AllDescriptors;

            case ItemDescriptor.Ring:
                return RingData.AllDescriptors;

            //Clothing
            case ItemDescriptor.Bag:
                return BagData.SubDescriptors.Concat(DyeDescriptors).ToArray();

            case ItemDescriptor.Dress:
                return DressData.SubDescriptors.Concat(DyeDescriptors).ToArray();

            case ItemDescriptor.Hat:
                return new ItemDescriptor[0];

            case ItemDescriptor.Glasses:
                return new ItemDescriptor[0];

            case ItemDescriptor.Gloves:
                return GlovesData.SubDescriptors.Concat(DyeDescriptors).ToArray();

            case ItemDescriptor.Goggles:
                return new ItemDescriptor[0];

            case ItemDescriptor.Headscarf:
                return HeadscarfData.SubDescriptors.Concat(DyeDescriptors).ToArray();

            case ItemDescriptor.Robes:
                return RobesData.SubDescriptors.Concat(DyeDescriptors).ToArray();

            case ItemDescriptor.Sandals:
                return new ItemDescriptor[0];

            //decorative
            case ItemDescriptor.Vase:
                return DyeDescriptors.ToArray();

            case ItemDescriptor.Urn:
                return DyeDescriptors.ToArray();

            case ItemDescriptor.Jug:
                return DyeDescriptors.ToArray();

            case ItemDescriptor.Painting:
                return new ItemDescriptor[] { ItemDescriptor.LargePainting, ItemDescriptor.MediumPainting, ItemDescriptor.SmallPainting };

            case ItemDescriptor.Flag:
                return new ItemDescriptor[] { ItemDescriptor.White, ItemDescriptor.Black, ItemDescriptor.Red, ItemDescriptor.Green, ItemDescriptor.Blue, ItemDescriptor.Purple };

            case ItemDescriptor.Carving:
                return CarvingData.SubDescriptors.ToArray();

            //food
            case ItemDescriptor.Herb:
                return HerbItems.ToArray();

            case ItemDescriptor.Spice:
                return SpiceItems.ToArray();

            case ItemDescriptor.Sweet:
                return SweetItems.ToArray();

            case ItemDescriptor.Meal:
                return MealItems.ToArray();

            case ItemDescriptor.Alcohol:
                return AlcoholDescriptors.ToArray();

            //tools
            case ItemDescriptor.Axe:
            case ItemDescriptor.Shovel:
            case ItemDescriptor.Spear:
                return new ItemDescriptor[] { ItemDescriptor.Stone, ItemDescriptor.Metal, ItemDescriptor.Wood };

            case ItemDescriptor.Sword:
                return new ItemDescriptor[] { ItemDescriptor.Metal, ItemDescriptor.Wood };

            case ItemDescriptor.Bottle:
                return new ItemDescriptor[0];

            case ItemDescriptor.Bowl:
            case ItemDescriptor.Cup:
            case ItemDescriptor.Staff:
                return new ItemDescriptor[0];

            case ItemDescriptor.Lantern:
            case ItemDescriptor.Lockbox:
                return new ItemDescriptor[0];

            case ItemDescriptor.Compass:
            case ItemDescriptor.Spyglass:
                return new ItemDescriptor[] { ItemDescriptor.Silver, ItemDescriptor.Brass, ItemDescriptor.Gold };
        }

        return new ItemDescriptor[0];
    }

    public ItemDescriptor GetRandomAnimalItem()
    {
        return SpecificAnimalItems[Random.Range(0, SpecificAnimalItems.Count)];
    }

    public ItemDescriptor GetRandomToolItem()
    {
        return ToolDescriptors[Random.Range(0, ToolDescriptors.Count)];
    }

}

[System.Serializable]
public class MaterialMultiplier
{
    [SerializeField]
    public float Multiplier;
    [SerializeField]
    public ItemDescriptor Material;
}


