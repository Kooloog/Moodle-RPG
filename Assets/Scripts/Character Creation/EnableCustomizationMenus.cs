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
    public GameObject menuColores;
    public GameObject menuCamisetas;
    public GameObject menuPantalon;
    public GameObject menuAccesorios;
    public GameObject menuFinal;

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
        Debug.Log(op);
        switch(op)
        {
            case "BOTONES":
                menus[0].SetActive(true);
                break;
            case "CARA":
                menus[1].SetActive(true);
                break;
            case "PELO":
                menus[2].SetActive(true);
                break;
            case "COLOR PELO":
                GetCustomColour.attributeToChange = "AvatarPelo";
                menus[3].SetActive(true);
                break;
            case "COLOR CAMISETA":
                GetCustomColour.attributeToChange = "AvatarCamiseta";
                menus[3].SetActive(true);
                break;
            case "COLOR PANTALON":
                GetCustomColour.attributeToChange = "AvatarPantalon";
                menus[3].SetActive(true);
                break;
            case "COLOR CALZADO":
                GetCustomColour.attributeToChange = "AvatarCalzado";
                menus[3].SetActive(true);
                break;
            case "CAMISETAS":
                GetCustomColour.attributeToChange = "AvatarCamiseta";
                menus[4].SetActive(true);
                break;
            case "PANTALONES":
                GetCustomColour.attributeToChange = "AvatarPantalon";
                menus[5].SetActive(true);
                break;
            case "ACCESORIOS":
                GetCustomColour.attributeToChange = "AvatarAccesorio";
                menus[6].SetActive(true);
                break;
            case "COLOR GAFAS":
                GetCustomColour.attributeToChange = "AvatarGafas";
                menus[3].SetActive(true);
                break;
            case "COLOR COLLAR":
                GetCustomColour.attributeToChange = "AvatarCollar";
                menus[3].SetActive(true);
                break;
            case "FINAL":
                menus[7].SetActive(true);
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
            case "PANTALONES":
                menuPantalon.SetActive(true);
                currentMenu = menuPantalon.gameObject.name;
                break;
            case "ACCESORIOS":
                menuAccesorios.SetActive(true);
                currentMenu = menuAccesorios.gameObject.name;
                break;
            case "FINALIZAR":
                menuFinal.SetActive(true);
                currentMenu = menuFinal.gameObject.name;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        menus.Add(menuBotones);        //ID = 0
        menus.Add(menuCara);           //ID = 1
        menus.Add(menuPelo);           //ID = 2
        menus.Add(menuColores);        //ID = 3
        menus.Add(menuCamisetas);      //ID = 4
        menus.Add(menuPantalon);       //ID = 5
        menus.Add(menuAccesorios);     //ID = 6
        menus.Add(menuFinal);          //ID = 7
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
