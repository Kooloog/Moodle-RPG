using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapTriggers : MonoBehaviour
{
    public static GameObject pulsaEspacio;
    public static GameObject currentPlace;
    public static Text currentPlaceText;

    //0 = No activar nada
    //1 = Activar ventana casa
    //2 = Activar ventana forja
    //3 = Activar ventana tienda
    //4 = Activar ventana tablón
    //5 = Activar ventana cofre
    //6 = Activar ventana aventura
    private int openWindow = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.name)
        {
            case "HouseTrigger":
                currentPlace.SetActive(true);
                currentPlaceText.text = "Casa de " + CharacterEdit.characterName;
                openWindow = 1;
                break;
            case "ForjaTrigger":
                currentPlace.SetActive(true);
                currentPlaceText.text = "Forja de armas";
                openWindow = 2;
                break;
            case "TiendaTrigger":
                currentPlace.SetActive(true);
                currentPlaceText.text = "Tienda";
                openWindow = 3;
                break;
            case "TablonTrigger":
                currentPlace.SetActive(true);
                currentPlaceText.text = "Clasificaciones";
                openWindow = 4;
                break;
            case "CofreTrigger":
                currentPlace.SetActive(true);
                currentPlaceText.text = "Cofre magico";
                openWindow = 5;
                break;
            case "AdventureTrigger":
                currentPlace.SetActive(true);
                currentPlaceText.text = "Aventura";
                openWindow = 6;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        currentPlace.SetActive(false);
        openWindow = 0;
    }

    public static void linkMapTriggers()
    {
        if (GameObject.Find("CurrentPlace"))
        {
            currentPlace = GameObject.Find("CurrentPlace");
            currentPlaceText = GameObject.Find("CurrentPlaceText").GetComponent<Text>();

            currentPlace.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch(openWindow)
            {
                case 1:
                    MapHandler.activateCanvas(1);
                    break;
                case 2:
                    MapHandler.activateCanvas(2);
                    break;
                case 3:
                    Debug.Log("Abriendo tienda");
                    break;
                case 4:
                    Debug.Log("Clasificaciones");
                    break;
                case 5:
                    Debug.Log("Abriendo cofre mágico");
                    break;
                case 6:
                    Debug.Log("Abriendo aventura");
                    break;
            }
        }
    }
}
