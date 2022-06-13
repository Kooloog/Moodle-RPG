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

    static InventoryManager instance;

    // Start is called before the first frame update
    void Start()
    {
        swords = new List<Sword>();
        shields = new List<Shield>();
        items = new List<Item>();

        instance = this;

        StartCoroutine(LoadInventoryItems());
    }

    public static void purchaseSword()
    {
        if(swords.Count < 10)
        {
            GameObject pickedSword = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
            int swordNumber = int.Parse(pickedSword.name.Split('_')[1]);
            Sword swordAux = new Sword(ObjectLists.swordsFinal[swordNumber]);

            if (swordAux.cost > Stats.coins)
            {
                instance.StartCoroutine(MapHandler.notEnoughMoney());
            }
            else
            {
                swords.Add(swordAux);

                GameObject load = new GameObject("InventoryPurchaseHandler");
                StatManager makePurchase = load.AddComponent<StatManager>();
                makePurchase.decreaseCoins(swordAux.cost);
                Destroy(load);

                instance.StartCoroutine(instance.AddInventoryItem("sword", swordNumber));
            }
        }
    }

    public static void purchaseShield()
    {
        if(shields.Count < 10)
        {
            GameObject pickedShield = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
            int shieldNumber = int.Parse(pickedShield.name.Split('_')[1]);
            Shield shieldAux = new Shield(ObjectLists.shieldsFinal[shieldNumber]);

            if(shieldAux.cost > Stats.coins)
            {
                instance.StartCoroutine(MapHandler.notEnoughMoney());
            }
            else
            {
                shields.Add(shieldAux);

                GameObject load = new GameObject("InventoryPurchaseHandler");
                StatManager makePurchase = load.AddComponent<StatManager>();
                makePurchase.decreaseCoins(shieldAux.cost);
                Destroy(load);

                instance.StartCoroutine(instance.AddInventoryItem("shield", shieldNumber));
            }
        }
    }

    public static void purchaseItem()
    {
        if(items.Count < 10)
        {
            GameObject pickedItem = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
            int itemNumber = int.Parse(pickedItem.name.Split('_')[1]);
            Item itemAux = new Item(ObjectLists.itemsFinal[itemNumber]);

            if(itemAux.cost > Stats.coins)
            {
                instance.StartCoroutine(MapHandler.notEnoughMoney());
            }
            else
            {
                items.Add(itemAux);

                GameObject load = new GameObject("InventoryPurchaseHandler");
                StatManager makePurchase = load.AddComponent<StatManager>();
                makePurchase.decreaseCoins(itemAux.cost);
                Destroy(load);

                instance.StartCoroutine(instance.AddInventoryItem("item", itemNumber));
            }
        }
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
        Debug.Log("RECIBIDO: " + inventoryDataText);

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
                        Sword swordAux = new Sword(ObjectLists.swordsFinal[swordNumber]);
                        swords.Add(swordAux);
                        break;
                    case "shield":
                        int shieldNumber = int.Parse(entry["ITEMID"]);
                        Shield shieldAux = new Shield(ObjectLists.shieldsFinal[shieldNumber]);
                        shields.Add(shieldAux);
                        break;
                    case "item":
                        int itemNumber = int.Parse(entry["ITEMID"]);
                        Item itemAux = new Item(ObjectLists.itemsFinal[itemNumber]);
                        items.Add(itemAux);
                        break;
                }
            }
        }

        InventoryMenu.swordInventory();
    }
}
