using UnityEngine;

public class ObjectLists : MonoBehaviour
{
    public Sword[] swords;
    public Shield[] shields;
    public Item[] items;

    public static Sword[] swordsFinal;
    public static Shield[] shieldsFinal;
    public static Item[] itemsFinal;

    // Start is called before the first frame update
    void Start()
    {
        swordsFinal = swords;
        shieldsFinal = shields;
        itemsFinal = items;
    }
}
