using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Battle")
        {
            StartCoroutine(endBattleTransitionCoroutine());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startBattleTransition()
    {
        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Stop();
        GameObject.Find("BattleStart").GetComponent<AudioSource>().Play();
        StartCoroutine(startBattleTransitionCoroutine());
    }

    public IEnumerator startBattleTransitionCoroutine()
    {
        Vector2 currentPos = transform.position;
        Vector2 finalPosition = new Vector2(0f, 0f);
        float timeToMove = 0.5f;
        float progress = 0f;

        while (progress < 1)
        {
            progress += Time.deltaTime / timeToMove;
            transform.position = Vector2.Lerp(currentPos, finalPosition, progress);
            yield return null;
        }

        SceneManager.LoadScene(2);
    }

    public IEnumerator showBattleData()
    {
        Debug.Log("TBD");
        yield return null;
    }

    public IEnumerator endBattleTransitionCoroutine()
    {
        yield return new WaitForSeconds(0.3f);

        Vector2 currentPos = transform.position;
        Vector2 finalPosition = new Vector2(0f, 11f);
        float timeToMove = 0.5f;
        float progress = 0f;

        while (progress < 1)
        {
            progress += Time.deltaTime / timeToMove;
            transform.position = Vector2.Lerp(currentPos, finalPosition, progress);
            yield return null;
        }

        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Play();
    }
}
