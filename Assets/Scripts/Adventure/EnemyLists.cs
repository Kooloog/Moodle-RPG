using UnityEngine;

public class EnemyLists : MonoBehaviour
{
    public Enemy[] enemies;
    public Level[] levels;

    public static Enemy[] enemiesFinal;
    public static Level[] levelsFinal;

    // Start is called before the first frame update
    void Start()
    {
        enemiesFinal = enemies;
        levelsFinal = levels;
    }
}
