using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public Sprite avatarBody;

    private List<GameObject> rankList;
    private string rankScoresURL = "http://localhost/moodle/unity/getPlayerRanking.php";

    public void loadRanking()
    {
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

            avatar.GetComponent<Image>().sprite = avatarBody;
            avatar.GetComponent<Image>().color = assignSkinTone(int.Parse(rank[current]["SKINTONE"]));
            //avatar.GetChild()

            current++;
        }

        for(int i=current; i<10; i++)
        {
            Debug.Log(i);
            GameObject.Find("RankingPlacements").transform.GetChild(i).gameObject.SetActive(false);
        }
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

    // Start is called before the first frame update
    void Start()
    {
        rankList = new List<GameObject>();

        foreach(Transform rank in GameObject.Find("RankingPlacements").transform) rankList.Add(rank.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
