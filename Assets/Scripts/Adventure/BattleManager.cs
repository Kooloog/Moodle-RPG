using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private int currentLevel;

    public GameObject objectMenu;
    public GameObject pickedObjects;
    public GameObject objectWarning1;
    public GameObject objectWarning2;
    public GameObject screenFlash;
    public AudioSource enemyHit;

    public static GameObject objectMenuFinal;
    public static GameObject pickedObjectsFinal;
    public static GameObject objectWarning1Final;
    public static GameObject objectWarning2Final;

    public static Vector2 defaultMenuPosition;
    public static Vector2 pickedObjectsPosition;

    public static bool attacking;
    public static bool firstMenuOpen;
    public static bool showingWarning;

    private List<Enemy> levelEnemies;
    private List<Transform> levelEnemiesTF;
    private List<Vector2> levelEnemiesPosition;
    private GameObject avatar;
    private Vector2 avatarPosition;

    private StatManager statManager;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("Avatar")) SceneManager.LoadScene(1);

        //Cargando jugador
        GameObject placeholder = GameObject.Find("AvatarPlaceholder");
        avatar = GameObject.Find("Avatar");

        avatar.transform.position = placeholder.transform.position;
        avatar.transform.localScale = new Vector2(
            avatar.transform.localScale.x * 2, avatar.transform.localScale.y * 2);
        avatarPosition = avatar.transform.position;
        placeholder.SetActive(false);

        //Cargando enemigos
        levelEnemies = new List<Enemy>();
        levelEnemiesTF = new List<Transform>();
        levelEnemiesPosition = new List<Vector2>();

        currentLevel = Stats.mapLevel;
        int amount = EnemyLists.levelsFinal[currentLevel - 1].enemies.Length;
        int cont = 1;

        foreach(Transform amountValue in GameObject.Find("Enemigos").transform)
        {
            if(cont == amount)
            {
                int contEnemy = 0;
                foreach(Transform enemy in amountValue)
                {
                    enemy.gameObject.GetComponent<Image>().sprite =
                        EnemyLists.levelsFinal[currentLevel - 1].enemies[contEnemy].sprite;

                    levelEnemies.Add(EnemyLists.levelsFinal[currentLevel - 1].enemies[contEnemy]);
                    levelEnemiesTF.Add(enemy);
                    levelEnemiesPosition.Add(enemy.position);
                    ++contEnemy;
                } 
            }
            else
            {
                amountValue.gameObject.SetActive(false);
            }

            cont++;
        }

        //Guardando el menú de selección de objetos en una variable estática, y mostrándolo.
        objectMenuFinal = objectMenu;
        defaultMenuPosition = objectMenuFinal.transform.position;
        StartCoroutine(showObjectMenu());

        //Guardando la posición del menú de objetos elegidos, y escondiéndolo.
        pickedObjectsFinal = pickedObjects;
        pickedObjectsPosition = pickedObjectsFinal.transform.position;
        pickedObjects.transform.position = new Vector2(pickedObjectsPosition.x, pickedObjectsPosition.y + 4.5f);

        //Guardando mensajes de warning y ocultándolos.
        objectWarning1Final = objectWarning1;
        objectWarning2Final = objectWarning2;
        objectWarning1Final.SetActive(false);
        objectWarning2Final.SetActive(false);

        //Guardando instancia de StatManager y efectos de sonido
        statManager = GameObject.Find("StatsManager").GetComponent<StatManager>();
        enemyHit = GameObject.Find("EnemyHit").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            SceneManager.LoadScene(1);
        }
    }

    public void battleMove()
    {
        TurnObjects.callFillObjects();
        hideWarnings();
        StartCoroutine(hideObjectMenu());
        StartCoroutine(showPickedObjects());

        StartCoroutine(turn());
    }

    public IEnumerator turn()
    {
        attacking = true;

        yield return new WaitForSeconds(1.0f);
        Object objectToUse = null;
        int defenseValue = Stats.defense;

        //Bucle que representa ambos posibles movimientos del jugador
        for(int move = 1; move <= 2; move++)
        {
            if (move == 1) objectToUse = TurnObjects.firstObjectTurn;
            else if (TurnObjects.secondObjectTurn != null) objectToUse = TurnObjects.secondObjectTurn;

            if (objectToUse is Sword)
            {
                //Movimiento: Ataque con espada
                Sword sword = (Sword)objectToUse;


                
            }
            else if (objectToUse is Shield)
            {
                //Movimiento: Defensa con escudo
                Shield shield = (Shield)objectToUse;

                //Aumentando la defensa dada por el escudo
                Stats.defense += shield.defense;
            }
            else if (objectToUse is Item)
            {
                //Movimiento: Uso de objeto
                Item item = (Item)objectToUse;
            }
            else
            {
                //No hay movimiento. Saltar turno
            }

            //Turno de ataque enemigo
            if(move == 1)
            {
                for(int i=0; i<levelEnemies.Count; i++)
                {
                    float timeToMove = 0.20f;
                    float progress = 0f;

                    //Ida
                    while (progress < 1)
                    {
                        progress += Time.deltaTime / timeToMove;
                        levelEnemiesTF[i].position = Vector2.Lerp(
                            levelEnemiesPosition[i], avatar.transform.position, progress);
                        yield return null;
                    }

                    yield return new WaitForSeconds(0.15f);

                    //Cálculo del daño hecho, luego se resta
                    StartCoroutine(flashScreen());
                    enemyHit.Play();
                    statManager.decreaseHealth(Mathf.Max((levelEnemies[i].attack - Stats.defense), 0));

                    yield return new WaitForSeconds(0.20f);
                    progress = 0f;

                    //Vuelta
                    while (progress < 1)
                    {
                        progress += Time.deltaTime / timeToMove;
                        levelEnemiesTF[i].position = Vector2.Lerp(
                            avatar.transform.position, levelEnemiesPosition[i], progress);
                        yield return null;
                    }

                    Stats.defense = defenseValue;
                }
            }
        }

        yield return new WaitForSeconds(1.0f);
        attacking = false;
        StartCoroutine(hidePickedObjects());
        StartCoroutine(showObjectMenu());
    }

    public static IEnumerator showObjectMenu()
    {
        objectMenuFinal.transform.position = new Vector2(defaultMenuPosition.x + 10.5f, defaultMenuPosition.y);
        Vector2 initialPosition = objectMenuFinal.transform.position;

        if (!firstMenuOpen)
        {
            yield return new WaitForSeconds(2f);
            firstMenuOpen = true;
        }
        float timeToMove = 0.5f;
        float progress = 0f;
        objectMenuFinal.SetActive(true);

        while (progress < 1)
        {
            progress += Time.deltaTime / timeToMove;
            objectMenuFinal.transform.position =
                Vector2.Lerp(initialPosition, defaultMenuPosition, progress);
            yield return null;
        }
    }

    public static IEnumerator hideObjectMenu()
    {
        Vector2 move = new Vector2(defaultMenuPosition.x + 10.5f , defaultMenuPosition.y);

        float timeToMove = 0.5f;
        float progress = 0f;
        while (progress < 1)
        {
            progress += Time.deltaTime / timeToMove;
            objectMenuFinal.transform.position = Vector2.Lerp(defaultMenuPosition, move, progress);
            yield return null;
        }

        objectMenuFinal.SetActive(false);
    }

    public static IEnumerator showPickedObjects()
    {
        Vector2 currentPosition = pickedObjectsFinal.transform.position;

        float timeToMove = 0.3f;
        float progress = 0f;
        while (progress < 1)
        {
            progress += Time.deltaTime / timeToMove;
            pickedObjectsFinal.transform.position = Vector2.Lerp(currentPosition, pickedObjectsPosition, progress);
            yield return null;
        }
    }

    public static IEnumerator hidePickedObjects()
    {
        Vector2 move = new Vector2(pickedObjectsPosition.x, pickedObjectsPosition.y + 4.5f);

        float timeToMove = 0.3f;
        float progress = 0f;
        while (progress < 1)
        {
            progress += Time.deltaTime / timeToMove;
            pickedObjectsFinal.transform.position = Vector2.Lerp(pickedObjectsPosition, move, progress);
            yield return null;
        }
    }

    public static IEnumerator showWarning(int id)
    {
        if (!showingWarning)
        {
            showingWarning = true;
            if(id == 1) objectWarning1Final.SetActive(true);
            else objectWarning2Final.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            objectWarning1Final.SetActive(false);
            showingWarning = false;
        }
    }

    public IEnumerator flashScreen()
    {
        screenFlash.SetActive(true);

        float flashTime = 0.15f;
        float progress = 0f;
        while (progress < 1)
        {
            progress += Time.deltaTime / flashTime;
            screenFlash.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1 - progress);
            yield return null;
        }

        screenFlash.SetActive(false);
        screenFlash.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    public void showObjectMenuMethod()
    {
        StartCoroutine(showObjectMenu());
    }

    public void hideObjectMenuMethod()
    {
        StartCoroutine(hideObjectMenu());
    }

    public void hideWarnings()
    {
        objectWarning1Final.SetActive(false);
        objectWarning2Final.SetActive(false);
    }
}
