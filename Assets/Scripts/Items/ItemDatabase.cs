using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<BaseItemData> AllItems;

    public BaseItemData Cash;

    public AnimationCurve QualityValueCurve;


    public List<ItemCategory> NPCBuyCategories;

    public List<ItemDescriptor> FoodDescriptors;
    public List<ItemDescriptor> ClothingDescriptors;
    public List<ItemDescriptor> JewelryDescriptors;
    public List<ItemDescriptor> ToolDescriptors;
    public List<ItemDescriptor> DecorativeDescriptors;

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

    public ItemDescriptor RandomSubdescriptorForDescriptor(ItemDescriptor descriptor)
    {
        return ItemDescriptor.Acacia;
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


