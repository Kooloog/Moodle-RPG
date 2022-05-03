using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SaveLoadCharacter : MonoBehaviour
{
    private string saveCharacterURL = "http://localhost/moodle/unity/uploadCharacter.php";

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

    IEnumerator sendCharacter(string fullURL)
    {
        UnityWebRequest charPost = UnityWebRequest.Post(fullURL, "");
        yield return charPost.SendWebRequest();
        Debug.Log(charPost.responseCode);
        Debug.Log(charPost.downloadHandler.text);
    }
}
