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
        EnableCustomizationMenus.changeToMenuView();
        EnableCustomizationMenus.hideEverything();
        EnableCustomizationMenus.enableSingleMenu("BOTONES");
        GameObject.Find("MenuSelect").GetComponent<AudioSource>().Play();
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
            case "AvatarPantalon":
                EnableCustomizationMenus.enableSingleMenu("PANTALONES");
                foreach (Transform button in GameObject.Find("Pantalones").transform)
                {
                    button.GetChild(0).gameObject.GetComponent<Image>().color = selectedColor;
                }
                break;
            case "AvatarCalzado":
                EnableCustomizationMenus.enableSingleMenu("PANTALONES");
                foreach (Transform button in GameObject.Find("Calzado").transform)
                {
                    button.GetChild(0).gameObject.GetComponent<Image>().color = selectedColor;
                }
                break;
            case "AvatarGafas":
                EnableCustomizationMenus.enableSingleMenu("ACCESORIOS");
                foreach (Transform button in GameObject.Find("Gafas").transform)
                {
                    button.GetChild(0).gameObject.GetComponent<Image>().color = selectedColor;
                }
                break;
            case "AvatarCollar":
                EnableCustomizationMenus.enableSingleMenu("ACCESORIOS");
                foreach (Transform button in GameObject.Find("Collares").transform)
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
