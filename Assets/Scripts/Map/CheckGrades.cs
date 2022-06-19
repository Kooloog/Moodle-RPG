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
        Debug.Log("RECIBIDO: " + gradeDataText);

        if(!gradeDataText.Contains("null"))
        {
            int totalCoins = int.Parse(gradeDataText);

            GameObject load = new GameObject("IncreaseCoins");
            StatManager coinIncrease = load.AddComponent<StatManager>();
            coinIncrease.increaseCoins(totalCoins);
            Destroy(load);

            MapHandler.gradeCanvas.SetActive(true);
            GameObject.Find("CoinGradesText").GetComponent<Text>().text = totalCoins.ToString();
        }
    }
}
