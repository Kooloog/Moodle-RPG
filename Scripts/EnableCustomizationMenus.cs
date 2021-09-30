using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnableCustomizationMenus : MonoBehaviour
{
    public GameObject menuCara;
    public GameObject menuPelo;

    private List<GameObject> menus = new List<GameObject>();

    public void changeCurrentMenu()
    {
        foreach(GameObject menu in menus)
        {
            if (menu == null) break;
            menu.SetActive(false);
        }

        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
        string menuToActivate = selectedButton.transform.GetChild(0).GetComponent<Text>().text;

        switch(menuToActivate)
        {
            case "CARA":
                menuCara.SetActive(true);
                break;
            case "PELO":
                menuPelo.SetActive(true);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // menuCara = GameObject.Find("MenuCara");
        // menuPelo = GameObject.Find("MenuPelo");
        
        menus.Add(menuCara);
        menus.Add(menuPelo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
