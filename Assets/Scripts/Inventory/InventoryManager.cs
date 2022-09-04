using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class InventoryManager : MonoBehaviour
{
    public static List<Sword> swords;
    public static List<Shield> shields;
    public static List<Item> items;

    private string saveInventoryURL = "http://localhost/moodle/unity/uploadInventory.php";
    private string loadInventoryURL = "http://localhost/moodle/unity/loadInventory.php";
    private static string highestIdURL = "http://localhost/moodle/unity/getHighestItemId.php";

    private static AudioSource purchaseSound;

    private StatManager statManager;

    // Start is called before the first frame update
    void Start()
    {
        swords = new List<Sword>();
        shields = new List<Shield>();
        items = new List<Item>();

        purchaseSound = GameObject.Find("ItemPurchaseSound").GetComponent<AudioSource>();
        statManager = GameObject.Find("StatsManager").GetComponent<StatManager>();

        StartCoroutine(LoadInventoryItems());
    }

    public void purchaseSword()
    {
        StartCoroutine(purchaseSwordCoroutine());
    }

    public void purchaseShield()
    {
        StartCoroutine(purchaseShieldCoroutine());
    }

    public void purchaseItem()
    {
        StartCoroutine(purchaseItemCoroutine());
    }

    public IEnumerator purchaseSwordCoroutine()
    {
        if (swords.Count < 10)
        {
            GameObject pickedSword = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
            int swordNumber = int.Parse(pickedSword.name.Split('_')[1]);

            UnityWebRequest highestIdGet = UnityWebRequest.Get(highestIdURL);
            yield return highestIdGet.SendWebRequest();

            int highestId = 0;
            if (!highestIdGet.downloadHandler.text.Contains("doesn't exist"))
                highestId = int.Parse(highestIdGet.downloadHandler.text);

            Sword swordAux = new Sword(ObjectLists.swordsFinal[swordNumber], highestId + 1);

            if (swordAux.cost > Stats.coins)
            {
                StartCoroutine(MapHandler.notEnoughMoney());
            }
            else
            {
                swords.Add(swordAux);
                statManager.decreaseCoins(swordAux.cost);
                StartCoroutine(AddInventoryItem("sword", swordNumber));
                purchaseSound.Play();
            }
        }
        else
        {
            StartCoroutine(MapHandler.notEnoughSpace());
        }

        yield return null;
    }

    public IEnumerator purchaseShieldCoroutine()
    {
        if (shields.Count < 10)
        {
            GameObject pickedShield = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
            int shieldNumber = int.Parse(pickedShield.name.Split('_')[1]);

            UnityWebRequest highestIdGet = UnityWebRequest.Get(highestIdURL);
            yield return highestIdGet.SendWebRequest();
            int highestId = int.Parse(highestIdGet.downloadHandler.text);

            Shield shieldAux = new Shield(ObjectLists.shieldsFinal[shieldNumber], highestId + 1);

            if (shieldAux.cost > Stats.coins)
            {
                StartCoroutine(MapHandler.notEnoughMoney());
            }
            else
            {
                shields.Add(shieldAux);
                statManager.decreaseCoins(shieldAux.cost);
                StartCoroutine(AddInventoryItem("shield", shieldNumber));
                purchaseSound.Play();
            }
        }
        else
        {
            StartCoroutine(MapHandler.notEnoughSpace());
        }

        yield return null;
    }

    public IEnumerator purchaseItemCoroutine()
    {
        if (items.Count < 10)
        {
            GameObject pickedItem = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
            int itemNumber = int.Parse(pickedItem.name.Split('_')[1]);

            UnityWebRequest highestIdGet = UnityWebRequest.Get(highestIdURL);
            yield return highestIdGet.SendWebRequest();
            int highestId = int.Parse(highestIdGet.downloadHandler.text);

            Item itemAux = new Item(ObjectLists.itemsFinal[itemNumber], highestId + 1);

            if (itemAux.cost > Stats.coins)
            {
                StartCoroutine(MapHandler.notEnoughMoney());
            }
            else
            {
                items.Add(itemAux);
                statManager.decreaseCoins(itemAux.cost);
                StartCoroutine(AddInventoryItem("item", itemNumber));
                purchaseSound.Play();
            }
        }
        else
        {
            StartCoroutine(MapHandler.notEnoughSpace());
        }

        yield return null;
    }

    IEnumerator AddInventoryItem(string type, int number)
    {
        string fullPostURL = saveInventoryURL +
            "?itemtype=" + type + "&itemid=" + number;

        switch(type)
        {
            case "sword": fullPostURL += "&uses=" + ObjectLists.swordsFinal[number].uses; break;
            case "shield": fullPostURL += "&uses=" + ObjectLists.shieldsFinal[number].uses; break;
            case "item": fullPostURL += "&uses=1"; break;
        }

        UnityWebRequest statPost = UnityWebRequest.Post(fullPostURL, "");
        yield return statPost.SendWebRequest();
    }

    IEnumerator LoadInventoryItems()
    {
        UnityWebRequest inventoryGet = UnityWebRequest.Get(loadInventoryURL);
        yield return inventoryGet.SendWebRequest();

        string inventoryDataText = inventoryGet.downloadHandler.text;

        if (!inventoryDataText.Contains("doesn't exist") && !inventoryDataText.Contains("null"))
        {
            List<Dictionary<string, string>> inventory = new List<Dictionary<string, string>>();

            string[] inventoryEntries = inventoryDataText.Split('|');
            foreach (string entry in inventoryEntries)
            {
                Dictionary<string, string> inventoryData = new Dictionary<string, string>();
                string[] inventoryFields = entry.Split('\n');
                foreach (string field in inventoryFields)
                {
                    string[] currentField = field.Split(',');
                    inventoryData[currentField[0]] = currentField[1];
                }

                inventory.Add(inventoryData);
            }

            foreach(Dictionary<string, string> entry in inventory)
            {
                switch (entry["ITEMTYPE"])
                {
                    case "sword":
                        int swordNumber = int.Parse(entry["ITEMID"]);
                        Sword swordAux = new Sword(ObjectLists.swordsFinal[swordNumber], int.Parse(entry["ID"]));
                        swordAux.usesLeft = int.Parse(entry["USES"]);
                        swords.Add(swordAux);
                        break;
                    case "shield":
                        int shieldNumber = int.Parse(entry["ITEMID"]);
                        Shield shieldAux = new Shield(ObjectLists.shieldsFinal[shieldNumber], int.Parse(entry["ID"]));
                        shieldAux.usesLeft = int.Parse(entry["USES"]);
                        shields.Add(shieldAux);
                        break;
                    case "item":
                        int itemNumber = int.Parse(entry["ITEMID"]);
                        Item itemAux = new Item(ObjectLists.itemsFinal[itemNumber], int.Parse(entry["ID"]));
                        items.Add(itemAux);
                        break;
                }
            }
        }

        InventoryMenu.playSound = false;
        InventoryMenu.swordInventory();
        InventoryMenu.playSound = true;
    }
}
