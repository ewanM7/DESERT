using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Item", menuName = "Data/NPC Generation Data")]
public class NPCGenerationData : ScriptableObject
{
    public BuyItemNumberTableEntry[] MaxBuyItemNumberTable;

    public ItemTableEntry[] HerbAndSpiceSellerItems;
    public ItemTableEntry[] FoodSellerItems;
    public ItemTableEntry[] MiningSellerItems;
    public ItemTableEntry[] AnimalSellerItems;
    public ItemTableEntry[] FabricsSellerItems;
    public ItemTableEntry[] NaturalsSellerItems;
    public BaseItemData[] ScholarItems;

    public ItemCategoryTableEntry[] BuyerItemCategories;

    public ItemDescriptor[] BuyerJewelryItems;
    public ItemDescriptor[] BuyerClothingItems;
    public ItemDescriptor[] BuyerFoodItems;
    public ItemDescriptor[] BuyerToolItems;
    public ItemDescriptor[] BuyerDecorativeItems;

    public Item[] RandomItemsForBuyer()
    {
        Item[] items;

        float roll = Random.Range(0f, 100f);

        if (roll < 33f)
        {
            //no items
            return new Item[0];

        }
        else if(roll < 67f)
        {
            //one items
            items = new Item[1];
        }
        else
        {
            //two items
            items = new Item[2];
        }

        for(int i = 0; i < items.Length; i++)
        {
            items[i] = RandomItemForBuyerCategory(GetBuyerItemCategory());
        }


        return items;
    }

    public Item RandomItemForBuyerCategory(ItemCategory category)
    {
        ItemDescriptor descriptor = GetBuyerItemDescriptor(category);

        switch (category)
        {
            case ItemCategory.RawMaterial:
                return new Item(GameManager.Instance._ItemDatabase.RawMaterialData[Random.Range(0, GameManager.Instance._ItemDatabase.RawMaterialData.Length)], -1, 0);

            case ItemCategory.Animal:
                return new Item(GameManager.Instance._ItemDatabase.AnimalsData[Random.Range(0, GameManager.Instance._ItemDatabase.AnimalsData.Length)], Random.Range(1, 11));

            case ItemCategory.Jewelry:
                BaseItemData data = null;
                switch (descriptor)
                {
                    case ItemDescriptor.Bracelet:
                        data = GameManager.Instance._ItemDatabase.BraceletData;
                        break;

                    case ItemDescriptor.Necklace:
                        data = GameManager.Instance._ItemDatabase.NecklaceData;
                        break;

                    case ItemDescriptor.Ring:
                        data = GameManager.Instance._ItemDatabase.RingData;
                        break;

                    case ItemDescriptor.Earrings:
                        data = GameManager.Instance._ItemDatabase.EarringsData;
                        break;

                    case ItemDescriptor.Watch:
                        data = GameManager.Instance._ItemDatabase.WatchData;
                        break;
                }

                ItemDescriptor[] descriptors;
                int quality = Random.Range(1, 13);

                //50% chance for item to be damaged if it's 1 or 2 star quality
                if (quality < 9 && Random.Range(0, 2) == 1)
                {
                    descriptors = new ItemDescriptor[3];
                    descriptors[2] = ItemDescriptor.Damaged;
                }
                else
                {
                    descriptors = new ItemDescriptor[2];
                }

                descriptors[0] = data.MaterialDescriptors[Random.Range(0, data.MaterialDescriptors.Length)];

                if (descriptors[0] == ItemDescriptor.Padauk || descriptors[0] == ItemDescriptor.Mahogany || descriptors[0] == ItemDescriptor.Acacia || descriptors[0] == ItemDescriptor.Bone || descriptor == ItemDescriptor.Watch)
                {
                    descriptors[1] = ItemDescriptor.NoGem;
                    return new JewelryItem(data, descriptors, new Item[] { }, quality);
                }
                else if (descriptors[0] == ItemDescriptor.String)
                {
                    descriptors[1] = ItemDescriptor.Seashell;
                    return new JewelryItem(data, descriptors, new Item[] { }, quality);
                }
                else
                {
                    descriptors[1] = data.SubDescriptors[Random.Range(0, data.SubDescriptors.Length)];
                    return new JewelryItem(data, descriptors, new Item[] { new Item(GameManager.Instance._ItemDatabase.GemDataForDescriptor(descriptors[1]), -1, 0) }, quality);
                }

            case ItemCategory.Clothing:
                data = null;
                switch(descriptor)
                {
                    case ItemDescriptor.Bag:
                        data = GameManager.Instance._ItemDatabase.BagData;
                        break;

                    case ItemDescriptor.Dress:
                        data = GameManager.Instance._ItemDatabase.DressData;
                        break;

                    case ItemDescriptor.Glasses:
                        data = GameManager.Instance._ItemDatabase.GlassesData;
                        break;

                    case ItemDescriptor.Gloves:
                        data = GameManager.Instance._ItemDatabase.GlovesData;
                        break;

                    case ItemDescriptor.Goggles:
                        data = GameManager.Instance._ItemDatabase.GogglesData;
                        break;

                    case ItemDescriptor.Headscarf:
                        data = GameManager.Instance._ItemDatabase.HeadscarfData;
                        break;

                    case ItemDescriptor.Robes:
                        data = GameManager.Instance._ItemDatabase.RobesData;
                        break;

                    case ItemDescriptor.Sandals:
                        data = GameManager.Instance._ItemDatabase.SandalsData;
                        break;

                }

                quality = Random.Range(1, 13);

                //50% chance for item to be damaged if it's 1 or 2 star quality
                bool isDamaged = quality < 9 && Random.Range(0, 2) == 1;

                if (data.BaseDescriptors.Contains(ItemDescriptor.IsDyeable))
                {
                    if (isDamaged)
                    {
                        descriptors = new ItemDescriptor[3];
                        descriptors[2] = ItemDescriptor.Damaged;
                    }
                    else
                    {
                        descriptors = new ItemDescriptor[2];
                    }
                }
                else
                {
                    if (isDamaged)
                    {
                        descriptors = new ItemDescriptor[2];
                        descriptors[1] = ItemDescriptor.Damaged;
                    }
                    else
                    {
                        descriptors = new ItemDescriptor[1];
                    }
                }

                descriptors[0] = data.MaterialDescriptors[Random.Range(0, data.MaterialDescriptors.Length)];

                if (!data.BaseDescriptors.Contains(ItemDescriptor.IsDyeable))
                {
                    return new ClothingItem(data, descriptors, new Item[0], quality);
                }
                else
                {
                    descriptors[1] = GameManager.Instance._ItemDatabase.DyeDescriptors[Random.Range(0, GameManager.Instance._ItemDatabase.DyeDescriptors.Count)];
                    return new ClothingItem(data, descriptors, new Item[] { new Item(GameManager.Instance._ItemDatabase.DyeDataForDescriptor(descriptors[1]), -1, 0) }, quality);
                }
        }

        if (category == ItemCategory.RawMaterial)
        {
            
        }
        else if (category == ItemCategory.Animal)
        {
            
        }
        else
        {
            

            switch (descriptor)
            {
                //Clothing
                case ItemDescriptor.Bag:

                case ItemDescriptor.Dress:

                case ItemDescriptor.Glasses:

                case ItemDescriptor.Gloves:

                case ItemDescriptor.Goggles:

                case ItemDescriptor.Headscarf:

                case ItemDescriptor.Robes:

                case ItemDescriptor.Sandals:

                //decorative
                case ItemDescriptor.Urn:

                case ItemDescriptor.Jug:

                case ItemDescriptor.Flag:

                case ItemDescriptor.Carving:

                //food
                case ItemDescriptor.Herb:

                case ItemDescriptor.Spice:

                case ItemDescriptor.Sweet:

                case ItemDescriptor.Meal:

                case ItemDescriptor.Alcohol:

                //tools
                case ItemDescriptor.Axe:
                case ItemDescriptor.Shovel:
                case ItemDescriptor.Spear:

                case ItemDescriptor.Sword:

                case ItemDescriptor.Bottle:

                case ItemDescriptor.Bowl:
                case ItemDescriptor.Cup:
                case ItemDescriptor.Staff:

                case ItemDescriptor.Lantern:
                case ItemDescriptor.Lockbox:

                case ItemDescriptor.Compass:
                case ItemDescriptor.Spyglass:
                    break;

            }
        }


        return null;
    }

    public BaseItemData[] ItemsForSellerType(SellerType type)
    {
        List<BaseItemData> dataList = new List<BaseItemData>();


        switch (type)
        {
            case SellerType.HerbsAndSpices:
                for(int i = 0; i < 9; i++)
                {
                    dataList.Add(GetSellerItem(HerbAndSpiceSellerItems));
                }
                break;

            case SellerType.Food:
                for (int i = 0; i < 9; i++)
                {
                    dataList.Add(GetSellerItem(FoodSellerItems));
                }
                break;

            case SellerType.Mining:
                for (int i = 0; i < 9; i++)
                {
                    dataList.Add(GetSellerItem(MiningSellerItems));
                }
                break;

            case SellerType.Animals:
                for (int i = 0; i < 9; i++)
                {
                    dataList.Add(GetSellerItem(AnimalSellerItems));
                }
                break;

            case SellerType.Fabrics:
                for (int i = 0; i < 9; i++)
                {
                    dataList.Add(GetSellerItem(FabricsSellerItems));
                }
                break;

            case SellerType.Naturals:
                for (int i = 0; i < 9; i++)
                {
                    dataList.Add(GetSellerItem(NaturalsSellerItems));
                }
                break;

            case SellerType.Scholar:
                return ScholarItems;
        }

        return dataList.ToArray();
    }

    public BaseItemData GetSellerItem(ItemTableEntry[] table)
    {
        float cumulativeProbability = 0;
        float roll = Random.Range(0, 1f);

        for(int i = 0; i < table.Length; i++)
        {
            cumulativeProbability += table[i].Chance;

            if(roll <= cumulativeProbability)
            {
                return table[i].ItemData;
            }
        }


        Debug.LogWarning("SALES ITEM TABLE RETURNED NULL");
        return null;
    }

    public int GetMaxBuyItemNumber()
    {
        float cumulativeProbability = 0;
        float roll = Random.Range(0, 1f);

        for(int i = 0; i < MaxBuyItemNumberTable.Length; i++)
        {
            cumulativeProbability += MaxBuyItemNumberTable[i].Chance;

            if(roll <= cumulativeProbability)
            {
                return MaxBuyItemNumberTable[i].MaxBuyItemNumber;
            }
        }

        return 1;
    }

    private ItemCategory GetBuyerItemCategory()
    {
        float cumulativeProbability = 0;
        float roll = Random.Range(0, 1f);

        for (int i = 0; i < BuyerItemCategories.Length; i++)
        {
            cumulativeProbability += MaxBuyItemNumberTable[i].Chance;

            if (roll <= cumulativeProbability)
            {
                return BuyerItemCategories[i].Category;
            }
        }
        return ItemCategory.CATEGORY_MAX;
    }

    private ItemDescriptor GetBuyerItemDescriptor(ItemCategory category)
    {
        ItemDescriptor[] table = null;

        switch(category)
        {
            case ItemCategory.Jewelry:
                table = BuyerJewelryItems;
                break;

            case ItemCategory.Clothing:
                table = BuyerClothingItems;
                break;

            case ItemCategory.Food:
                table = BuyerFoodItems;
                break;

            case ItemCategory.Tool:
                table = BuyerToolItems;
                break;

            case ItemCategory.Decorative:
                table = BuyerDecorativeItems;
                break;
        }

        return table[Random.Range(0, table.Length)];
    }
}

[System.Serializable]
public class BuyItemNumberTableEntry
{
    public int MaxBuyItemNumber;
    public float Chance;
}

[System.Serializable]
public class ItemCategoryTableEntry
{
    public ItemCategory Category;
    public float Chance;
}

[System.Serializable]
public class ItemDescriptorTableEntry
{
    public ItemDescriptor Descriptor;
    public float Chance;
}

[System.Serializable]
public class ItemTableEntry
{
    public BaseItemData ItemData;
    public float Chance;
}