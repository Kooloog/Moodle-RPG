using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] public class Sword
{
    public Sword(Sword s)
    {
        swordName = s.swordName;
        attack = s.attack;
        uses = s.uses;
        usesLeft = s.usesLeft;
        sprite = s.sprite;
    }

    public string swordName;
    public int attack;
    public int uses;
    public int usesLeft;
    public Sprite sprite;
}
