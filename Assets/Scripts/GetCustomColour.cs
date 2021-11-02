using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GetCustomColour : MonoBehaviour, IPointerDownHandler
{
    private static Color selectedColor;

    public static GameObject previousEventSystemStatic;
    public GameObject previousEventSystem;

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 localCursor;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle
            (GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localCursor))
            return;

        selectedColor = GetComponent<Image>().sprite.texture.GetPixel((int)localCursor.x * 12, (int)localCursor.y * 12);
        GameObject.Find("AvatarPelo").gameObject.GetComponent<SpriteRenderer>().color = selectedColor;
        CharacterEdit.customHairColour(ColorUtility.ToHtmlStringRGB(selectedColor));
    }

    public void returnToHairMenu()
    {
        EnableCustomizationMenus.hideEverything();
        EnableCustomizationMenus.enableSingleMenu("PELO");
        EnableCustomizationMenus.enableSingleMenu("BOTONES");
        previousEventSystemStatic.SetActive(true);

        foreach (Transform button in GameObject.Find("Peinado").transform)
        {
            button.GetChild(0).gameObject.GetComponent<Image>().color = selectedColor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        previousEventSystemStatic = previousEventSystem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
