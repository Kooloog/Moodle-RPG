using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Shield : Object
{
    public Shield(Shield s, int iId)
    {
        id = iId;
        shieldName = s.shieldName;
        defense = s.defense;
        uses = s.uses;
        usesLeft = s.usesLeft;
        cost = s.cost;
        sprite = s.sprite;
    }

    public int id;
    public string shieldName;
    public int defense;
    public int uses;
    public int usesLeft;
    public int cost;
    public Sprite sprite;
}
