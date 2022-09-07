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

        ItemCategory buyerCategory = GetBuyerItemCategory();

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = RandomItemForBuyerCategory(buyerCategory);
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

                    case ItemDescriptor.Gloves:
                        data = GameManager.Instance._ItemDatabase.GlovesData;
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

            case ItemCategory.Decorative:
                data = null;

                switch(descriptor)
                {
                    case ItemDescriptor.Urn:
                        data = GameManager.Instance._ItemDatabase.UrnData;
                        break;

                    case ItemDescriptor.Jug:
                        data = GameManager.Instance._ItemDatabase.JugData;
                        break;

                    case ItemDescriptor.Vase:
                        data = GameManager.Instance._ItemDatabase.VaseData;
                        break;

                    case ItemDescriptor.Carving:
                        data = GameManager.Instance._ItemDatabase.CarvingData;
                        break;

                }

                quality = Random.Range(1, 13);

                //50% chance for item to be damaged if it's 1 or 2 star quality
                isDamaged = quality < 9 && Random.Range(0, 2) == 1;

                if (data.BaseDescriptors[0] == ItemDescriptor.Carving)
                {
                    if (isDamaged)
                    {
                        descriptors = new ItemDescriptor[2];
                        descriptors[2] = ItemDescriptor.Damaged;
                    }
                    else
                    {
                        descriptors = new ItemDescriptor[1];
                    }

                    descriptors[0] = data.MaterialDescriptors[Random.Range(0, data.MaterialDescriptors.Length)];
                }
                else
                {
                    if (data.BaseDescriptors.Contains(ItemDescriptor.IsDyeable))
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
                    else
                    {
                        if (isDamaged)
                        {
                            descriptors = new ItemDescriptor[1];
                            descriptors[0] = ItemDescriptor.Damaged;
                        }
                        else
                        {
                            descriptors = null;
                        }
                    }
                }

                if (!data.BaseDescriptors.Contains(ItemDescriptor.IsDyeable))
                {
                    return new DecorativeItem(data, descriptors, new Item[0], quality);
                }
                else
                {
                    descriptors[0] = GameManager.Instance._ItemDatabase.DyeDescriptors[Random.Range(0, GameManager.Instance._ItemDatabase.DyeDescriptors.Count)];

                    if(descriptors[0] == ItemDescriptor.UnDyed)
                    {
                        return new DecorativeItem(data, descriptors, null, quality);
                    }
                    else
                    {
                        return new DecorativeItem(data, descriptors, new Item[] { new Item(GameManager.Instance._ItemDatabase.DyeDataForDescriptor(descriptors[0]), -1, 0) }, quality);
                    }

                    
                }

            case ItemCategory.Food:
                data = null;
                switch (descriptor)
                {
                    //food
                    case ItemDescriptor.Herb:
                        data = GameManager.Instance._ItemDatabase.HerbData[Random.Range(0, GameManager.Instance._ItemDatabase.HerbData.Length)];
                        break;

                    case ItemDescriptor.Spice:
                        data = GameManager.Instance._ItemDatabase.SpiceData[Random.Range(0, GameManager.Instance._ItemDatabase.SpiceData.Length)];
                        break;

                    case ItemDescriptor.Fruit:
                        data = GameManager.Instance._ItemDatabase.FruitData[Random.Range(0, GameManager.Instance._ItemDatabase.FruitData.Length)];
                        break;

                    case ItemDescriptor.Vegetable:
                        data = GameManager.Instance._ItemDatabase.VegetableData[Random.Range(0, GameManager.Instance._ItemDatabase.VegetableData.Length)];
                        break;

                    case ItemDescriptor.Meat:
                        data = GameManager.Instance._ItemDatabase.MeatData[Random.Range(0, GameManager.Instance._ItemDatabase.MeatData.Length)];
                        break;

                    case ItemDescriptor.Sugar:
                        data = GameManager.Instance._ItemDatabase.SugarData;
                        break;

                    case ItemDescriptor.Milk:
                        data = GameManager.Instance._ItemDatabase.MilkData;
                        break;

                    case ItemDescriptor.Water:
                        data = GameManager.Instance._ItemDatabase.WaterData;
                        break;

                    case ItemDescriptor.Bread:
                        data = GameManager.Instance._ItemDatabase.BreadData;
                        break;
                }

                return new FoodItem(data, new ItemDescriptor[0], -1);

            case ItemCategory.Tool:
                data = null;
                switch(descriptor)
                {
                    case ItemDescriptor.Axe:
                        data = GameManager.Instance._ItemDatabase.AxeData;
                        break;

                    case ItemDescriptor.Shovel:
                        data = GameManager.Instance._ItemDatabase.ShovelData;
                        break;

                    case ItemDescriptor.Spear:
                        data = GameManager.Instance._ItemDatabase.SpearData;
                        break;

                    case ItemDescriptor.Sword:
                        data = GameManager.Instance._ItemDatabase.SwordData;
                        break;

                    case ItemDescriptor.Bottle:
                        data = GameManager.Instance._ItemDatabase.BottleData;
                        break;

                    case ItemDescriptor.Bowl:
                        data = GameManager.Instance._ItemDatabase.BowlData;
                        break;

                    case ItemDescriptor.Cup:
                        data = GameManager.Instance._ItemDatabase.CupData;
                        break;

                    case ItemDescriptor.Staff:
                        data = GameManager.Instance._ItemDatabase.StaffData;
                        break;

                    case ItemDescriptor.Lantern:
                        data = GameManager.Instance._ItemDatabase.LanternData;
                        break;

                    case ItemDescriptor.Lockbox:
                        data = GameManager.Instance._ItemDatabase.LockboxData;
                        break;
                }

                quality = Random.Range(1, 13);

                //50% chance for item to be damaged if it's 1 or 2 star quality
                if (quality < 9 && Random.Range(0, 2) == 1)
                {
                    descriptors = new ItemDescriptor[2];
                    descriptors[1] = ItemDescriptor.Damaged;
                }
                else
                {
                    descriptors = new ItemDescriptor[1];
                }

                descriptors[0] = data.MaterialDescriptors[Random.Range(0, data.MaterialDescriptors.Length)];
                return new ToolItem(data, descriptors, quality);
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


        Debug.Log("SALES ITEM TABLE RETURNED NULL");
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

        if(table == null)
        {
            return ItemDescriptor.None;
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