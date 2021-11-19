using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterEdit : MonoBehaviour
{  
    private GameObject avatar;

    //Attribute IDs, one by one
    private static int skinTone;
    private static int eyeColor;
    private static string hairColor;
    private static int hairStyle;

    //Cambia, uno por uno, los diferentes rasgos del personaje.
    public void changeAttribute()
    {
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
        GameObject selectedItem = selectedButton.transform.GetChild(0).gameObject;
        GameObject selectedCategory = selectedButton.transform.parent.gameObject;

        switch(selectedCategory.name)
        {
            //Cambio del tono de piel del personaje
            case "TonoPiel":
                avatar.GetComponent<SpriteRenderer>().color = selectedItem.GetComponent<Image>().color;
                break;

            //Cambio del color de ojos del personaje
            case "ColorOjos":
                GameObject ojosAvatar = avatar.transform.Find("AvatarOjos").gameObject;
                ojosAvatar.GetComponent<SpriteRenderer>().color = selectedItem.GetComponent<Image>().color;
                break;

            //Cambio del color del pelo
            case "ColorPelo":
                Color color = selectedItem.GetComponent<Image>().color;
                GameObject getHairstyles = GameObject.Find("Peinado");
                foreach(Transform button in getHairstyles.transform)
                {
                    button.GetChild(0).gameObject.GetComponent<Image>().color = color;
                }

                hairColor = ColorUtility.ToHtmlStringRGB(color);
                GameObject.Find("AvatarPelo").gameObject.GetComponent<SpriteRenderer>().color = color;
                Debug.Log("ColorPelo: " + hairColor);
                break;

            //Cambio del peinado
            case "Peinado":
                break;
        }

        if (selectedCategory.name != "ColorPelo")
        {
            findAttributeID(selectedCategory, selectedButton, selectedCategory.name);
        }
    }

    private void findAttributeID(GameObject selCategory, GameObject selButton, string attribute)
    {
        int current = 0;
        foreach (Transform button in selCategory.transform)
        {
            if (button == selButton.transform)
            {
                switch(attribute)
                {
                    case "TonoPiel":
                        skinTone = current; break;
                    case "ColorOjos":
                        eyeColor = current; break;
                    case "Peinado":
                        hairStyle = current;
                        GameObject.Find("AvatarPelo").gameObject.GetComponent<SpriteRenderer>().sprite =
                            SpriteListsCharacter.pelosFinal[current];
                        break;
                }
                break;
            }
            current++;
        }
        Debug.Log(attribute + ": " + current);
    }

    public static void customHairColour(string hex)
    {
        hairColor = hex;
        Debug.Log("ColorPelo: " + hairColor);
    }

    public void enableHairColours()
    {
        EnableCustomizationMenus.hideEverything();
        EnableCustomizationMenus.enableSingleMenu("COLOR PELO");
    }

    // Start is called before the first frame update
    void Start()
    {
        avatar = GameObject.Find("Avatar");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
