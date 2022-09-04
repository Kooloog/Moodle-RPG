using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CheckGrades : MonoBehaviour
{
    private static string checkGradesURL = "http://localhost/moodle/unity/checkGrades.php";

    public static IEnumerator checkDBGrades()
    {
        UnityWebRequest gradeTotalGet = UnityWebRequest.Get(checkGradesURL);
        yield return gradeTotalGet.SendWebRequest();

        string gradeDataText = gradeTotalGet.downloadHandler.text;

        if(!gradeDataText.Contains("null"))
        {
            int totalCoins = int.Parse(gradeDataText);
            string totalCoinsText = (Stats.coins + totalCoins).ToString();

            GameObject load = new GameObject("IncreaseCoins");
            StatManager coinIncrease = load.AddComponent<StatManager>();
            coinIncrease.increaseCoins(totalCoins);
            Destroy(load);

            MapHandler.gradeCanvas.SetActive(true);
            GameObject.Find("CoinGradesText").GetComponent<Text>().text = totalCoins.ToString();

            yield return new WaitForSeconds(1.0f);
            GameObject.Find("TextMonedas").GetComponent<Text>().text = totalCoinsText;
        }
    }
}
