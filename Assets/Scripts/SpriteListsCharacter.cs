using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteListsCharacter : MonoBehaviour
{
    [SerializeField] public Sprite[] pelos;
    [SerializeField] public Sprite[] camisetasBasicas;
    [SerializeField] public Sprite[] pantalones;
    [SerializeField] public Sprite[] calzado;
    [SerializeField] public Sprite[] vello;
    [SerializeField] public Sprite[] gafas;
    [SerializeField] public Sprite[] otros;
    public static Sprite[] pelosFinal;
    public static Sprite[] camisetasBasicasFinal;
    public static Sprite[] pantalonesFinal;
    public static Sprite[] calzadoFinal;
    public static Sprite[] velloFinal;
    public static Sprite[] gafasFinal;
    public static Sprite[] otrosFinal;

    private void Start()
    {
        pelosFinal = pelos;
        camisetasBasicasFinal = camisetasBasicas;
        pantalonesFinal = pantalones;
        calzadoFinal = calzado;
        velloFinal = vello;
        gafasFinal = gafas;
        otrosFinal = otros;
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
