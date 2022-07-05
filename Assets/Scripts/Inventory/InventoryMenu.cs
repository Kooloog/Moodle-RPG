using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public static GameObject objectIcons;

    public static GameObject swordsButton;
    public static GameObject shieldsButton;
    public static GameObject itemsButton;

    private static AudioSource menuSound;
    public static bool playSound;

    // Start is called before the first frame update
    void Start()
    {
        objectIcons = GameObject.Find("ObjetosInventory");

        swordsButton = GameObject.Find("EspadasButton");
        shieldsButton = GameObject.Find("EscudosButton");
        itemsButton = GameObject.Find("ObjetosButton");

        menuSound = GameObject.Find("MenuSelect").GetComponent<AudioSource>();

        //El menú de inventario debe desactivarse aquí para prevenir errores
        GameObject.Find("InventoryCanvas").SetActive(false);
    }

    public static void swordInventory()
    {
        if(playSound) menuSound.Play();

        int currentElement = 0;
        foreach(Transform objectIcon in objectIcons.transform)
        {
            if(currentElement < InventoryManager.swords.Count)
            {
                objectIcon.GetComponent<Button>().interactable = true;
                objectIcon.GetChild(0).GetComponent<Image>().sprite = 
                    InventoryManager.swords[currentElement].sprite;
                objectIcon.GetChild(1).GetComponent<Text>().text = 
                    InventoryManager.swords[currentElement].usesLeft.ToString();
            }
            else
            {
                objectIcon.GetComponent<Button>().interactable = false;
                objectIcon.GetChild(0).GetComponent<Image>().sprite = null;
                objectIcon.GetChild(1).GetComponent<Text>().text = "";
            }

            ++currentElement;
        }

        swordsButton.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
        shieldsButton.GetComponent<Image>().color = new Color(255, 255, 255);
        itemsButton.GetComponent<Image>().color = new Color(255, 255, 255);

        if(GameObject.Find("MenuActualInventory") != null)
            GameObject.Find("MenuActualInventory").GetComponent<Text>().text = "ESPADAS";
    }

    public static void shieldInventory()
    {
        menuSound.Play();

        int currentElement = 0;
        foreach (Transform objectIcon in objectIcons.transform)
        {
            if (currentElement < InventoryManager.shields.Count)
            {
                objectIcon.GetComponent<Button>().interactable = true;
                objectIcon.GetChild(0).GetComponent<Image>().sprite =
                    InventoryManager.shields[currentElement].sprite;
                objectIcon.GetChild(1).GetComponent<Text>().text =
                    InventoryManager.shields[currentElement].usesLeft.ToString();
            }
            else
            {
                objectIcon.GetComponent<Button>().interactable = false;
                objectIcon.GetChild(0).GetComponent<Image>().sprite = null;
                objectIcon.GetChild(1).GetComponent<Text>().text = "";
            }

            ++currentElement;
        }

        swordsButton.GetComponent<Image>().color = new Color(255, 255, 255);
        shieldsButton.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
        itemsButton.GetComponent<Image>().color = new Color(255, 255, 255);

        GameObject.Find("MenuActualInventory").GetComponent<Text>().text = "ESCUDOS";
    }

    public static void itemInventory()
    {
        menuSound.Play();

        int currentElement = 0;
        foreach (Transform objectIcon in objectIcons.transform)
        {
            if (currentElement < InventoryManager.items.Count)
            {
                objectIcon.GetComponent<Button>().interactable = true;
                objectIcon.GetChild(0).GetComponent<Image>().sprite =
                    InventoryManager.items[currentElement].sprite;
                objectIcon.GetChild(1).GetComponent<Text>().text = "";
            }
            else
            {
                objectIcon.GetComponent<Button>().interactable = false;
                objectIcon.GetChild(0).GetComponent<Image>().sprite = null;
                objectIcon.GetChild(1).GetComponent<Text>().text = "";
            }

            ++currentElement;
        }

        swordsButton.GetComponent<Image>().color = new Color(255, 255, 255);
        shieldsButton.GetComponent<Image>().color = new Color(255, 255, 255);
        itemsButton.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);

        GameObject.Find("MenuActualInventory").GetComponent<Text>().text = "OBJETOS";
    }
}
