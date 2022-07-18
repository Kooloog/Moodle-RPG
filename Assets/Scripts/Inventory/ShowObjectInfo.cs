using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowObjectInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static GameObject enemyInfo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject selectedObject = eventData.pointerCurrentRaycast.gameObject;
        GameObject selectedObjectParent = selectedObject.transform.parent.parent.gameObject;

        switch(selectedObjectParent.name)
        {
            case "EnemigosList":
                EnemyLoader.enemyInfoScreens
                    [int.Parse(selectedObject.name.ToCharArray()[5].ToString())-1].SetActive(true);
                break;

            case "ObjetosInventory":
                InventoryMenu.inventoryInfoScreens
                    [int.Parse(selectedObject.name.ToCharArray()[7].ToString())].SetActive(true);
                break;

            case "Enemigos":
                if (!BattleManager.attacking)
                {
                    Enemy thisEnemy = EnemyLists.levelsFinal[Stats.mapLevel - 1].enemies
                        [int.Parse(selectedObject.name.ToCharArray()[5].ToString()) - 1];

                    //Cambiando el contenido del cuadro de información;
                    enemyInfo.transform.GetChild(1).gameObject.GetComponent<Text>().text =
                        thisEnemy.enemyName.ToString();
                    enemyInfo.transform.GetChild(2).gameObject.GetComponent<Text>().text =
                        "Ataque: " + thisEnemy.attack.ToString();
                    enemyInfo.transform.GetChild(3).gameObject.GetComponent<Text>().text =
                        "Defensa: " + thisEnemy.defense.ToString();
                    enemyInfo.transform.GetChild(4).gameObject.GetComponent<Text>().text =
                        "Ataque: " + thisEnemy.health.ToString();
                    enemyInfo.SetActive(true);
                }

                break;

            default:
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(SceneManager.GetActiveScene().name == "Mapa")
        {
            foreach (GameObject g in EnemyLoader.enemyInfoScreens) g.SetActive(false);
            foreach (GameObject g in InventoryMenu.inventoryInfoScreens) g.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "Battle")
        {
            enemyInfo.SetActive(false);
            foreach (GameObject g in InventoryMenu.inventoryInfoScreens) g.SetActive(false);
        }
    }

    void Start()
    {
        if (GameObject.Find("EnemyInfo"))
        {
            enemyInfo = GameObject.Find("EnemyInfo");
            enemyInfo.SetActive(false);
        }
    }
}
