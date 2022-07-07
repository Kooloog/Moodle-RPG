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

    public static List<GameObject> inventoryImages;
    public static List<GameObject> inventoryInfoScreens;

    // Start is called before the first frame update
    void Start()
    {
        objectIcons = GameObject.Find("ObjetosInventory");

        swordsButton = GameObject.Find("EspadasButton");
        shieldsButton = GameObject.Find("EscudosButton");
        itemsButton = GameObject.Find("ObjetosButton");

        menuSound = GameObject.Find("MenuSelect").GetComponent<AudioSource>();

        inventoryImages = new List<GameObject>();
        inventoryInfoScreens = new List<GameObject>();

        foreach (Transform objectIcon in objectIcons.transform)
        {
            inventoryImages.Add(objectIcon.GetChild(1).gameObject);
            inventoryInfoScreens.Add(objectIcon.GetChild(1).GetChild(0).gameObject);
        }

        foreach (GameObject g in inventoryInfoScreens) g.SetActive(false);

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
                inventoryImages[currentElement].SetActive(true);
                inventoryInfoScreens[currentElement].transform.GetChild(1).GetComponent<Text>().text =
                    InventoryManager.swords[currentElement].swordName;

                objectIcon.GetComponent<Button>().interactable = true;
                objectIcon.GetChild(1).GetComponent<Image>().sprite = 
                    InventoryManager.swords[currentElement].sprite;
                objectIcon.GetChild(0).GetComponent<Text>().text = 
                    InventoryManager.swords[currentElement].usesLeft.ToString();
            }
            else
            {
                inventoryImages[currentElement].SetActive(false);

                objectIcon.GetComponent<Button>().interactable = false;
                objectIcon.GetChild(1).GetComponent<Image>().sprite = null;
                objectIcon.GetChild(0).GetComponent<Text>().text = "";
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
                inventoryImages[currentElement].SetActive(true);
                inventoryInfoScreens[currentElement].transform.GetChild(1).GetComponent<Text>().text =
                    InventoryManager.shields[currentElement].shieldName;

                objectIcon.GetComponent<Button>().interactable = true;
                objectIcon.GetChild(1).GetComponent<Image>().sprite =
                    InventoryManager.shields[currentElement].sprite;
                objectIcon.GetChild(0).GetComponent<Text>().text =
                    InventoryManager.shields[currentElement].usesLeft.ToString();
            }
            else
            {
                inventoryImages[currentElement].SetActive(false);

                objectIcon.GetComponent<Button>().interactable = false;
                objectIcon.GetChild(1).GetComponent<Image>().sprite = null;
                objectIcon.GetChild(0).GetComponent<Text>().text = "";
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
                inventoryImages[currentElement].SetActive(true);
                inventoryInfoScreens[currentElement].transform.GetChild(1).GetComponent<Text>().text =
                    InventoryManager.items[currentElement].itemName;

                objectIcon.GetComponent<Button>().interactable = true;
                objectIcon.GetChild(1).GetComponent<Image>().sprite =
                    InventoryManager.items[currentElement].sprite;
                objectIcon.GetChild(0).GetComponent<Text>().text = "";
            }
            else
            {
                inventoryImages[currentElement].SetActive(false);

                objectIcon.GetComponent<Button>().interactable = false;
                objectIcon.GetChild(1).GetComponent<Image>().sprite = null;
                objectIcon.GetChild(0).GetComponent<Text>().text = "";
            }

            ++currentElement;
        }

        swordsButton.GetComponent<Image>().color = new Color(255, 255, 255);
        shieldsButton.GetComponent<Image>().color = new Color(255, 255, 255);
        itemsButton.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);

        GameObject.Find("MenuActualInventory").GetComponent<Text>().text = "OBJETOS";
    }
}
