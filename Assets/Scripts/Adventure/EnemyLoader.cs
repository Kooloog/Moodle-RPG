using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyLoader : MonoBehaviour
{
    public static List<GameObject> enemyImageSlots;
    public static List<GameObject> enemyInfoScreens;

    public static void loadImageSlots()
    {
        enemyImageSlots = new List<GameObject>();
        enemyInfoScreens = new List<GameObject>();

        foreach (Transform imageSlot in GameObject.Find("EnemigosList").transform)
        {
            if (imageSlot.childCount > 0)
            {
                GameObject enemyImage = imageSlot.GetChild(0).gameObject;
                enemyImageSlots.Add(enemyImage);
                enemyInfoScreens.Add(enemyImage.transform.GetChild(0).gameObject);
            }
        }

        foreach (GameObject g in enemyInfoScreens) g.SetActive(false);
        foreach (GameObject g in enemyImageSlots) g.SetActive(false); 
    }

    public static void loadEnemies()
    {
        foreach (GameObject g in enemyImageSlots) g.SetActive(false);

        Enemy[] enemyList = EnemyLists.levelsFinal[Stats.mapLevel - 1].enemies;
        
        for(int i = 0; i < enemyList.Length; i++)
        {
            enemyImageSlots[i].SetActive(true);
            enemyImageSlots[i].GetComponent<Image>().sprite = enemyList[i].sprite;
            enemyInfoScreens[i].SetActive(true);

            //1: Nombre
            //2: Ataque
            //3: Defensa
            //4: Vida
            enemyInfoScreens[i].transform.GetChild(1).gameObject.GetComponent<Text>().text =
                enemyList[i].enemyName;
            enemyInfoScreens[i].transform.GetChild(2).gameObject.GetComponent<Text>().text =
                "Ataque: " + enemyList[i].attack;
            enemyInfoScreens[i].transform.GetChild(3).gameObject.GetComponent<Text>().text =
                "Defensa: " + enemyList[i].defense;
            enemyInfoScreens[i].transform.GetChild(4).gameObject.GetComponent<Text>().text =
                "Vida: " + enemyList[i].health;

            enemyInfoScreens[i].SetActive(false);
        }
    }
}
