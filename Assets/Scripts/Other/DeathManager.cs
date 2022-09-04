using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour
{
    public GameObject deathMessage;
    public GameObject reviveMessage;

    public static bool isDead;
    private string urlMessage;

    private static string killPlayerURL = "http://localhost/moodle/unity/loadPlayerDeath.php";
    private static string checkAliveURL = "http://localhost/moodle/unity/checkPlayerDeath.php";
    private static string isPlayerDeadURL = "http://localhost/moodle/unity/isPlayerDead.php";

    private StatManager statManager;

    public void killPlayer()
    {
        StartCoroutine(sendDeath());
    }

    public void checkAlive()
    {
        statManager = GameObject.Find("StatsManager").GetComponent<StatManager>();
        urlMessage = "none";
        StartCoroutine(loadDeath());
    }

    IEnumerator sendDeath()
    {
        UnityWebRequest deathPost = UnityWebRequest.Post(killPlayerURL, "");
        yield return deathPost.SendWebRequest();
    }

    IEnumerator checkDeath()
    {
        UnityWebRequest deathGet = UnityWebRequest.Post(isPlayerDeadURL, "");
        yield return deathGet.SendWebRequest();
        urlMessage = deathGet.downloadHandler.text;
        if(urlMessage.Contains("yes")) SceneManager.LoadScene(2);
    }

    IEnumerator loadDeath()
    {
        UnityWebRequest deathGet = UnityWebRequest.Post(checkAliveURL, "");
        yield return deathGet.SendWebRequest();
        urlMessage = deathGet.downloadHandler.text;

        if (urlMessage.Contains("alive"))
        {
            //Do nothing. Player is alive.
        }
        else if (urlMessage.Contains("revive"))
        {
            isDead = true;
            reviveMessage.SetActive(true);
            statManager.increaseHealth(Stats.maxHealth);
        }
        else if (urlMessage.Contains("doesn't exist"))
        {
            //Do nothing. The player was just created.
        }
        else
        {
            isDead = true;
            string[] time = urlMessage.Split(':');

            deathMessage.SetActive(true);
            GameObject.Find("MuerteHora").GetComponent<Text>().text = time[0];
            GameObject.Find("MuerteMinuto").GetComponent<Text>().text = time[1];
        }
    }

    public void reviveButton()
    {
        isDead = false;
        reviveMessage.SetActive(false);
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name.StartsWith("Editor")) StartCoroutine(checkDeath());
    }
}
