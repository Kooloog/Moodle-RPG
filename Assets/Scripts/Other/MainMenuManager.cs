using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject instructionCanvas;
    public GameObject creditsCanvas;

    public GameObject tutorialImg;
    public GameObject tutorialTxt;
    public GameObject tutorialPage;

    public string[] instructionTxt;
    public Sprite[] instructionImg;

    public GameObject loadScreen;

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

    public void startGame()
    {
        loadScreen.SetActive(true);
        StartCoroutine(startGameCoroutine());
    }

    private void updateInstructions()
    {
        int currentPage = page + 1;
        tutorialImg.GetComponent<Image>().sprite = instructionImg[page];
        tutorialTxt.GetComponent<Text>().text = instructionTxt[page];
        tutorialPage.GetComponent<Text>().text = currentPage + "/" + maxSize;
    }

    public void showInstructions()
    {
        mainCanvas.SetActive(false);
        instructionCanvas.SetActive(true);
        updateInstructions();
    }

    public void showCredits()
    {
        mainCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void nextPage()
    {
        if (!(page + 1 >= maxSize)) page++;   
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

    public void hideCredits()
    {
        creditsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public IEnumerator startGameCoroutine()
    {
        UnityWebRequest avatarGet = UnityWebRequest.Get(checkAvatar);
        yield return avatarGet.SendWebRequest();

        if (avatarGet.downloadHandler.text.Contains("yes")) fromMainMenu = true;

        SceneManager.LoadScene(1);
    }
}
