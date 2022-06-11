using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Shield(Shield s)
    {
        shieldName = s.shieldName;
        defense = s.defense;
        uses = s.uses;
        usesLeft = s.usesLeft;
        cost = s.cost;
        sprite = s.sprite;
    }

    public string shieldName;
    public int defense;
    public int uses;
    public int usesLeft;
    public int cost;
    public Sprite sprite;
}
