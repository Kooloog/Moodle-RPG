using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetEnemy : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static bool enemyTargeted;
    public static Enemy targetEnemy;
    public static GameObject targetEnemyObject;

    public static GameObject mouseOver;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject selectedObject = eventData.pointerCurrentRaycast.gameObject;
        int enemyNumber = int.Parse(selectedObject.name.ToCharArray()[5].ToString()) - 1;

        if (BattleManager.canClickEnemy && !enemyTargeted)
        {
            selectedObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            targetEnemy = EnemyLists.levelsFinal[Stats.mapLevel - 1].enemies[enemyNumber];
            targetEnemyObject = selectedObject;
            enemyTargeted = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (BattleManager.canClickEnemy && !enemyTargeted)
        {
            mouseOver = eventData.pointerCurrentRaycast.gameObject;
            mouseOver.GetComponent<Image>().color = new Color(0f, 1f, 0f, 1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (BattleManager.canClickEnemy && !enemyTargeted)
        {
            mouseOver.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public static void untargetEnemy()
    {
        enemyTargeted = false;
        targetEnemy = null;
        targetEnemyObject = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyTargeted = false;
    }
}
