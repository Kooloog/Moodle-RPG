using UnityEngine;
using UnityEngine.EventSystems;

public class ShowObjectInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject selectedObject = eventData.pointerCurrentRaycast.gameObject;
        GameObject selectedObjectParent = selectedObject.transform.parent.parent.gameObject;
        Debug.Log(selectedObject.name);

        switch(selectedObjectParent.name)
        {
            case "EnemigosList":
                EnemyLoader.enemyInfoScreens
                    [int.Parse(selectedObject.name.ToCharArray()[5].ToString()) - 1].SetActive(true);
                break;

            case "ObjetosInventory":
                InventoryMenu.inventoryInfoScreens
                    [int.Parse(selectedObject.name.ToCharArray()[7].ToString())].SetActive(true);
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (GameObject g in EnemyLoader.enemyInfoScreens) g.SetActive(false);
        foreach (GameObject g in InventoryMenu.inventoryInfoScreens) g.SetActive(false);
    }
}
