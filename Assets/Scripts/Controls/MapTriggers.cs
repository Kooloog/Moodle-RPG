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
    //1 = Activar ventana casa del jugador
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
            case "AdventureTrigger":
                currentPlace.SetActive(true);
                currentPlaceText.text = "Aventura";
                openWindow = 5;
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
                    if(this.gameObject != null) Destroy(this.gameObject);
                    SceneManager.LoadScene(0, LoadSceneMode.Single); 
                    break;
            }
        }
    }
}
