using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapHandler : MonoBehaviour
{
    public static bool activated;

    public static GameObject houseCanvas;
    public static GameObject forjaCanvas;
    public static GameObject tiendaCanvas;

    public static GameObject avatarCanvas;
    public static GameObject ataqueCanvas;
    public static GameObject defensaCanvas;
    public static GameObject inventoryCanvas;
    public static GameObject gradeCanvas;
    public static GameObject adventureCanvas;
    public static GameObject rankingCanvas;

    private static AudioSource menuSound;
    private static AudioSource openMapSound;
    private static GameObject noDinero;
    private static GameObject noSpace;
    private static bool playSound;

    // Start is called before the first frame update
    void Start()
    {
        houseCanvas = GameObject.Find("HouseCanvas");
        forjaCanvas = GameObject.Find("ForjaCanvas");
        tiendaCanvas = GameObject.Find("TiendaCanvas");

        avatarCanvas = GameObject.Find("AvatarCanvas");
        ataqueCanvas = GameObject.Find("MenuAtaque");
        defensaCanvas = GameObject.Find("MenuDefensa");
        inventoryCanvas = GameObject.Find("InventoryCanvas");
        gradeCanvas = GameObject.Find("GradeCheckCanvas");
        adventureCanvas = GameObject.Find("AdventureCanvas");
        rankingCanvas = GameObject.Find("RankingCanvas");

        menuSound = GameObject.Find("MenuSelect").GetComponent<AudioSource>();
        openMapSound = GameObject.Find("OpenMap").GetComponent<AudioSource>();
        noDinero = GameObject.Find("NoDinero");
        noSpace = GameObject.Find("NoEspacio");

        MapTriggers.linkMapTriggers();

        //Preparando canvas de la casa del personaje
        avatarCanvas.GetComponent<Image>().sprite = 
            GameObject.Find("Avatar").GetComponent<SpriteRenderer>().sprite;
        avatarCanvas.GetComponent<Image>().color =
            GameObject.Find("Avatar").GetComponent<SpriteRenderer>().color;

        for(int i=0; i<avatarCanvas.transform.childCount; i++)
        {
            avatarCanvas.transform.GetChild(i).GetComponent<Image>().sprite =
                GameObject.Find("Avatar").transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;

            avatarCanvas.transform.GetChild(i).GetComponent<Image>().color =
                GameObject.Find("Avatar").transform.GetChild(i).GetComponent<SpriteRenderer>().color;
        }

        GameObject.Find("HousePlayerName").GetComponent<Text>().text = CharacterEdit.characterName;

        //Preparando canvas de la aventura
        EnemyLoader.loadImageSlots();

        //Desactivando canvas
        houseCanvas.SetActive(false);
        forjaCanvas.SetActive(false);
        tiendaCanvas.SetActive(false);
        gradeCanvas.SetActive(false);
        adventureCanvas.SetActive(false);
        noDinero.SetActive(false);
        noSpace.SetActive(false);

        activated = false;

        //Llamando a la función para obtener calificaciones recientes y convertirlas a monedas
        StartCoroutine(CheckGrades.checkDBGrades());
    }

    public static void activateCanvas(int id)
    {
        activated = true;
        switch(id)
        {
            case 1: 
                houseCanvas.SetActive(true);
                GameObject.Find("HouseAttackAmount").GetComponent<Text>().text = Stats.attack.ToString();
                GameObject.Find("HouseDefenseAmount").GetComponent<Text>().text = Stats.defense.ToString();
                break;
            case 2:
                forjaCanvas.SetActive(true);
                playSound = false;
                forjaAttackMenu();
                playSound = true;
                break;
            case 3:
                tiendaCanvas.SetActive(true);
                break;
            case 4:
                rankingCanvas.SetActive(true);
                GameObject.Find("RankingManager").GetComponent<RankingManager>().loadRanking();
                break;
            case 5:
                adventureCanvas.SetActive(true);
                EnemyLoader.loadEnemies();
                GameObject.Find("CurrentMapLevel").GetComponent<Text>().text = Stats.mapLevel.ToString();
                GameObject.Find("CurrentBattle").GetComponent<Text>().text = "Batalla " + Stats.mapLevel.ToString();
                GameObject.Find("AventuraAtaqueText").GetComponent<Text>().text = Stats.attack.ToString();
                GameObject.Find("AventuraDefensaText").GetComponent<Text>().text = Stats.defense.ToString();
                openMapSound.Play();
                break;
        }
    }

    public static void deactivateCanvas()
    {
        activated = false;
        switch(EventSystem.current.currentSelectedGameObject.name)
        {
            case "HouseX": houseCanvas.SetActive(false); break;
            case "ForjaX": forjaCanvas.SetActive(false); break;
            case "TiendaX": tiendaCanvas.SetActive(false); break;
            case "InventoryX": inventoryCanvas.SetActive(false); break;
            case "AdventureX": adventureCanvas.SetActive(false); openMapSound.Play(); break;
            case "RankingX": rankingCanvas.SetActive(false); break;
            case "GradeX": gradeCanvas.SetActive(false); break;
        }
    }

    public static void openInventory()
    {
        inventoryCanvas.SetActive(true);
        InventoryMenu.swordInventory();
    }

    public static void openCharacterEditor()
    {
        if(GameObject.Find("Avatar").gameObject != null) Destroy(GameObject.Find("Avatar").gameObject);
        SceneManager.LoadScene(1, LoadSceneMode.Single); 
    }

    public static void forjaAttackMenu()
    {
        ataqueCanvas.SetActive(true);
        defensaCanvas.SetActive(false);
        if(playSound) menuSound.Play();

        GameObject.Find("AtaqueButton").GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
        GameObject.Find("DefensaButton").GetComponent<Image>().color = new Color(255, 255, 255);
    }

    public static void forjaDefenseMenu()
    {
        ataqueCanvas.SetActive(false);
        defensaCanvas.SetActive(true);
        menuSound.Play();

        GameObject.Find("DefensaButton").GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
        GameObject.Find("AtaqueButton").GetComponent<Image>().color = new Color(255, 255, 255);
    }

    public static IEnumerator notEnoughMoney()
    {
        noDinero.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        noDinero.SetActive(false);
    }

    public static IEnumerator notEnoughSpace()
    {
        noSpace.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        noSpace.SetActive(false);
    }
}
