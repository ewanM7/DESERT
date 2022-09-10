using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public Inventory _Inventory;

    private void Awake()
    {
        _Inventory = new Inventory(50);
    }

    /// <summary>
    /// Testing function to fill the house with random items
    /// </summary>
    public void GenerateRandomItems()
    {

    }
}
