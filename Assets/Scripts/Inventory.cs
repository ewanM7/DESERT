using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public void IncreaseSize(int additionalSize)
    {
        Capacity += additionalSize;

        ItemStack[] stacks = new ItemStack[Capacity];

        ItemStacks.CopyTo(stacks, 0);

        ItemStacks = stacks;
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
            for (int i = 0; i < ItemStacks.Length; i++)
            {
                if (ItemStacks[i].count < MAX_STACK_SIZE)
                {
                    ItemStacks[i].count++;
                    addedToStack = true;
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

    /// <summary>
    /// Remove a single item from the inventory
    /// </summary>
    /// <param name="itemToRemove"></param>
    public void RemoveItem(Item itemToRemove)
    {
        for(int i = 0; i < ItemStacks.Length; i++)
        {
            if(itemToRemove == ItemStacks[i].item)
            {
                ItemStacks[i].item = null;
                return;
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