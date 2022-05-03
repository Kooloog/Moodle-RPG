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
    public static string facehairColor;
    public static int facehairStyle;
    public static float facehairAlpha;
    public static string glassesColor;
    public static int glassesStyle;
    public static string collarColor;
    public static int collarStyle;
    public static string characterName;
    public static string characterGender;

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

            //Cambio del color del vello facial
            case "ColorVello":
                Color colorFacialHair = selectedItem.GetComponent<Image>().color;
                GameObject getFacialHairs = GameObject.Find("Vello");
                float alphaValue = GameObject.Find("SliderVello").GetComponent<Slider>().value;

                foreach (Transform button in getFacialHairs.transform)
                {
                    button.GetChild(0).gameObject.GetComponent<Image>().color = colorFacialHair;
                }

                facehairColor = ColorUtility.ToHtmlStringRGB(colorFacialHair);
                facehairAlpha = alphaValue;

                colorFacialHair.a = alphaValue;
                GameObject.Find("AvatarVello").gameObject.GetComponent<SpriteRenderer>().color = colorFacialHair;
                Debug.Log("ColorVello: " + facehairColor);
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
                button.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
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
                    case "Calzado":
                        shoeStyle = current;
                        GameObject.Find("AvatarCalzado").gameObject.GetComponent<SpriteRenderer>().sprite =
                            SpriteListsCharacter.calzadoFinal[current];
                        break;
                    case "Vello":
                        facehairStyle = current;
                        GameObject.Find("AvatarVello").gameObject.GetComponent<SpriteRenderer>().sprite =
                            SpriteListsCharacter.velloFinal[current];
                        break;
                    case "Gafas":
                        glassesStyle = current;
                        GameObject.Find("AvatarGafas").gameObject.GetComponent<SpriteRenderer>().sprite =
                            SpriteListsCharacter.gafasFinal[current];
                        break;
                    case "Collares":
                        collarStyle = current;
                        GameObject.Find("AvatarCollar").gameObject.GetComponent<SpriteRenderer>().sprite =
                            SpriteListsCharacter.collaresFinal[current];
                        break;
                }
            }
            else button.GetComponent<Image>().color = new Color(255, 255, 255);
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
            case "AvatarGafas":
                glassesColor = hex;
                Debug.Log("ColorGafas: " + glassesColor);
                break;
            case "AvatarCollar":
                collarColor = hex;
                Debug.Log("ColorCollar: " + collarColor);
                break;
        }
    }

    public void changeFacialHairTransparency()
    {
        SpriteRenderer vello = GameObject.Find("AvatarVello").gameObject.GetComponent<SpriteRenderer>();
        Slider slider = GameObject.Find("SliderVello").GetComponent<Slider>();
        vello.color = new Color(vello.color.r, vello.color.g, vello.color.b, slider.value);
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

    public void enableGlassesColours()
    {
        EnableCustomizationMenus.hideEverything();
        EnableCustomizationMenus.enableSingleMenu("COLOR GAFAS");
    }

    public void enableCollarColours()
    {
        EnableCustomizationMenus.hideEverything();
        EnableCustomizationMenus.enableSingleMenu("COLOR COLLAR");
    }

    public void changeCharacterName()
    {
        characterName = GameObject.Find("NameText").GetComponent<Text>().text.ToUpper();
        Debug.Log(characterName);
        
        if(characterName.Length > 0)
        {
            GameObject.Find("Acabar").GetComponent<Button>().interactable = true;
        }
        else
        {
            GameObject.Find("Acabar").GetComponent<Button>().interactable = false;
        }
    }

    public void changeCharacterGender()
    {
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
        characterGender = selectedButton.name;
        selectedButton.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
        Debug.Log(characterGender);

        if (characterGender == "Male")
        {
            GameObject.Find("Female").GetComponent<Image>().color = new Color(255, 255, 255);
        }
        else
        {
            GameObject.Find("Male").GetComponent<Image>().color = new Color(255, 255, 255);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        avatar = GameObject.Find("Avatar");
        DontDestroyOnLoad(avatar);
    }
}
