using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            case SellerType.SecondHand:

                break;

            case SellerType.Scholar:

                break;
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
}



public class BuyItemNumberTableEntry
{
    public int MaxBuyItemNumber;
    public float Chance;
}

[System.Serializable]
public class ItemTableEntry
{
    public BaseItemData ItemData;
    public float Chance;
}