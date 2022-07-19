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

    public static GameObject objectMenuFinal;
    public static GameObject pickedObjectsFinal;
    public static GameObject objectWarning1Final;
    public static GameObject objectWarning2Final;

    public static Vector2 defaultMenuPosition;
    public static Vector2 pickedObjectsPosition;

    public static bool attacking;
    public static bool firstMenuOpen;
    public static bool showingWarning;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("Avatar")) SceneManager.LoadScene(1);

        //Cargando jugador
        GameObject placeholder = GameObject.Find("AvatarPlaceholder");
        GameObject avatar = GameObject.Find("Avatar");

        avatar.transform.position = placeholder.transform.position;
        avatar.transform.localScale = new Vector2(
            avatar.transform.localScale.x * 2, avatar.transform.localScale.y * 2);
        placeholder.SetActive(false);

        //Cargando enemigos
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
        StartCoroutine(hideObjectMenu());
        StartCoroutine(showPickedObjects());
    }

    public static void playerAttack()
    {

    }

    public static void enemyAttack()
    {

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
