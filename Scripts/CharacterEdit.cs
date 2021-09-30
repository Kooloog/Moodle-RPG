using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterEdit : MonoBehaviour
{
    private GameObject avatar;

    //Cambia, uno por uno, los diferentes rasgos del personaje.
    public void changeAttribute()
    {
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
        GameObject selectedItem = selectedButton.transform.GetChild(0).gameObject;
        GameObject selectedCategory = selectedButton.transform.parent.gameObject;

        switch(selectedCategory.name)
        {
            //Cambio del tono de piel del personaje
            case "TonoPiel":
                avatar.GetComponent<SpriteRenderer>().color = selectedItem.GetComponent<Image>().color;
                break;

            //Cambio del color de ojos del personaje
            case "ColorOjos":
                GameObject ojosAvatar = avatar.transform.Find("AvatarOjos").gameObject;
                ojosAvatar.GetComponent<SpriteRenderer>().color = selectedItem.GetComponent<Image>().color;
                break;

            //Cambio del color del pelo
            case "ColorPelo":
                Debug.Log("Coming soon...");
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        avatar = GameObject.Find("Avatar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
