using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GetCustomColour : MonoBehaviour, IPointerDownHandler
{
    private static Color selectedColor;

    public static string attributeToChange;
    public static GameObject previousEventSystemStatic;
    public GameObject previousEventSystem;

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 localCursor;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle
            (GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localCursor))
            return;

        selectedColor = GetComponent<Image>().sprite.texture.GetPixel((int)localCursor.x * 12, (int)localCursor.y * 12);
        GameObject.Find(attributeToChange).gameObject.GetComponent<SpriteRenderer>().color = selectedColor;
        CharacterEdit.customColour(ColorUtility.ToHtmlStringRGB(selectedColor));
    }

    public void returnToMenu()
    {
        EnableCustomizationMenus.hideEverything();
        EnableCustomizationMenus.enableSingleMenu("BOTONES");
        previousEventSystemStatic.SetActive(true);

        switch (attributeToChange) {
            case "AvatarPelo":
                EnableCustomizationMenus.enableSingleMenu("PELO");
                foreach (Transform button in GameObject.Find("Peinado").transform)
                {
                    button.GetChild(0).gameObject.GetComponent<Image>().color = selectedColor;
                }
                break;

            case "AvatarCamiseta":
                EnableCustomizationMenus.enableSingleMenu("CAMISETAS");
                foreach (Transform button in GameObject.Find("CamisetaBasica").transform)
                {
                    button.GetChild(0).gameObject.GetComponent<Image>().color = selectedColor;
                }
                break;
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
