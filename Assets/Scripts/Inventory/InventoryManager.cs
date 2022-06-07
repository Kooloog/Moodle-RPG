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
            swords.Add(swordAux);

            instance.StartCoroutine(instance.AddInventoryItem("sword", swordNumber));
        }
    }

    IEnumerator AddInventoryItem(string type, int number)
    {
        string fullPostURL = saveInventoryURL +
            "?itemtype=" + type + "&itemid=" + number +
            "&uses=" + ObjectLists.swordsFinal[number].uses;

        UnityWebRequest statPost = UnityWebRequest.Post(fullPostURL, "");
        yield return statPost.SendWebRequest();
    }

    IEnumerator LoadInventoryItems()
    {
        UnityWebRequest inventoryGet = UnityWebRequest.Get(loadInventoryURL);
        yield return inventoryGet.SendWebRequest();

        string inventoryDataText = inventoryGet.downloadHandler.text;
        Debug.Log(inventoryDataText);

        if (!inventoryDataText.Contains("doesn't exist") && !inventoryDataText.Contains("null"))
        {
            List<Dictionary<string, string>> inventory = new List<Dictionary<string, string>>();

            string[] inventoryEntries = inventoryDataText.Split('|');
            foreach (string entry in inventoryEntries)
            {
                Dictionary<string, string> inventoryData = new Dictionary<string, string>();
                string[] inventoryFields = inventoryDataText.Split('\n');
                foreach (string field in inventoryFields)
                {
                    string[] currentField = field.Split(',');
                    inventoryData[currentField[0]] = currentField[1];
                }

                inventory.Add(inventoryData);
            }

            foreach(Dictionary<string, string> entry in inventory)
            {
                switch(entry["ITEMTYPE"])
                {
                    case "sword":
                        int swordNumber = int.Parse(entry["ITEMID"]);
                        Sword swordAux = new Sword(ObjectLists.swordsFinal[swordNumber]);
                        swordAux.usesLeft = int.Parse(entry["USES"]) - 2;
                        swords.Add(swordAux);
                        Debug.Log(swords[0].usesLeft);
                        break;
                    case "shield":
                        break;
                    case "item":
                        break;
                }
            }
        }
    }
}
