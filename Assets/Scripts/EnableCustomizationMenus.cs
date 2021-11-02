using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnableCustomizationMenus : MonoBehaviour
{
    public GameObject menuBotones;
    public GameObject menuCara;
    public GameObject menuPelo;
    public GameObject menuColorPelo;
    public GameObject menuCamisetas;

    private static List<GameObject> menus = new List<GameObject>();
    public static string currentMenu;

    public static void hideEverything()
    {
        foreach (GameObject menu in menus)
        {
            if (menu == null) break;
            menu.SetActive(false);
        }
    }

    public static void enableSingleMenu(string op)
    {
        switch(op)
        {
            case "BOTONES":
                menus[0].SetActive(true);
                break;
            case "PELO":
                menus[2].SetActive(true);
                break;
            case "COLOR PELO":
                menus[3].SetActive(true);
                break;
        }
    }

    public void changeCurrentMenu()
    {
        hideEverything();
        menuBotones.SetActive(true);

        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
        string menuToActivate = selectedButton.transform.GetChild(0).GetComponent<Text>().text;

        switch(menuToActivate)
        {
            case "CARA":
                menuCara.SetActive(true);
                currentMenu = menuCara.gameObject.name;
                break;
            case "PELO":
                menuPelo.SetActive(true);
                currentMenu = menuPelo.gameObject.name;
                break;
            case "CAMISETAS":
                menuCamisetas.SetActive(true);
                currentMenu = menuCamisetas.gameObject.name;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        menus.Add(menuBotones);        //ID = 0
        menus.Add(menuCara);           //ID = 1
        menus.Add(menuPelo);           //ID = 2
        menus.Add(menuColorPelo);      //ID = 3
        menus.Add(menuCamisetas);      //ID = 4
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
