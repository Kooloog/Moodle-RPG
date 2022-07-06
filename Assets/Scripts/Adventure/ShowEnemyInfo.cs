using UnityEngine;
using UnityEngine.EventSystems;

public class ShowEnemyInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject selectedEnemy = eventData.pointerCurrentRaycast.gameObject;

        switch (selectedEnemy.name.ToCharArray()[5])
        {
            case '1': EnemyLoader.enemyInfoScreens[0].SetActive(true); break;
            case '2': EnemyLoader.enemyInfoScreens[1].SetActive(true); break;
            case '3': EnemyLoader.enemyInfoScreens[2].SetActive(true); break;
            case '4': EnemyLoader.enemyInfoScreens[3].SetActive(true); break;
            case '5': EnemyLoader.enemyInfoScreens[4].SetActive(true); break;
        }

        //EnemyLoader.enemyInfoScreens[selectedEnemy.name.ToCharArray()[5] - 1].SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (GameObject g in EnemyLoader.enemyInfoScreens) g.SetActive(false);
    }
}