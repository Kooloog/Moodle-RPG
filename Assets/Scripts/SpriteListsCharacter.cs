using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteListsCharacter : MonoBehaviour
{
    [SerializeField] public Sprite[] pelos;
    [SerializeField] public Sprite[] camisetasBasicas;
    public static Sprite[] pelosFinal;
    public static Sprite[] camisetasBasicasFinal;

    private void Start()
    {
        pelosFinal = pelos;
        camisetasBasicasFinal = camisetasBasicas;
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
