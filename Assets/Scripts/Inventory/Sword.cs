using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] public class Sword : Object
{
    public Sword(Sword s, int iId)
    {
        id = iId;
        swordName = s.swordName;
        attack = s.attack;
        uses = s.uses;
        usesLeft = s.usesLeft;
        cost = s.cost;
        sprite = s.sprite;
    }

    public int id;
    public string swordName;
    public int attack;
    public int uses;
    public int usesLeft;
    public int cost;
    public Sprite sprite;
}
