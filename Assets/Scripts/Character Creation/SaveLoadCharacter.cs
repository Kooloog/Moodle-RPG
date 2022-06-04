using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoadCharacter : MonoBehaviour
{
    private string saveCharacterURL = "http://localhost/moodle/unity/uploadCharacter.php";
    private string loadCharacterURL = "http://localhost/moodle/unity/loadCharacterData.php";

    public void saveCharacter()
    {
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
        SceneManager.LoadScene(1);
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
            GameObject avatarPantalon = GameObject.Find("AvatarPantalon");
            GameObject avatarCalzado = GameObject.Find("AvatarCalzado");
            GameObject avatarVello = GameObject.Find("AvatarVello");
            GameObject avatarGafas = GameObject.Find("AvatarGafas");
            GameObject avatarCollar = GameObject.Find("AvatarCollar");

            //Atributo 1: Tono de piel
            GameObject tonosPiel = GameObject.Find("TonoPiel");
            foreach(Transform button in tonosPiel.transform)
            {
                button.GetComponent<Image>().color = new Color(255, 255, 255);
            }

            Transform botonPiel = tonosPiel.transform.GetChild(int.Parse(charData["SKINTONE"]));
            botonPiel.gameObject.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            avatar.GetComponent<SpriteRenderer>().color = botonPiel.GetChild(0).GetComponent<Image>().color;
            CharacterEdit.skinTone = int.Parse(charData["SKINTONE"]);

            //Atributo 2: Color de ojos
            GameObject colorOjos = GameObject.Find("ColorOjos");
            foreach (Transform button in colorOjos.transform)
            {
                button.GetComponent<Image>().color = new Color(255, 255, 255);
            }

            Transform botonOjos = colorOjos.transform.GetChild(int.Parse(charData["EYECOLOR"]));
            botonOjos.gameObject.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            avatarOjos.GetComponent<SpriteRenderer>().color = botonOjos.GetChild(0).GetComponent<Image>().color;
            CharacterEdit.eyeColor = int.Parse(charData["EYECOLOR"]);

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
            CharacterEdit.hairColor = charData["HAIRCOLOR"];

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
            CharacterEdit.hairStyle = int.Parse(charData["HAIRSTYLE"]);

            //Atributo 5: Color de camiseta
            Color shirtColor;
            if (ColorUtility.TryParseHtmlString("#" + charData["SHIRTCOLOR"] + "FF", out shirtColor))
            {
                avatarCamiseta.GetComponent<SpriteRenderer>().color = shirtColor;
            }
            CharacterEdit.shirtColor = charData["SHIRTCOLOR"];

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
            CharacterEdit.shirtStyle = int.Parse(charData["SHIRTSTYLE"]);

            //Atributo 7: Color de pantalón
            Color pantsColor;
            if (ColorUtility.TryParseHtmlString("#" + charData["PANTSCOLOR"] + "FF", out pantsColor))
            {
                avatarPantalon.GetComponent<SpriteRenderer>().color = pantsColor;
            }
            CharacterEdit.pantsColor = charData["PANTSCOLOR"];

            //Atributo 8: Estilo de pantalón
            GameObject estiloPantalon = GameObject.Find("Pantalones");
            foreach (Transform button in estiloPantalon.transform)
            {
                button.GetComponent<Image>().color = new Color(255, 255, 255);
            }

            Transform botonPantalon = estiloPantalon.transform.GetChild(int.Parse(charData["PANTSSTYLE"]));
            botonPantalon.gameObject.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            avatarPantalon.gameObject.GetComponent<SpriteRenderer>().sprite =
                SpriteListsCharacter.pantalonesFinal[int.Parse(charData["PANTSSTYLE"])];
            CharacterEdit.pantsStyle = int.Parse(charData["PANTSSTYLE"]);

            //Atributo 9: Color de calzado
            Color shoeColor;
            if (ColorUtility.TryParseHtmlString("#" + charData["SHOECOLOR"] + "FF", out shoeColor))
            {
                avatarCalzado.GetComponent<SpriteRenderer>().color = shoeColor;
            }
            CharacterEdit.shoeColor = charData["SHOECOLOR"];

            //Atributo 10: Estilo de calzado
            GameObject estiloCalzado = GameObject.Find("Calzado");
            foreach (Transform button in estiloCalzado.transform)
            {
                button.GetComponent<Image>().color = new Color(255, 255, 255);
            }

            Transform botonCalzado = estiloCalzado.transform.GetChild(int.Parse(charData["SHOESTYLE"]));
            botonCalzado.gameObject.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            avatarCalzado.gameObject.GetComponent<SpriteRenderer>().sprite =
                SpriteListsCharacter.calzadoFinal[int.Parse(charData["SHOESTYLE"])];
            CharacterEdit.shoeStyle = int.Parse(charData["SHOESTYLE"]);

            //Atributo 11: Color de vello
            GameObject colorVello = GameObject.Find("ColorVello");
            foreach (Transform button in colorVello.transform)
            {
                button.GetComponent<Image>().color = new Color(255, 255, 255);
            }

            switch (charData["FACEHAIRCOLOR"])
            {
                case "181717": found = 0; break;
                case "3F1F10": found = 1; break;
                case "5B1808": found = 2; break;
                case "7B3A14": found = 3; break;
                case "4E3D0F": found = 4; break;
                case "885818": found = 5; break;
                case "D2A04B": found = 6; break;
            }

            GameObject botonColorVello = colorVello.transform.GetChild(found).gameObject;
            botonColorVello.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            Color velloColor;
            if (ColorUtility.TryParseHtmlString("#" + charData["FACEHAIRCOLOR"] + "FF", out velloColor))
            {
                avatarVello.GetComponent<SpriteRenderer>().color = velloColor;
            }
            CharacterEdit.facehairColor = charData["FACEHAIRCOLOR"];

            //Atributo 12: Estilo de vello
            GameObject estiloVello = GameObject.Find("Vello");
            foreach (Transform button in estiloVello.transform)
            {
                button.GetComponent<Image>().color = new Color(255, 255, 255);
            }

            Transform botonVello = estiloVello.transform.GetChild(int.Parse(charData["FACEHAIRSTYLE"]));
            botonVello.gameObject.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            avatarVello.gameObject.GetComponent<SpriteRenderer>().sprite =
                SpriteListsCharacter.velloFinal[int.Parse(charData["FACEHAIRSTYLE"])];
            CharacterEdit.facehairStyle = int.Parse(charData["FACEHAIRSTYLE"]);

            //Atributo 13: Opacidad de vello
            GameObject alphaVello = GameObject.Find("SliderVello");
            alphaVello.GetComponent<Slider>().value = float.Parse(charData["FACEHAIRALPHA"]);
            Color colorVelloAlpha = avatarVello.gameObject.GetComponent<SpriteRenderer>().color;
            colorVelloAlpha = new Color(colorVelloAlpha.r, colorVelloAlpha.g, colorVelloAlpha.b, 
                float.Parse(charData["FACEHAIRALPHA"]));
            CharacterEdit.facehairAlpha = float.Parse(charData["FACEHAIRALPHA"]);

            //Atributo 14: Color de gafas
            Color gafasColor;
            if (ColorUtility.TryParseHtmlString("#" + charData["GLASSESCOLOR"] + "FF", out gafasColor))
            {
                avatarGafas.GetComponent<SpriteRenderer>().color = gafasColor;
            }
            CharacterEdit.glassesColor = charData["GLASSESCOLOR"];

            //Atributo 15: Estilo de gafas
            GameObject estiloGafas = GameObject.Find("Gafas");
            foreach (Transform button in estiloGafas.transform)
            {
                button.GetComponent<Image>().color = new Color(255, 255, 255);
            }

            Transform botonGafas = estiloGafas.transform.GetChild(int.Parse(charData["GLASSESSTYLE"]));
            botonGafas.gameObject.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            avatarGafas.gameObject.GetComponent<SpriteRenderer>().sprite =
                SpriteListsCharacter.gafasFinal[int.Parse(charData["GLASSESSTYLE"])];
            CharacterEdit.glassesStyle = int.Parse(charData["GLASSESSTYLE"]);

            //Atributo 16: Color de collar
            Color collarColor;
            if (ColorUtility.TryParseHtmlString("#" + charData["COLLARCOLOR"] + "FF", out collarColor))
            {
                avatarCollar.GetComponent<SpriteRenderer>().color = collarColor;
            }
            CharacterEdit.collarColor = charData["COLLARCOLOR"];

            //Atributo 17: Estilo de collar
            GameObject estiloCollar = GameObject.Find("Collares");
            foreach (Transform button in estiloCollar.transform)
            {
                button.GetComponent<Image>().color = new Color(255, 255, 255);
            }

            Transform botonCollar = estiloCollar.transform.GetChild(int.Parse(charData["COLLARSTYLE"]));
            botonCollar.gameObject.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            avatarCollar.gameObject.GetComponent<SpriteRenderer>().sprite =
                SpriteListsCharacter.collaresFinal[int.Parse(charData["COLLARSTYLE"])];
            CharacterEdit.collarStyle = int.Parse(charData["COLLARSTYLE"]);

            //Atributo 18: Nombre
            GameObject.Find("NombrePersonaje").GetComponent<InputField>().text = charData["CHARNAME"];
            CharacterEdit.characterName = charData["CHARNAME"];
            GameObject.Find("Acabar").GetComponent<Button>().interactable = true;

            //Atributo 19: Género
            if (charData["CHARGENDER"] == "Male")
            {
                GameObject.Find("Male").GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
                GameObject.Find("Female").GetComponent<Image>().color = new Color(255, 255, 255);
            }
            else if(charData["CHARGENDER"] == "Female")
            {
                GameObject.Find("Female").GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
                GameObject.Find("Male").GetComponent<Image>().color = new Color(255, 255, 255);
            }
            CharacterEdit.characterGender = charData["CHARGENDER"];
        }

        //Abriendo menú por defecto
        EnableCustomizationMenus.hideEverything();
        EnableCustomizationMenus.enableSingleMenu("BOTONES");
        EnableCustomizationMenus.enableSingleMenu("CARA");
    }
}
