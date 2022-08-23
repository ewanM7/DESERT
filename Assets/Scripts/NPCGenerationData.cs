using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/NPC Generation Data")]
public class NPCGenerationData : ScriptableObject
{
    public BuyItemNumberTableEntry[] MaxBuyItemNumberTable;



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


[System.Serializable]
public class BuyItemNumberTableEntry
{
    public int MaxBuyItemNumber;
    public float Chance;
}