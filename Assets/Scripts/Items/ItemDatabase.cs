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

    public List<ItemCategory> NPCBuyCategories;

    public List<ItemDescriptor> FoodDescriptors;
    public List<ItemDescriptor> ClothingDescriptors;
    public List<ItemDescriptor> JewelryDescriptors;
    public List<ItemDescriptor> ToolDescriptors;
    public List<ItemDescriptor> DecorativeDescriptors;

    public List<ItemDescriptor> DyeDescriptors;
    public List<ItemDescriptor> HerbAndSpiceItems;
    public List<ItemDescriptor> MealAndSweetItems;
    public List<ItemDescriptor> AlcoholDescriptors;
    public List<ItemDescriptor> SpecificAnimalItems;

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
                return BraceletData.SubDescriptors;

            case ItemDescriptor.Crown:
                return CrownData.SubDescriptors;

            case ItemDescriptor.Earrings:
                return CrownData.SubDescriptors;

            case ItemDescriptor.Necklace:
                return NecklaceData.SubDescriptors;

            case ItemDescriptor.Ring:
                return RingData.SubDescriptors;

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

            case ItemDescriptor.Vase:
                return DyeDescriptors.ToArray();

            case ItemDescriptor.Urn:
                return DyeDescriptors.ToArray();

            case ItemDescriptor.Jug:
                return DyeDescriptors.ToArray();

            case ItemDescriptor.Painting:
                return new ItemDescriptor[] { ItemDescriptor.LargePainting, ItemDescriptor.MediumPainting, ItemDescriptor.SmallPainting };

            case ItemDescriptor.Flag:
                return new ItemDescriptor[] { ItemDescriptor.White, ItemDescriptor.Black, ItemDescriptor.Red, ItemDescriptor.Green, ItemDescriptor.Blue, ItemDescriptor.Purple, ItemDescriptor.Yellow };

            case ItemDescriptor.Carving:
                return CarvingData.SubDescriptors.ToArray();

            case ItemDescriptor.Herb:
            case ItemDescriptor.Spice:
                return HerbAndSpiceItems.ToArray();

            case ItemDescriptor.Sweet:
            case ItemDescriptor.Meal:
                return MealAndSweetItems.ToArray();

            case ItemDescriptor.Alcohol:
                return AlcoholDescriptors.ToArray();
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


