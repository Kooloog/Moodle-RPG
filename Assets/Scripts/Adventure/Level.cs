using UnityEngine;

[System.Serializable] public class Level
{
    public Level (Level l)
    {
        levelNumber = l.levelNumber;
        enemies = l.enemies;
        expOnCompletion = l.expOnCompletion;
    }

    public int levelNumber;
    public Enemy[] enemies;
    public int expOnCompletion;
}
