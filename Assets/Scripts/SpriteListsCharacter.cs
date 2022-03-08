using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteListsCharacter : MonoBehaviour
{
    [SerializeField] public Sprite[] pelos;
    [SerializeField] public Sprite[] camisetasBasicas;
    [SerializeField] public Sprite[] pantalones;
    [SerializeField] public Sprite[] calzado;
    public static Sprite[] pelosFinal;
    public static Sprite[] camisetasBasicasFinal;
    public static Sprite[] pantalonesFinal;
    public static Sprite[] calzadoFinal;

    private void Start()
    {
        pelosFinal = pelos;
        camisetasBasicasFinal = camisetasBasicas;
        pantalonesFinal = pantalones;
        calzadoFinal = calzado;
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
