using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private int currentLevel;

    public static bool attacking;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            SceneManager.LoadScene(1);
        }
    }
}
