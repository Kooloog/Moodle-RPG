using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurnObjects : MonoBehaviour, IPointerClickHandler
{
    public static GameObject firstObject;
    public static GameObject secondObject;
    public static GameObject firstObjectOutline;
    public static GameObject secondObjectOutline;

    public static int firstObjectId;
    public static bool firstObjectPicked;
    public static Object firstObjectTurn;
    public static int secondObjectId;
    public static bool secondObjectPicked;
    public static Object secondObjectTurn;

    public static AudioSource selectSound;
    public static AudioSource eraseSound;

    public void selectObject(Sprite sprite, string text, int id)
    {
        if (!firstObjectPicked)
        {
            GameObject.Find("ImagenObjeto1").GetComponent<Image>().sprite = sprite;
            GameObject.Find("TextoObjeto1").GetComponent<Text>().text = text;
            selectSound.Play();
            firstObjectId = id;
            firstObjectPicked = true;

            //Guardando primer objeto
            switch (GameObject.Find("MenuActualInventory").GetComponent<Text>().text)
            {
                case "ESPADAS": firstObjectTurn = InventoryManager.swords[id]; break;
                case "ESCUDOS": firstObjectTurn = InventoryManager.shields[id]; break;
                case "OBJETOS": firstObjectTurn = InventoryManager.items[id]; break;
            }

            //A partir de este punto no se pueden elegir escudos.
            if (text.StartsWith("Escudo")) InventoryMenu.swordInventory();
            GameObject.Find("EscudosButton").GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject.Find("ImagenObjeto2").GetComponent<Image>().sprite = sprite;
            GameObject.Find("TextoObjeto2").GetComponent<Text>().text = text;
            selectSound.Play();
            secondObjectId = id;
            secondObjectPicked = true;

            //Guardando segundo objeto
            switch (GameObject.Find("MenuActualInventory").GetComponent<Text>().text)
            {
                case "ESPADAS": secondObjectTurn = InventoryManager.swords[id]; break;
                case "ESCUDOS": secondObjectTurn = InventoryManager.shields[id]; break;
                case "OBJETOS": secondObjectTurn = InventoryManager.items[id]; break;
            }
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
                    selectObject(pickedSword.sprite, "Espada " + pickedSword.swordName, selectedObjectNumber);
                    break;
                case "ESCUDOS":
                    Shield pickedShield = InventoryManager.shields[selectedObjectNumber];
                    selectObject(pickedShield.sprite, "Escudo " + pickedShield.shieldName, selectedObjectNumber);
                    break;
                case "OBJETOS":
                    Item pickedItem = InventoryManager.items[selectedObjectNumber];
                    selectObject(pickedItem.sprite, pickedItem.itemName, selectedObjectNumber);
                    break;
            }
        }
        else
        {
            Debug.Log("All objects picked.");
        }
    }

    private static void fillSelectedItemBox(int id, Object obj)
    {
        if(obj is Sword)
        {
            Sword sword = (Sword)obj;
            if (id == 1) GameObject.Find("ObjetoElegido1").GetComponent<Image>().sprite = sword.sprite;
            else GameObject.Find("ObjetoElegido2").GetComponent<Image>().sprite = sword.sprite;
        }
        else if(obj is Shield)
        {
            Shield shield = (Shield)obj;
            if (id == 1) GameObject.Find("ObjetoElegido1").GetComponent<Image>().sprite = shield.sprite;
            else GameObject.Find("ObjetoElegido2").GetComponent<Image>().sprite = shield.sprite;
        }
        else if(obj is Item)
        {
            Item item = (Item)obj;
            if (id == 1) GameObject.Find("ObjetoElegido1").GetComponent<Image>().sprite = item.sprite;
            else GameObject.Find("ObjetoElegido2").GetComponent<Image>().sprite = item.sprite;
        }
    }

    public static void callFillObjects()
    {
        fillSelectedItemBox(1, firstObjectTurn);

        if (secondObjectPicked) fillSelectedItemBox(2, secondObjectTurn);
        else GameObject.Find("ObjetoElegido2").GetComponent<Image>().sprite =
                GameObject.Find("InventoryHandler").GetComponent<Image>().sprite;
    }

    public void finishTurn()
    {
        if (!firstObjectPicked)
        {
            StartCoroutine(BattleManager.showWarning(1));
        }
        else if (!secondObjectPicked)
        {
            Debug.Log("showing");
            StartCoroutine(BattleManager.showWarning(2));
        }
        else
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().battleMove();
        }
    }

    public void clearAllItems()
    {
        GameObject.Find("ImagenObjeto1").GetComponent<Image>().sprite = null;
        GameObject.Find("TextoObjeto1").GetComponent<Text>().text = "";
        GameObject.Find("ImagenObjeto2").GetComponent<Image>().sprite = null;
        GameObject.Find("TextoObjeto2").GetComponent<Text>().text = "";
        firstObjectTurn = null;
        secondObjectTurn = null;
        firstObjectPicked = false;
        secondObjectPicked = false;
        eraseSound.Play();

        GameObject.Find("EscudosButton").GetComponent<Button>().interactable = true;
    }

    public static void changeOutline(int id, bool add)
    {
        if (add)
        {
            if (id == 1) firstObjectOutline.SetActive(true);
            else secondObjectOutline.SetActive(true);
        }
        else
        {
            if (id == 1) firstObjectOutline.SetActive(false);
            else secondObjectOutline.SetActive(false);
        }
    }

    public static void reduceUses(Object obj, int turn)
    {
        if (turn == 1)
        {
            if (obj is Sword)
            {
                InventoryManager.swords[firstObjectId].usesLeft--;
                if (InventoryManager.swords[firstObjectId].usesLeft <= 0)
                    InventoryManager.swords.Remove(InventoryManager.swords[firstObjectId]);
            }
            else if (obj is Shield)
            {
                InventoryManager.shields[firstObjectId].usesLeft--;
                if (InventoryManager.shields[firstObjectId].usesLeft <= 0)
                    InventoryManager.shields.Remove(InventoryManager.shields[firstObjectId]);
            }
            else if (obj is Item) InventoryManager.items.Remove(InventoryManager.items[firstObjectId]);
        }
        else if(turn == 2)
        {
            if (obj is Sword)
            {
                InventoryManager.swords[secondObjectId].usesLeft--;
                if (InventoryManager.swords[secondObjectId].usesLeft <= 0)
                    InventoryManager.swords.Remove(InventoryManager.swords[secondObjectId]);
            }
            else if (obj is Shield)
            {
                InventoryManager.shields[secondObjectId].usesLeft--;
                if (InventoryManager.shields[secondObjectId].usesLeft <= 0)
                    InventoryManager.shields.Remove(InventoryManager.shields[secondObjectId]);
            }
            else if (obj is Item) InventoryManager.items.Remove(InventoryManager.items[secondObjectId]);
        }
    }

    void Start()
    {
        selectSound = GameObject.Find("ItemSelect").GetComponent<AudioSource>();
        eraseSound = GameObject.Find("EraseItems").GetComponent<AudioSource>();
        firstObjectOutline = GameObject.Find("BrilloObjeto1");
        secondObjectOutline = GameObject.Find("BrilloObjeto2");
    }
}
