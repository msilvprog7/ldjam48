using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
        AddItem(new Item {name = "Shirt", desc = "A shirt, lovingly-stitched by your grandma"});
        AddItem(new Item {name = "Pants", desc = "Pants, the only thing that separates men from beasts"});
        AddItem(new Item {name = "Sword", desc = "Slicing, stabbing - what more could you want?"});
        //Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
