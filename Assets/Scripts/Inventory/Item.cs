using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Item
{
    public Item(Item i)
    {
        itemName = i.itemName;
        cost = i.cost;
        sprite = i.sprite;
    }

    public string itemName;
    public int cost;
    public Sprite sprite;
}
