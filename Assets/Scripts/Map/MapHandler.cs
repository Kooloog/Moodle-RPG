using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapHandler : MonoBehaviour
{
    public static bool activated;

    public static GameObject houseCanvas;
    public static GameObject forjaCanvas;

    public static GameObject avatarCanvas;
    public static GameObject ataqueCanvas;
    public static GameObject defensaCanvas;

    private static GameObject noDinero;

    // Start is called before the first frame update
    void Start()
    {
        houseCanvas = GameObject.Find("HouseCanvas");
        forjaCanvas = GameObject.Find("ForjaCanvas");

        avatarCanvas = GameObject.Find("AvatarCanvas");
        ataqueCanvas = GameObject.Find("MenuAtaque");
        defensaCanvas = GameObject.Find("MenuDefensa");

        noDinero = GameObject.Find("NoDinero");

        MapTriggers.linkMapTriggers();

        //Preparando canvas de la casa del personaje
        avatarCanvas.GetComponent<Image>().sprite = 
            GameObject.Find("Avatar").GetComponent<SpriteRenderer>().sprite;
        avatarCanvas.GetComponent<Image>().color =
            GameObject.Find("Avatar").GetComponent<SpriteRenderer>().color;

        for(int i=0; i<avatarCanvas.transform.childCount; i++)
        {
            avatarCanvas.transform.GetChild(i).GetComponent<Image>().sprite =
                GameObject.Find("Avatar").transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;

            avatarCanvas.transform.GetChild(i).GetComponent<Image>().color =
                GameObject.Find("Avatar").transform.GetChild(i).GetComponent<SpriteRenderer>().color;
        }

        GameObject.Find("HousePlayerName").GetComponent<Text>().text = CharacterEdit.characterName;

        //Preparando canvas de la forja (armas)

        houseCanvas.SetActive(false);
        forjaCanvas.SetActive(false);
        noDinero.SetActive(false);

        activated = false;
    }

    public static void activateCanvas(int id)
    {
        activated = true;
        switch(id)
        {
            case 1: 
                houseCanvas.SetActive(true);
                GameObject.Find("HouseAttackAmount").GetComponent<Text>().text = Stats.attack.ToString();
                GameObject.Find("HouseDefenseAmount").GetComponent<Text>().text = Stats.defense.ToString();
                break;
            case 2:
                forjaCanvas.SetActive(true);
                forjaAttackMenu();
                break;
        }
    }

    public static void deactivateCanvas()
    {
        activated = false;
        switch(EventSystem.current.currentSelectedGameObject.name)
        {
            case "HouseX": houseCanvas.SetActive(false); break;
            case "ForjaX": forjaCanvas.SetActive(false); break;
        }
    }

    public static void openInventory()
    {
        Debug.Log("Opening Inventory");
    }

    public static void openCharacterEditor()
    {
        if(GameObject.Find("Avatar").gameObject != null) Destroy(GameObject.Find("Avatar").gameObject);
        SceneManager.LoadScene(0, LoadSceneMode.Single); 
    }

    public static void forjaAttackMenu()
    {
        ataqueCanvas.SetActive(true);
        defensaCanvas.SetActive(false);

        GameObject.Find("AtaqueButton").GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
        GameObject.Find("DefensaButton").GetComponent<Image>().color = new Color(255, 255, 255);
    }

    public static void forjaDefenseMenu()
    {
        ataqueCanvas.SetActive(false);
        defensaCanvas.SetActive(true);

        GameObject.Find("DefensaButton").GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
        GameObject.Find("AtaqueButton").GetComponent<Image>().color = new Color(255, 255, 255);
    }

    public static IEnumerator notEnoughMoney()
    {
        noDinero.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        noDinero.SetActive(false);
    }
}
