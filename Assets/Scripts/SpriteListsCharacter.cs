using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteListsCharacter : MonoBehaviour
{
    [SerializeField] public Sprite[] pelos;
    [SerializeField] public Sprite[] camisetasBasicas;
    [SerializeField] public Sprite[] pantalones;
    public static Sprite[] pelosFinal;
    public static Sprite[] camisetasBasicasFinal;
    public static Sprite[] pantalonesFinal;

    private void Start()
    {
        pelosFinal = pelos;
        camisetasBasicasFinal = camisetasBasicas;
        pantalonesFinal = pantalones;
    }

    private void Update()
    {
        //Flip hair if Z key is pressed.
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (GameObject.Find("AvatarPelo").gameObject.GetComponent<SpriteRenderer>().flipX)
            {
                GameObject.Find("AvatarPelo").gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GameObject.Find("AvatarPelo").gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
