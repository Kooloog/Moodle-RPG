using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurnObjects : MonoBehaviour, IPointerClickHandler
{
    public static GameObject firstObject;
    public static GameObject secondObject;

    public static bool firstObjectPicked;
    public static bool secondObjectPicked;

    public static AudioSource selectSound;
    public static AudioSource eraseSound;

    public void selectObject(Sprite sprite, string text)
    {
        if (!firstObjectPicked)
        {
            GameObject.Find("ImagenObjeto1").GetComponent<Image>().sprite = sprite;
            GameObject.Find("TextoObjeto1").GetComponent<Text>().text = text;
            selectSound.Play();
            firstObjectPicked = true;

            //A partir de este punto no se pueden elegir escudos.
            if(text.StartsWith("Escudo")) InventoryMenu.swordInventory();
            GameObject.Find("EscudosButton").GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject.Find("ImagenObjeto2").GetComponent<Image>().sprite = sprite;
            GameObject.Find("TextoObjeto2").GetComponent<Text>().text = text;
            selectSound.Play();
            secondObjectPicked = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        firstObject = GameObject.Find("PrimerObjeto");
        secondObject = GameObject.Find("SegundoObjeto");

        GameObject selectedObject = eventData.pointerCurrentRaycast.gameObject;
        int selectedObjectNumber = int.Parse(selectedObject.name.ToCharArray()[7].ToString());

        if (!firstObjectPicked || !secondObjectPicked)
        {
            switch (GameObject.Find("MenuActualInventory").GetComponent<Text>().text)
            {
                case "ESPADAS":
                    Sword pickedSword = InventoryManager.swords[selectedObjectNumber];
                    selectObject(pickedSword.sprite, "Espada " + pickedSword.swordName);
                    break;
                case "ESCUDOS":
                    Shield pickedShield = InventoryManager.shields[selectedObjectNumber];
                    selectObject(pickedShield.sprite, "Escudo " + pickedShield.shieldName);
                    break;
                case "OBJETOS":
                    Item pickedItem = InventoryManager.items[selectedObjectNumber];
                    selectObject(pickedItem.sprite, pickedItem.itemName);
                    break;
            }
        }
        else
        {
            Debug.Log("All objects picked.");
        }
    }

    public void clearAllItems()
    {
        GameObject.Find("ImagenObjeto1").GetComponent<Image>().sprite = null;
        GameObject.Find("TextoObjeto1").GetComponent<Text>().text = "";
        GameObject.Find("ImagenObjeto2").GetComponent<Image>().sprite = null;
        GameObject.Find("TextoObjeto2").GetComponent<Text>().text = "";
        firstObjectPicked = false;
        secondObjectPicked = false;
        eraseSound.Play();

        GameObject.Find("EscudosButton").GetComponent<Button>().interactable = true;
    }

    void Start()
    {
        selectSound = GameObject.Find("ItemSelect").GetComponent<AudioSource>();
        eraseSound = GameObject.Find("EraseItems").GetComponent<AudioSource>();
    }
}
