using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject instructionCanvas;

    public GameObject tutorialImg;
    public GameObject tutorialTxt;

    public string[] instructionTxt;
    public Sprite[] instructionImg;

    public static bool fromMainMenu;
    private string checkAvatar = "http://localhost/moodle/unity/checkAvatar.php";

    private int page;
    private int maxSize;

    // Start is called before the first frame update
    void Start()
    {
        fromMainMenu = false;
        page = 0;
        maxSize = instructionTxt.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        StartCoroutine(startGameCoroutine());
    }

    private void updateInstructions()
    {
        tutorialImg.GetComponent<Image>().sprite = instructionImg[page];
        tutorialTxt.GetComponent<Text>().text = instructionTxt[page];
    }

    public void showInstructions()
    {
        mainCanvas.SetActive(false);
        instructionCanvas.SetActive(true);
        updateInstructions();
    }

    public void nextPage()
    {
        if(!(page + 1 >= maxSize)) page++;
        updateInstructions();
    }

    public void prevPage()
    {
        if(!(page <= 0)) page--;
        updateInstructions();
    }

    public void hideInstructions()
    {
        instructionCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public IEnumerator startGameCoroutine()
    {
        UnityWebRequest avatarGet = UnityWebRequest.Get(checkAvatar);
        yield return avatarGet.SendWebRequest();

        if (avatarGet.downloadHandler.text.Contains("yes"))
        {
            fromMainMenu = true;
        }
        SceneManager.LoadScene(1);
    }
}
