using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SaveLoadCharacter : MonoBehaviour
{
    private string saveCharacterURL = "http://localhost/moodle/unity/uploadCharacter.php";
    private string loadCharacterURL = "http://localhost/moodle/unity/loadCharacterData.php";

    public void saveCharacter()
    {
        Debug.Log("Tono de piel: " + CharacterEdit.skinTone);
        Debug.Log("Color de ojos: " + CharacterEdit.eyeColor);
        Debug.Log("Color del pelo: " + CharacterEdit.hairColor);
        Debug.Log("Estilo del pelo: " + CharacterEdit.hairStyle);
        Debug.Log("Color de camiseta: " + CharacterEdit.shirtColor);
        Debug.Log("Estilo de camiseta: " + CharacterEdit.shirtStyle);
        Debug.Log("Color del pantalón: " + CharacterEdit.pantsColor);
        Debug.Log("Estilo del pantalón: " + CharacterEdit.pantsStyle);
        Debug.Log("Color del calzado: " + CharacterEdit.shoeColor);
        Debug.Log("Estilo del calzado: " + CharacterEdit.shoeStyle);
        Debug.Log("Color del vello: " + CharacterEdit.facehairColor);
        Debug.Log("Estilo del vello: " + CharacterEdit.facehairStyle);
        Debug.Log("Opacidad del pelo: " + CharacterEdit.facehairAlpha);
        Debug.Log("Color de gafas: " + CharacterEdit.glassesColor);
        Debug.Log("Estilo de gafas: " + CharacterEdit.glassesStyle);
        Debug.Log("Color del collar: " + CharacterEdit.collarColor);
        Debug.Log("Estilo del collar: " + CharacterEdit.collarStyle);
        Debug.Log("Nombre: " + CharacterEdit.characterName);
        Debug.Log("Género: " + CharacterEdit.characterGender);

        string postURL = saveCharacterURL +
            "?skintone=" + CharacterEdit.skinTone +
            "&eyecolor=" + CharacterEdit.eyeColor +
            "&haircolor=" + CharacterEdit.hairColor +
            "&hairstyle=" + CharacterEdit.hairStyle +
            "&shirtcolor=" + CharacterEdit.shirtColor +
            "&shirtstyle=" + CharacterEdit.shirtStyle +
            "&pantscolor=" + CharacterEdit.pantsColor +
            "&pantsstyle=" + CharacterEdit.pantsStyle +
            "&shoecolor=" + CharacterEdit.shoeColor +
            "&shoestyle=" + CharacterEdit.shoeStyle +
            "&facehaircolor=" + CharacterEdit.facehairColor +
            "&facehairstyle=" + CharacterEdit.facehairStyle +
            "&facehairalpha=" + CharacterEdit.facehairAlpha +
            "&glassescolor=" + CharacterEdit.glassesColor +
            "&glassesstyle=" + CharacterEdit.glassesStyle +
            "&collarcolor=" + CharacterEdit.collarColor +
            "&collarstyle=" + CharacterEdit.collarStyle +
            "&charname=" + CharacterEdit.characterName +
            "&chargender=" + CharacterEdit.characterGender;

        Debug.Log(postURL);
        StartCoroutine(sendCharacter(postURL));

        Debug.Log("Done!");
    }

    public void loadCharacter()
    {
        StartCoroutine(getCharacter(loadCharacterURL));
    }

    IEnumerator sendCharacter(string fullURL)
    {
        UnityWebRequest charPost = UnityWebRequest.Post(fullURL, "");
        yield return charPost.SendWebRequest();
        Debug.Log(charPost.responseCode);
        Debug.Log(charPost.downloadHandler.text);
    }

    IEnumerator getCharacter(string fullURL)
    {
        UnityWebRequest charGet = UnityWebRequest.Get(fullURL);
        yield return charGet.SendWebRequest();
        Debug.Log(charGet.responseCode);

        string charDataText = charGet.downloadHandler.text;
        Debug.Log(charDataText);

        if (!charDataText.Contains("null")) {
            Dictionary<string, string> charData = new Dictionary<string, string>();

            string[] charFields = charDataText.Split('\n');
            foreach (string field in charFields)
            {
                string[] currentField = field.Split(',');
                charData[currentField[0]] = currentField[1];
            }

            //Cambiando los datos del personaje
            GameObject avatar = GameObject.Find("Avatar");
            GameObject avatarOjos = GameObject.Find("AvatarOjos");
            GameObject avatarPelo = GameObject.Find("AvatarPelo");
            GameObject avatarCamiseta = GameObject.Find("AvatarCamiseta");

            //Abriendo todos los menús
            EnableCustomizationMenus.enableSingleMenu("CARA");
            EnableCustomizationMenus.enableSingleMenu("PELO");
            EnableCustomizationMenus.enableSingleMenu("CAMISETAS");
            EnableCustomizationMenus.enableSingleMenu("PANTALONES");
            EnableCustomizationMenus.enableSingleMenu("ACCESORIOS");
            EnableCustomizationMenus.enableSingleMenu("FINAL");

            //Atributo 1: Tono de piel
            GameObject tonosPiel = GameObject.Find("TonoPiel");
            foreach(Transform button in tonosPiel.transform)
            {
                button.GetComponent<Image>().color = new Color(255, 255, 255);
            }

            Transform botonPiel = tonosPiel.transform.GetChild(int.Parse(charData["SKINTONE"]));
            botonPiel.gameObject.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            avatar.GetComponent<SpriteRenderer>().color = botonPiel.GetChild(0).GetComponent<Image>().color;

            //Atributo 2: Color de ojos
            GameObject colorOjos = GameObject.Find("ColorOjos");
            foreach (Transform button in colorOjos.transform)
            {
                button.GetComponent<Image>().color = new Color(255, 255, 255);
            }

            Transform botonOjos = colorOjos.transform.GetChild(int.Parse(charData["EYECOLOR"]));
            botonOjos.gameObject.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            avatarOjos.GetComponent<SpriteRenderer>().color = botonOjos.GetChild(0).GetComponent<Image>().color;

            //Atributo 3: Color de pelo
            GameObject colorPelo = GameObject.Find("ColorPelo");
            foreach (Transform button in colorPelo.transform)
            {
                button.GetComponent<Image>().color = new Color(255, 255, 255);
            }

            int found = 7;
            switch(charData["HAIRCOLOR"])
            {
                case "181717": found = 0; break;
                case "3F1F10": found = 1; break;
                case "5B1808": found = 2; break;
                case "7B3A14": found = 3; break;
                case "4E3D0F": found = 4; break;
                case "885818": found = 5; break;
                case "D2A04B": found = 6; break;
            }

            GameObject botonColorPelo = colorPelo.transform.GetChild(found).gameObject;
            botonColorPelo.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            Color hairColor;
            if(ColorUtility.TryParseHtmlString("#" + charData["HAIRCOLOR"] + "FF", out hairColor)) 
            {
                avatarPelo.GetComponent<SpriteRenderer>().color = hairColor;
            }

            //Atributo 4: Estilo de pelo
            GameObject estiloPelo = GameObject.Find("Peinado");
            foreach (Transform button in estiloPelo.transform)
            {
                button.GetComponent<Image>().color = new Color(255, 255, 255);
            }

            Transform botonEstiloPelo = estiloPelo.transform.GetChild(int.Parse(charData["HAIRSTYLE"]));
            botonEstiloPelo.gameObject.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            avatarPelo.gameObject.GetComponent<SpriteRenderer>().sprite = 
                SpriteListsCharacter.pelosFinal[int.Parse(charData["HAIRSTYLE"])];

            //Atributo 5: Color de camiseta
            Color shirtColor;
            if (ColorUtility.TryParseHtmlString("#" + charData["SHIRTCOLOR"] + "FF", out shirtColor))
            {
                avatarCamiseta.GetComponent<SpriteRenderer>().color = shirtColor;
            }

            //Atributo 6: Estilo de camiseta
            GameObject estiloCamiseta = GameObject.Find("CamisetaBasica");
            foreach (Transform button in estiloCamiseta.transform)
            {
                button.GetComponent<Image>().color = new Color(255, 255, 255);
            }

            Transform botonCamiseta = estiloCamiseta.transform.GetChild(int.Parse(charData["SHIRTSTYLE"]));
            botonCamiseta.gameObject.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            avatarCamiseta.gameObject.GetComponent<SpriteRenderer>().sprite =
                SpriteListsCharacter.camisetasBasicasFinal[int.Parse(charData["SHIRTSTYLE"])];

            //Abriendo menú por defecto
            EnableCustomizationMenus.hideEverything();
            EnableCustomizationMenus.enableSingleMenu("BOTONES");
            EnableCustomizationMenus.enableSingleMenu("CARA");
        }
    }
}
