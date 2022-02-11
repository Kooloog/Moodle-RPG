using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterEdit : MonoBehaviour
{  
    private GameObject avatar;

    //Attribute IDs, one by one
    public static int skinTone;
    public static int eyeColor;
    public static string hairColor;
    public static int hairStyle;
    public static string shirtColor;
    public static int shirtStyle;
    public static string pantsColor;
    public static int pantsStyle;
    public static string shoeColor;
    public static int shoeStyle;

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
                Color colorHair = selectedItem.GetComponent<Image>().color;
                GameObject getHairstyles = GameObject.Find("Peinado");
                foreach(Transform button in getHairstyles.transform)
                {
                    button.GetChild(0).gameObject.GetComponent<Image>().color = colorHair;
                }

                hairColor = ColorUtility.ToHtmlStringRGB(colorHair);
                GameObject.Find("AvatarPelo").gameObject.GetComponent<SpriteRenderer>().color = colorHair;
                Debug.Log("ColorPelo: " + hairColor);
                break;

            //Cambio del peinado
            case "Peinado":
                break;

            case "CamisetaBasica":
                break;
        }

        if (selectedCategory.name != "ColorPelo" && selectedCategory.name != "ColorCamiseta" &&
            selectedCategory.name != "ColorPantalon" && selectedCategory.name != "ColorCalzado")
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
                    case "CamisetaBasica":
                        shirtStyle = current;
                        GameObject.Find("AvatarCamiseta").gameObject.GetComponent<SpriteRenderer>().sprite =
                            SpriteListsCharacter.camisetasBasicasFinal[current];
                        break;
                    case "Pantalones":
                        pantsStyle = current;
                        GameObject.Find("AvatarPantalon").gameObject.GetComponent<SpriteRenderer>().sprite =
                            SpriteListsCharacter.pantalonesFinal[current];
                        break;
                }
                break;
            }
            current++;
        }
        Debug.Log(attribute + ": " + current);
    }

    public static void customColour(string hex)
    {
        switch(GetCustomColour.attributeToChange)
        {
            case "AvatarPelo":
                hairColor = hex;
                Debug.Log("ColorPelo: " + hairColor);
                break;
            case "AvatarCamiseta":
                shirtColor = hex;
                Debug.Log("ColorCamiseta: " + shirtColor);
                break;
            case "AvatarPantalon":
                pantsColor = hex;
                Debug.Log("ColorPantalon: " + pantsColor);
                break;
            case "AvatarCalzado":
                shoeColor = hex;
                Debug.Log("ColorCalzado: " + shoeColor);
                break;
        }
    }

    public void enableHairColours()
    {
        EnableCustomizationMenus.hideEverything();
        EnableCustomizationMenus.enableSingleMenu("COLOR PELO");
    }

    public void enableShirtColours()
    {
        EnableCustomizationMenus.hideEverything();
        EnableCustomizationMenus.enableSingleMenu("COLOR CAMISETA");
    }

    public void enablePantsColours()
    {
        EnableCustomizationMenus.hideEverything();
        EnableCustomizationMenus.enableSingleMenu("COLOR PANTALON");
    }

    public void enableShoeColours()
    {
        EnableCustomizationMenus.hideEverything();
        EnableCustomizationMenus.enableSingleMenu("COLOR CALZADO");
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
