using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int Capacity;
    public ItemStack[] ItemStacks;

    public const int MAX_STACK_SIZE = 9;

    public Inventory(int Capacity)
    {
        this.Capacity = Capacity;

        ItemStacks = new ItemStack[Capacity];

        for (int i = 0; i < Capacity; i++)
        {
            ItemStacks[i] = new ItemStack();
        }
    }

    /// <summary>
    /// Get the index of the first available slot in the inventory. Returns -1 if the inventory is full
    /// </summary>
    /// <returns></returns>
    private int FreeSlot()
    {
        int z = 0;

        for (int i = z; i < ItemStacks.Length; i++)
        {
            if (ItemStacks[i].item == null)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Add an item to the inventory. Returns false if it cannot be added
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns></returns>
    public bool AddItem(Item itemToAdd)
    { 
        bool addedToStack = false;
        //add the item to a stack if there is a stack of the given item in the inventory already, and it is a stackable item
        /*
        if (itemToAdd.BaseItemData.IsStackable)
        {
            for (int i = z; i < ItemStacks.Length; i++)
            {
                //item id and quality must be the same in order for them to stack
                if (ItemStacks[i].item.BaseItemData.ID == itemToAdd.BaseItemData.ID && ItemStacks[i].item.Quality == itemToAdd.Quality)
                {
                    if (ItemStacks[i].count < MAX_STACK_SIZE)
                    {
                        ItemStacks[i].count++;
                        addedToStack = true;
                    }
                }
            }
        }*/

        if (addedToStack)
        {
            return true;
        }
        else
        {
            //if not added to a stack, add it to the first free slot. return false if the inventory is full

            int firstFreeIndex = FreeSlot();
            if (firstFreeIndex == -1)
            {
                return false;
            }
            else
            {
                ItemStacks[firstFreeIndex] = new ItemStack(itemToAdd, 1);
                return true;
            }
        }
    }
}

public class ItemStack
{
    public ItemStack(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    public ItemStack()
    {
        item = null;
        count = 0;
    }

    public Item item;
    public int count;
}