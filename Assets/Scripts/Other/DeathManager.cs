using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeathManager : MonoBehaviour
{
    private static string killPlayerURL = "http://localhost/moodle/unity/loadPlayerDeath.php";

    public void killPlayer()
    {
        StartCoroutine(sendDeath());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator sendDeath()
    {
        UnityWebRequest charPost = UnityWebRequest.Post(killPlayerURL, "");
        yield return charPost.SendWebRequest();
        Debug.Log(charPost.responseCode);
        Debug.Log(charPost.downloadHandler.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
