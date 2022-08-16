using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public Sprite avatarBody;
    public GameObject loading;

    private List<GameObject> rankList;
    private string rankScoresURL = "http://localhost/moodle/unity/getPlayerRanking.php";
    private string playerPositionURL = "http://localhost/moodle/unity/getCurrentPlayerRank.php";

    public void loadRanking()
    {
        loading.SetActive(true);
        StartCoroutine(scoreRanking());
    }

    IEnumerator scoreRanking()
    {
        UnityWebRequest rankGet = UnityWebRequest.Get(rankScoresURL);
        yield return rankGet.SendWebRequest();
        string rankDataText = rankGet.downloadHandler.text;

        List<Dictionary<string, string>> rank = new List<Dictionary<string, string>>();
        int current = 0;

        string[] rankEntries = rankDataText.Split('|');
        foreach (string entry in rankEntries)
        {
            Dictionary<string, string> rankData = new Dictionary<string, string>();
            string[] rankFields = entry.Split('\n');
            foreach (string field in rankFields)
            {
                string[] currentField = field.Split(',');
                rankData[currentField[0]] = currentField[1];
            }

            rank.Add(rankData);

            //PASO 1: Formar el avatar del personaje actualmente cargado
            GameObject avatar = rankList[current].transform.GetChild(0).gameObject;

            //Cuerpo
            avatar.GetComponent<Image>().sprite = avatarBody;
            avatar.GetComponent<Image>().color = assignSkinTone(int.Parse(rank[current]["SKINTONE"]));

            //Ojos
            avatar.transform.GetChild(1).gameObject.GetComponent<Image>().color =
                assignHairColor(int.Parse(rank[current]["EYECOLOR"]));

            //Pelo
            Color hairColor;
            ColorUtility.TryParseHtmlString("#" + rank[current]["HAIRCOLOR"] + "FF", out hairColor);
            avatar.transform.GetChild(2).gameObject.GetComponent<Image>().sprite =
                SpriteListsCharacter.pelosFinal[int.Parse(rank[current]["HAIRSTYLE"])];
            avatar.transform.GetChild(2).gameObject.GetComponent<Image>().color = hairColor;

            //Camiseta
            Color shirtColor;
            ColorUtility.TryParseHtmlString("#" + rank[current]["SHIRTCOLOR"] + "FF", out shirtColor);
            avatar.transform.GetChild(3).gameObject.GetComponent<Image>().sprite =
                SpriteListsCharacter.camisetasBasicasFinal[int.Parse(rank[current]["SHIRTSTYLE"])];
            avatar.transform.GetChild(3).gameObject.GetComponent<Image>().color = shirtColor;

            //Pantalon
            Color pantsColor;
            ColorUtility.TryParseHtmlString("#" + rank[current]["PANTSCOLOR"] + "FF", out pantsColor);
            avatar.transform.GetChild(4).gameObject.GetComponent<Image>().sprite =
                SpriteListsCharacter.pantalonesFinal[int.Parse(rank[current]["PANTSSTYLE"])];
            avatar.transform.GetChild(4).gameObject.GetComponent<Image>().color = pantsColor;

            //Zapatos
            Color shoeColor;
            ColorUtility.TryParseHtmlString("#" + rank[current]["SHOECOLOR"] + "FF", out shoeColor);
            avatar.transform.GetChild(5).gameObject.GetComponent<Image>().sprite =
                SpriteListsCharacter.calzadoFinal[int.Parse(rank[current]["SHOESTYLE"])];
            avatar.transform.GetChild(5).gameObject.GetComponent<Image>().color = shoeColor;

            //Vello
            Color facehairColor;
            ColorUtility.TryParseHtmlString("#" + rank[current]["FACEHAIRCOLOR"] + "FF", out facehairColor);
            avatar.transform.GetChild(6).gameObject.GetComponent<Image>().sprite =
                SpriteListsCharacter.velloFinal[int.Parse(rank[current]["FACEHAIRSTYLE"])];
            Color colorVelloAlpha = new Color(facehairColor.r, facehairColor.g, facehairColor.b, 
                float.Parse(rank[current]["FACEHAIRALPHA"]));
            avatar.transform.GetChild(6).gameObject.GetComponent<Image>().color = facehairColor;

            //Gafas
            Color glassesColor;
            ColorUtility.TryParseHtmlString("#" + rank[current]["GLASSESCOLOR"] + "FF", out glassesColor);
            avatar.transform.GetChild(7).gameObject.GetComponent<Image>().sprite =
                SpriteListsCharacter.gafasFinal[int.Parse(rank[current]["GLASSESSTYLE"])];
            avatar.transform.GetChild(7).gameObject.GetComponent<Image>().color = glassesColor;

            //Collar
            Color collarColor;
            ColorUtility.TryParseHtmlString("#" + rank[current]["COLLARCOLOR"] + "FF", out collarColor);
            avatar.transform.GetChild(8).gameObject.GetComponent<Image>().sprite =
                SpriteListsCharacter.collaresFinal[int.Parse(rank[current]["COLLARSTYLE"])];
            avatar.transform.GetChild(8).gameObject.GetComponent<Image>().color = collarColor;

            //PASO 2: Cambiar nombre y puntuación
            rankList[current].transform.GetChild(2).GetComponent<Text>().text = rank[current]["CHARNAME"].ToString();
            rankList[current].transform.GetChild(3).GetComponent<Text>().text = rank[current]["SCORE"].ToString();

            //Paso 3: Obtener rank del jugador actual
            UnityWebRequest positionGet = UnityWebRequest.Get(playerPositionURL);
            yield return positionGet.SendWebRequest();
            string positionDataText = positionGet.downloadHandler.text;

            if (positionDataText.Length == 1) positionDataText = "0" + positionDataText;
            GameObject.Find("TuRank").GetComponent<Text>().text = positionDataText;

            current++;
        }

        for(int i=current; i<10; i++)
            GameObject.Find("RankingPlacements").transform.GetChild(i).gameObject.SetActive(false);

        loading.SetActive(false);
    }

    private Color assignSkinTone(int value)
    {
        Color c = new Color();
        switch(value)
        {
            case 0: c = new Color(1f, 0.8274f, 0.6745f, 1f); break;
            case 1: c = new Color(1f, 0.7411f, 0.6392f, 1f); break;
            case 2: c = new Color(1f, 0.7098f, 0.4156f, 1f); break;
            case 3: c = new Color(0.8705f, 0.4745f, 0.2549f, 1f); break;
            case 4: c = new Color(0.5490f, 0.2352f, 0.1372f, 1f); break;
            case 5: c = new Color(0.2313f, 0.1724f, 0.1333f, 1f); break;
        }
        return c;
    }

    private Color assignHairColor(int value)
    {
        Color c = new Color();
        switch (value)
        {
            case 0: c = new Color(0.3960f, 0.2313f, 0.1686f, 1f); break;
            case 1: c = new Color(0.2705f, 0.3294f, 0.6549f, 1f); break;
            case 2: c = new Color(0.2196f, 0.4392f, 0.3411f, 1f); break;
            case 3: c = new Color(0.4235f, 0.4392f, 0.4431f, 1f); break;
            case 4: c = new Color(0.3764f, 0.3686f, 0.1803f, 1f); break;
            case 5: c = new Color(0f, 0f, 0f, 1f); break;
            case 6: c = new Color(0.7647f, 0.5490f, 0.2745f, 1f); break;
            case 7: c = new Color(0.5294f, 0.8078f, 0.9215f, 1f); break;
            case 8: c = new Color(0.7803f, 0.0509f, 0.0509f, 1f); break;
        }
        return c;
    }

    // Start is called before the first frame update
    void Start()
    {
        rankList = new List<GameObject>();
        foreach(Transform rank in GameObject.Find("RankingPlacements").transform) rankList.Add(rank.gameObject);
        MapHandler.rankingCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
