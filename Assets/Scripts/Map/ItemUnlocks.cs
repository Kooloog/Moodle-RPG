using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUnlocks : MonoBehaviour
{
    public static GameObject swords;
    public static GameObject shields;
    public static GameObject items;

    public int[] swordExpUnlocks;
    public int[] shieldExpUnlocks;
    public int[] itemExpUnlocks;

    public static int[] swordExpUnlocksFinal;
    public static int[] shieldExpUnlocksFinal;
    public static int[] itemExpUnlocksFinal;

    void Start()
    {
        swords = GameObject.Find("MenuAtaque");
        shields = GameObject.Find("MenuDefensa");
        items = GameObject.Find("MenuObjetos");

        swordExpUnlocksFinal = swordExpUnlocks;
        shieldExpUnlocksFinal = shieldExpUnlocks;
        itemExpUnlocksFinal = itemExpUnlocks;
    }

    public static void shopLockItems()
    {
        for (int i = 0; i < swords.transform.childCount; i++)
        {
            if (Stats.score < swordExpUnlocksFinal[i])
            {
                swords.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < shields.transform.childCount; i++)
        {
            if (Stats.score < shieldExpUnlocksFinal[i])
            {
                shields.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < items.transform.childCount; i++)
        {
            if (Stats.score < itemExpUnlocksFinal[i])
            {
                items.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
