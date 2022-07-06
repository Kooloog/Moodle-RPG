using UnityEngine;

[System.Serializable] public class Enemy
{
    public Enemy(Enemy e)
    {
        enemyName = e.enemyName;
        attack = e.attack;
        defense = e.defense;
        health = e.health;
        sprite = e.sprite;
    }

    public string enemyName;
    public int attack;
    public int defense;
    public int health;
    public Sprite sprite;
}
