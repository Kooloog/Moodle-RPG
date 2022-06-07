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

    // Start is called before the first frame update
    void Start()
    {
        houseCanvas = GameObject.Find("HouseCanvas");
        forjaCanvas = GameObject.Find("ForjaCanvas");

        avatarCanvas = GameObject.Find("AvatarCanvas");
        ataqueCanvas = GameObject.Find("AtaqueCanvas");
        defensaCanvas = GameObject.Find("DefensaCanvas");

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
}
