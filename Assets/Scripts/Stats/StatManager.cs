using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    private static Text txtVida;
    private static Text txtPuntos;
    private static Text txtMonedas;
    private static Slider sliderVida;

    private GameObject avatar;
    private string manageStatsURL = "http://localhost/moodle/unity/UIStatsManager.php";

    public void decreaseHealth(int amount)
    {
        Stats.health -= amount;
        if (Stats.health < 0) Stats.health = 0;

        txtVida.text = Stats.health + "/" + Stats.maxHealth;
        sliderVida.value = Stats.health;
        StartCoroutine(playerRedFlash());
        StartCoroutine(updateStats("health", Stats.health));
    }

    public void increaseHealth(int amount)
    {
        Stats.health += amount;
        if (Stats.health > Stats.maxHealth) Stats.health = Stats.maxHealth;

        txtVida.text = Stats.health + "/" + Stats.maxHealth;
        sliderVida.value = Stats.health;
        StartCoroutine(updateStats("health", Stats.health));
    }

    public void increaseMaxHealth(int amount)
    {
        Stats.maxHealth += amount;
        Stats.health = Stats.maxHealth;
        increaseHealth(Stats.maxHealth);

        sliderVida.maxValue = Stats.maxHealth;
        sliderVida.value = Stats.health;
        txtVida.text = Stats.health + "/" + Stats.maxHealth;
        StartCoroutine(updateStats("maxhealth", Stats.maxHealth));
    }

    public void increaseScore(int amount)
    {
        Stats.score += amount;

        txtPuntos.text = Stats.score.ToString();
        StartCoroutine(updateStats("score", Stats.score));
    }

    public void decreaseCoins(int amount)
    {
        Stats.coins -= amount;
        if (Stats.coins < 0) Stats.coins = 0;

        txtMonedas.text = Stats.coins.ToString();
        StartCoroutine(updateStats("coins", Stats.coins));
    }

    public void increaseCoins(int amount)
    {
        Stats.coins += amount;

        txtMonedas.text = Stats.coins.ToString();
        StartCoroutine(updateStats("coins", Stats.coins));
    }

    public void decreaseAttack(int amount)
    {
        Stats.attack -= amount;
        if (Stats.attack < 10) Stats.attack = 10;
        StartCoroutine(updateStats("attack", Stats.attack));
    }

    public void increaseAttack(int amount)
    {
        Stats.attack += amount;
        StartCoroutine(updateStats("attack", Stats.attack));
    }

    public void decreaseDefense(int amount)
    {
        Stats.defense -= amount;
        if (Stats.defense < 10) Stats.defense = 10;
        StartCoroutine(updateStats("defense", Stats.defense));
    }

    public void increaseDefense(int amount)
    {
        Stats.defense += amount;
        StartCoroutine(updateStats("defense", Stats.defense));
    }

    public void nextMapLevel()
    {
        Stats.mapLevel += 1;
        if (Stats.mapLevel > 17) Stats.mapLevel = 1;
        StartCoroutine(updateStats("maplevel", Stats.mapLevel));
    }

    public void updateNextUpgrade()
    {
        Stats.nextUpgrade = (Stats.attack + Stats.defense - 1) * 500;
    }

    IEnumerator updateStats(string stat, int value)
    {
        string postURL = manageStatsURL +
            "?change=" + stat +
            "&" + stat + "=" + value;

        UnityWebRequest statPost = UnityWebRequest.Post(postURL, "");
        yield return statPost.SendWebRequest();
    }

    IEnumerator playerRedFlash()
    {
        Color originalColour = avatar.GetComponent<SpriteRenderer>().material.color;
        
        for (int i = 0; i < 3; i++)
        {
            avatar.GetComponent<SpriteRenderer>().material.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            avatar.GetComponent<SpriteRenderer>().material.color = originalColour;
            yield return new WaitForSeconds(0.02f);
        }
        StopCoroutine("EnemyFlash");
    }

    // Start is called before the first frame update
    void Start()
    {
        txtVida = GameObject.Find("TextVida").GetComponent<Text>();
        txtPuntos = GameObject.Find("TextPuntos").GetComponent<Text>();
        txtMonedas = GameObject.Find("TextMonedas").GetComponent<Text>();
        sliderVida = GameObject.Find("SliderVida").GetComponent<Slider>();
        avatar = GameObject.Find("Avatar");
    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) increaseHealth(1);
        if (Input.GetKeyDown(KeyCode.T)) decreaseHealth(1);
    }
    */
}
