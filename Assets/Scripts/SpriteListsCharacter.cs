using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteListsCharacter : MonoBehaviour
{
    [SerializeField] public Sprite[] pelos;
    public static Sprite[] pelosFinal;

    private void Start()
    {
        pelosFinal = pelos;
    }
}
