using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StatLoader : MonoBehaviour
{
    private Text txtVida;
    private Text txtPuntos;
    private Text txtMonedas;
    private Slider sliderVida;

    private string loadStatsURL = "http://localhost/moodle/unity/loadUIStats.php";
    private string manageStatsURL = "http://localhost/moodle/unity/UIStatsManager.php";

    void Start()
    {
        txtVida = GameObject.Find("TextVida").GetComponent<Text>(); 
        txtPuntos = GameObject.Find("TextPuntos").GetComponent<Text>(); 
        txtMonedas = GameObject.Find("TextMonedas").GetComponent<Text>();
        sliderVida = GameObject.Find("SliderVida").GetComponent<Slider>();

        StartCoroutine(loadStats());
    }

    IEnumerator loadStats()
    {
        UnityWebRequest statGet = UnityWebRequest.Get(loadStatsURL);
        yield return statGet.SendWebRequest();

        string statDataText = statGet.downloadHandler.text;
        Debug.Log(statDataText);

        if (statDataText.Contains("doesn't exist"))
        {
            UnityWebRequest statPost = UnityWebRequest.Post(manageStatsURL, "");
            yield return statPost.SendWebRequest();
            StartCoroutine(loadStats());
        }
        else
        {
            Dictionary<string, string> statData = new Dictionary<string, string>();

            string[] statFields = statDataText.Split('\n');
            foreach (string field in statFields)
            {
                string[] currentField = field.Split(',');
                statData[currentField[0]] = currentField[1];
            }

            txtVida.text = statData["HEALTH"] + "/" + statData["MAXHEALTH"];
            txtPuntos.text = statData["SCORE"];
            txtMonedas.text = statData["COINS"];

            sliderVida.maxValue = float.Parse(statData["MAXHEALTH"]);
            sliderVida.value = float.Parse(statData["HEALTH"]);
        }
    }
}
