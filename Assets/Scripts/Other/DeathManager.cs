using System;
using System.Collections;
using System.Collections.Generic;
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
        if(urlMessage.Contains("yes")) SceneManager.LoadScene(1);
    }

    IEnumerator loadDeath()
    {
        UnityWebRequest deathGet = UnityWebRequest.Post(checkAliveURL, "");
        yield return deathGet.SendWebRequest();
        urlMessage = deathGet.downloadHandler.text;
        Debug.Log(urlMessage);

        if (urlMessage.Contains("alive"))
        {
            Debug.Log("player is alive");
        }
        else if (urlMessage.Contains("revive"))
        {
            isDead = true;
            reviveMessage.SetActive(true);
            statManager.increaseHealth(Stats.maxHealth);
        }
        else
        {
            isDead = true;
            string[] time = urlMessage.Split(':');
            Debug.Log(time[0] + ":" + time[1] + ":" + time[2]);

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
