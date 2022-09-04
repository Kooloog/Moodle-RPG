using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnableCustomizationMenus : MonoBehaviour
{
    [NonSerialized] public static GameObject menuBotones;
    [NonSerialized] public static GameObject menuCara;
    [NonSerialized] public static GameObject menuPelo;
    [NonSerialized] public static GameObject menuColores;
    [NonSerialized] public static GameObject menuCamisetas;
    [NonSerialized] public static GameObject menuPantalon;
    [NonSerialized] public static GameObject menuAccesorios;
    [NonSerialized] public static GameObject menuFinal;

    [NonSerialized] public static GameObject blackWindowMenus;
    [NonSerialized] public static GameObject blackWindowColors;

    public AudioSource menuChange;

    public static string currentMenu;

    public static void hideEverything()
    {
        menuBotones.SetActive(false);
        menuCara.SetActive(false);
        menuPelo.SetActive(false);
        menuColores.SetActive(false);
        menuCamisetas.SetActive(false);
        menuPantalon.SetActive(false);
        menuAccesorios.SetActive(false);
        menuFinal.SetActive(false);
    }

    public static void enableSingleMenu(string op)
    {
        switch(op)
        {
            case "BOTONES":
                menuBotones.SetActive(true);
                break;
            case "CARA":
                menuCara.SetActive(true);
                break;
            case "PELO":
                menuPelo.SetActive(true);
                break;
            case "COLOR PELO":
                GetCustomColour.attributeToChange = "AvatarPelo";
                menuColores.SetActive(true);
                break;
            case "COLOR CAMISETA":
                GetCustomColour.attributeToChange = "AvatarCamiseta";
                menuColores.SetActive(true);
                break;
            case "COLOR PANTALON":
                GetCustomColour.attributeToChange = "AvatarPantalon";
                menuColores.SetActive(true);
                break;
            case "COLOR CALZADO":
                GetCustomColour.attributeToChange = "AvatarCalzado";
                menuColores.SetActive(true);
                break;
            case "CAMISETAS":
                GetCustomColour.attributeToChange = "AvatarCamiseta";
                menuCamisetas.SetActive(true);
                break;
            case "PANTALONES":
                GetCustomColour.attributeToChange = "AvatarPantalon";
                menuPantalon.SetActive(true);
                break;
            case "ACCESORIOS":
                GetCustomColour.attributeToChange = "AvatarAccesorio";
                menuAccesorios.SetActive(true);
                break;
            case "COLOR GAFAS":
                GetCustomColour.attributeToChange = "AvatarGafas";
                menuColores.SetActive(true);
                break;
            case "COLOR COLLAR":
                GetCustomColour.attributeToChange = "AvatarCollar";
                menuColores.SetActive(true);
                break;
            case "FINAL":
                menuFinal.SetActive(true);
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

        menuChange.Play();
    }

    public static void changeToMenuView()
    {
        blackWindowMenus.SetActive(true);
        blackWindowColors.SetActive(false);
    }

    public static void changeToColorView()
    {
        blackWindowColors.SetActive(true);
        blackWindowMenus.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        menuBotones = GameObject.Find("Botones");
        menuCara = GameObject.Find("MenuCara");
        menuPelo = GameObject.Find("MenuPelo");
        menuColores = GameObject.Find("MenuColores");
        menuCamisetas = GameObject.Find("MenuCamiseta");
        menuPantalon = GameObject.Find("MenuPantalon");
        menuAccesorios = GameObject.Find("MenuAccesorios");
        menuFinal = GameObject.Find("MenuFinal");

        blackWindowMenus = GameObject.Find("BlackWindow");
        blackWindowColors = GameObject.Find("BlackWindowColors");

        blackWindowColors.SetActive(false);
    }
}
