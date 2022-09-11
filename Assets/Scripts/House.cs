using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public Inventory _Inventory;

    private void Start()
    {
        _Inventory = new Inventory(50);
        GenerateRandomItems();
    }

    /// <summary>
    /// Testing function to fill the house with random items
    /// </summary>
    public void GenerateRandomItems()
    {
        Item[] items = GameManager.Instance._NPCGenerationData.RandomItemsForHouse(20);

        foreach(Item item in items)
        {
            _Inventory.AddItem(item);
        }
    }
}
