using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Item : Object
{
    public Item(Item i, int iId)
    {
        id = iId;
        itemName = i.itemName;
        cost = i.cost;
        sprite = i.sprite;
    }

    public int id;
    public string itemName;
    public int cost;
    public Sprite sprite;
}
