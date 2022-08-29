using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private int currentLevel;

    public GameObject objectMenu;
    public GameObject pickedObjects;
    public GameObject objectWarning1;
    public GameObject objectWarning2;
    public GameObject villaWarning;
    public GameObject avatarDamage;
    public GameObject enemyDamage;
    public GameObject screenFlash;
    public GameObject eligeEnemigo;
    public GameObject mostrarMenu;
    public GameObject deathEffect;
    public GameObject itemUse;
    public GameObject levelUpScreen;
    public GameObject victorySprite;
    public GameObject defeatSprite;
    public GameObject victoryInfo;

    public AudioSource backgroundMusic;
    public AudioSource enemyHit;
    public AudioSource sonidoMuerte;
    public AudioSource victorySound;
    public AudioSource defeatSound;
    public AudioSource itemUsedSound;
    public AudioSource levelUpSound;

    public static GameObject objectMenuFinal;
    public static GameObject pickedObjectsFinal;
    public static GameObject objectWarning1Final;
    public static GameObject objectWarning2Final;
    public static GameObject villaWarningFinal;

    public static Vector2 defaultMenuPosition;
    public static Vector2 pickedObjectsPosition;
    public static float scoreMultiplier;
    public static int enemiesDefeated;

    //Estadisticas
    public static int attackAtStart;
    public static int defenseAtStart;
    public static int maxHealthAtStart;

    //Flags
    public static bool attacking;
    public static bool canClickEnemy;
    public static bool firstMenuOpen;
    public static bool showingWarning;
    public static bool battleWon;

    private List<Enemy> levelEnemies;
    private List<Transform> levelEnemiesTF;
    private List<Vector2> levelEnemiesPosition;
    private GameObject avatar;
    private Vector2 avatarPosition;

    private StatManager statManager;
    private DeathManager deathManager;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("Avatar")) SceneManager.LoadScene(2);
        GameObject.Find("NumBatalla").GetComponent<Text>().text = "Batalla " + Stats.mapLevel.ToString();

        //Cargando jugador
        GameObject placeholder = GameObject.Find("AvatarPlaceholder");
        avatar = GameObject.Find("Avatar");

        if (avatar.transform.localScale.x < 5)
        {
            avatar.transform.position = placeholder.transform.position;
            avatar.transform.localScale = new Vector2(
                avatar.transform.localScale.x * 2, avatar.transform.localScale.y * 2);
        }

        avatarPosition = avatar.transform.position;
        placeholder.SetActive(false);

        //Cargando enemigos
        levelEnemies = new List<Enemy>();
        levelEnemiesTF = new List<Transform>();
        levelEnemiesPosition = new List<Vector2>();
        enemiesDefeated = 0;

        currentLevel = Stats.mapLevel;
        int amount = EnemyLists.levelsFinal[currentLevel - 1].enemies.Length;
        int cont = 1;

        foreach(Transform amountValue in GameObject.Find("Enemigos").transform)
        {
            if(cont == amount)
            {
                int contEnemy = 0;
                foreach(Transform enemy in amountValue)
                {
                    enemy.gameObject.GetComponent<Image>().sprite =
                        EnemyLists.levelsFinal[currentLevel - 1].enemies[contEnemy].sprite;

                    levelEnemies.Add(EnemyLists.levelsFinal[currentLevel - 1].enemies[contEnemy]);
                    levelEnemiesTF.Add(enemy);
                    levelEnemiesPosition.Add(enemy.position);
                    ++contEnemy;
                } 
            }
            else
            {
                amountValue.gameObject.SetActive(false);
            }

            cont++;
            if (scoreMultiplier < 1) scoreMultiplier = 1.0f;
        }

        //Guardando el menú de selección de objetos en una variable estática, y mostrándolo.
        objectMenuFinal = objectMenu;
        defaultMenuPosition = objectMenuFinal.transform.position;
        StartCoroutine(showObjectMenu());

        //Guardando la posición del menú de objetos elegidos, y escondiéndolo.
        pickedObjectsFinal = pickedObjects;
        pickedObjectsPosition = pickedObjectsFinal.transform.position;
        pickedObjects.transform.position = new Vector2(pickedObjectsPosition.x, pickedObjectsPosition.y + 4.5f);

        //Guardando estadísticas
        attackAtStart = Stats.attack;
        defenseAtStart = Stats.defense;
        maxHealthAtStart = Stats.maxHealth;

        //Guardando mensajes de warning y ocultándolos.
        objectWarning1Final = objectWarning1;
        objectWarning2Final = objectWarning2;
        villaWarningFinal = villaWarning;
        objectWarning1Final.SetActive(false);
        objectWarning2Final.SetActive(false);
        villaWarningFinal.SetActive(false);

        //Guardando instancia de StatManager
        statManager = GameObject.Find("StatsManager").GetComponent<StatManager>();
        deathManager = GameObject.Find("DeathManager").GetComponent<DeathManager>();

        TurnObjects.clearAllItems();
    }

    public void battleMove()
    {
        TurnObjects.callFillObjects();
        TurnObjects.changeOutline(1, false);
        TurnObjects.changeOutline(2, false);
        hideWarnings();
        StartCoroutine(hideObjectMenu());
        StartCoroutine(showPickedObjects());

        StartCoroutine(turn());
    }

    public IEnumerator turn()
    {
        attacking = true;
        mostrarMenu.SetActive(false);

        int enemyAmount = EnemyLists.levelsFinal[currentLevel - 1].enemies.Length;
        yield return new WaitForSeconds(1.0f);
        Object objectToUse = null;
        int defenseValue = Stats.defense;

        //Bucle que representa ambos posibles movimientos del jugador
        for(int move = 1; move <= 2; move++)
        {
            //No se avanza al siguiente turno si el jugador ha perdido toda la vida
            if (Stats.health <= 0) break;

            //No se avanza al siguiente turno si todos los enemigos han perdido la vida
            if (enemiesDefeated >= enemyAmount) break;

            //Se remarca en la UI el objeto que va a utilizar el jugador
            if (move == 1)
            {
                objectToUse = TurnObjects.firstObjectTurn;
                TurnObjects.reduceUses(objectToUse, 1);
                TurnObjects.changeOutline(1, true);
            }
            else if (TurnObjects.secondObjectTurn != null)
            {
                objectToUse = TurnObjects.secondObjectTurn;
                TurnObjects.reduceUses(objectToUse, 2);
                TurnObjects.changeOutline(2, true);
            }
            else
            {
                Debug.Log("No object");
                objectToUse = null;
            }

            //Movimiento en función del objeto utilizado
            if (objectToUse is Sword)
            {
                //Movimiento: Ataque con espada
                Sword sword = (Sword)objectToUse;

                //Se activan los flags necesarios para permitir que el jugador pueda elegir a qué
                //enemigo atacar haciéndole click.
                eligeEnemigo.SetActive(true);
                canClickEnemy = true;
                while(!TargetEnemy.enemyTargeted) { yield return null; }
                canClickEnemy = false;
                eligeEnemigo.SetActive(false);

                //Se guardan los datos del enemigo al que se le va a hacer el ataque
                Enemy target = TargetEnemy.targetEnemy;
                GameObject targetObject = TargetEnemy.targetEnemyObject;
                Vector2 targetObjectPosition = targetObject.transform.position;
                Vector2 movement = new Vector2
                    (targetObjectPosition.x - 1.5f, targetObjectPosition.y + 0.66f);

                float timeToMove = 0.20f;
                float progress = 0f;

                //Animación de ida: el jugador se mueve hacia el enemigo que va a atacar
                while (progress < 1)
                {
                    progress += Time.deltaTime / timeToMove;
                    avatar.transform.position = Vector2.Lerp(avatarPosition, movement, progress);
                    yield return null;
                }

                yield return new WaitForSeconds(0.15f);

                //Cálculo del daño hecho, luego se resta
                StartCoroutine(flashScreen());
                enemyHit.Play();

                int damageDealt = Stats.attack + sword.attack - target.defense;
                target.health -= damageDealt;
                statManager.increaseScore((int)(damageDealt * 5 * scoreMultiplier));
                StartCoroutine(showDamage("enemigo", damageDealt));

                yield return new WaitForSeconds(0.20f);
                progress = 0f;

                //Animación de vuelta: el jugador vuelve a su posición original
                while (progress < 1)
                {
                    progress += Time.deltaTime / timeToMove;
                    avatar.transform.position = Vector2.Lerp(movement, avatarPosition, progress);
                    yield return null;
                }

                //Se comprueba si el enemigo que ha recibido el ataque ha perdido la vida o no y, en tal
                //caso, se elimina del terreno de combate.
                if(target.health <= 0)
                {
                    Instantiate(deathEffect, targetObjectPosition, Quaternion.identity);
                    sonidoMuerte.Play();

                    yield return new WaitForSeconds(0.5f);
                    targetObject.SetActive(false);
                    TargetEnemy.untargetEnemy();

                    //Si se han derrotado a todos los enemigos, la batalla se gana.
                    enemiesDefeated++;
                    if(enemiesDefeated >= enemyAmount)
                    {
                        battleWon = true;
                        StartCoroutine(battleVictory());
                    }

                    break;
                }
            }
            else if (objectToUse is Shield)
            {
                //Movimiento: Defensa con escudo
                Shield shield = (Shield)objectToUse;

                //Aumentando la defensa dada por el escudo
                Stats.defense += shield.defense;
            }
            else if (objectToUse is Item)
            {
                //Movimiento: Uso de objeto
                Item item = (Item)objectToUse;

                switch(item.itemName)
                {
                    case "Pocion de Salud": statManager.increaseHealth(5); break;
                    case "Pocion de Ataque": Stats.attack += 2; break;
                    case "Pocion de Defensa": Stats.defense += 2; break;
                }

                StartCoroutine(showItemDescription(item.itemName));
                yield return new WaitForSeconds(2.0f);
            }
            else
            {
                //No hay movimiento. Saltar turno
            }

            //Turno de ataque enemigo
            if(move == 1)
            {
                for(int i=0; i<levelEnemies.Count; i++)
                {
                    float timeToMove = 0.20f;
                    float progress = 0f;

                    if (levelEnemiesTF[i].gameObject.activeSelf)
                    {
                        //Animación de ida: el enemigo se mueve hacia el jugador
                        while (progress < 1)
                        {
                            progress += Time.deltaTime / timeToMove;
                            levelEnemiesTF[i].position = Vector2.Lerp(
                                levelEnemiesPosition[i], avatar.transform.position, progress);
                            yield return null;
                        }

                        yield return new WaitForSeconds(0.15f);

                        //Cálculo del daño hecho, luego se resta
                        StartCoroutine(flashScreen());
                        enemyHit.Play();

                        int damageDealt = Mathf.Max((levelEnemies[i].attack - Stats.defense), 0);
                        statManager.decreaseHealth(damageDealt);
                        StartCoroutine(showDamage("avatar", damageDealt));

                        yield return new WaitForSeconds(0.20f);
                        progress = 0f;

                        //Animación de vuelta: el jugador vuelve a su posición original
                        while (progress < 1)
                        {
                            progress += Time.deltaTime / timeToMove;
                            levelEnemiesTF[i].position = Vector2.Lerp(
                                avatar.transform.position, levelEnemiesPosition[i], progress);
                            yield return null;
                        }

                        //Se comprueba si el jugador ha perdido toda la vida y, en tal caso, se elimina
                        //del terreno de combate y este pierde.
                        if (Stats.health <= 0)
                        {
                            deathManager.killPlayer();
                            Instantiate(deathEffect, avatarPosition, Quaternion.identity);
                            sonidoMuerte.Play();

                            yield return new WaitForSeconds(0.5f);
                            avatar.SetActive(false);

                            StartCoroutine(battleDefeat());

                            break;
                        }
                    }
                }
            }

            //Se vuelve la defensa a su valor original, y el enemigo objetivo deja de serlo.
            Stats.defense = defenseValue;
            TurnObjects.changeOutline(1, false);
            TurnObjects.changeOutline(2, false);
            TargetEnemy.untargetEnemy();
        }

        //Pausa antes de volver a elegir objeto.
        yield return new WaitForSeconds(1.0f);
        attacking = false;
        StartCoroutine(hidePickedObjects());

        if (Stats.health > 0 && enemiesDefeated < enemyAmount)
        {
            InventoryMenu.swordInventory();
            StartCoroutine(showObjectMenu());
            TurnObjects.playSound = false;
            TurnObjects.clearAllItems();
        }

        if(!battleWon) mostrarMenu.SetActive(true);
    }

    //Método que muestra el menú de objetos a elegir
    public static IEnumerator showObjectMenu()
    {
        objectMenuFinal.transform.position = new Vector2(defaultMenuPosition.x + 10.5f, defaultMenuPosition.y);
        Vector2 initialPosition = objectMenuFinal.transform.position;

        if (!firstMenuOpen)
        {
            yield return new WaitForSeconds(2f);
            firstMenuOpen = true;
        }
        float timeToMove = 0.5f;
        float progress = 0f;
        objectMenuFinal.SetActive(true);

        while (progress < 1)
        {
            progress += Time.deltaTime / timeToMove;
            objectMenuFinal.transform.position =
                Vector2.Lerp(initialPosition, defaultMenuPosition, progress);
            yield return null;
        }
    }

    //Método que esconde el menú de objetos a elegir
    public static IEnumerator hideObjectMenu()
    {
        Vector2 move = new Vector2(defaultMenuPosition.x + 10.5f , defaultMenuPosition.y);

        float timeToMove = 0.5f;
        float progress = 0f;
        while (progress < 1)
        {
            progress += Time.deltaTime / timeToMove;
            objectMenuFinal.transform.position = Vector2.Lerp(defaultMenuPosition, move, progress);
            yield return null;
        }

        objectMenuFinal.SetActive(false);
    }

    //Método que muestra los dos objetos elegidos.
    public static IEnumerator showPickedObjects()
    {
        Vector2 currentPosition = pickedObjectsFinal.transform.position;

        float timeToMove = 0.3f;
        float progress = 0f;
        while (progress < 1)
        {
            progress += Time.deltaTime / timeToMove;
            pickedObjectsFinal.transform.position = Vector2.Lerp(currentPosition, pickedObjectsPosition, progress);
            yield return null;
        }
    }

    //Método que esconde los dos objetos elegidos.
    public static IEnumerator hidePickedObjects()
    {
        Vector2 move = new Vector2(pickedObjectsPosition.x, pickedObjectsPosition.y + 4.5f);

        float timeToMove = 0.3f;
        float progress = 0f;
        while (progress < 1)
        {
            progress += Time.deltaTime / timeToMove;
            pickedObjectsFinal.transform.position = Vector2.Lerp(pickedObjectsPosition, move, progress);
            yield return null;
        }
    }

    //Método que muestra uno de dos avisos según la ID:
    //1 - No se ha elegido ningún objeto, no se puede seguir el combate.
    //2 - Sólo se ha elegido un objeto, se puede seguir el combate.
    public static IEnumerator showWarning(int id)
    {
        if (!showingWarning)
        {
            showingWarning = true;
            if(id == 1) objectWarning1Final.SetActive(true);
            else objectWarning2Final.SetActive(true);

            yield return new WaitForSeconds(2.0f);
            objectWarning1Final.SetActive(false);
            showingWarning = false;
        }
    }

    //Se muestra el daño que ha recibido o bien el jugador, o bien alguno de los enemigos
    public IEnumerator showDamage(string type, int amount)
    {
        GameObject show = null;
        if (type == "avatar") show = avatarDamage;
        else if (type == "enemigo") show = enemyDamage;

        show.transform.GetChild(0).gameObject.GetComponent<Text>().text = amount.ToString();
        show.SetActive(true);

        yield return new WaitForSeconds(1f);
        float fadeTime = 1f;
        float progress = 0f;
        while (progress < 1)
        {
            progress += Time.deltaTime / fadeTime;
            Color transparency = new Color(1f, 1f, 1f, 1 - progress);
            show.transform.GetChild(0).gameObject.GetComponent<Text>().color = transparency;
            show.GetComponent<Image>().color = transparency;
            yield return null;
        }

        show.SetActive(false);
        show.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        show.transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
    }

    //Flash blanco corto en la pantalla cuando se da o se recibe un ataque.
    public IEnumerator flashScreen()
    {
        screenFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        float flashTime = 0.2f;
        float progress = 0f;
        while (progress < 1)
        {
            progress += Time.deltaTime / flashTime;
            screenFlash.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1 - progress);
            yield return null;
        }

        screenFlash.SetActive(false);
        screenFlash.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    //Método para mostrar por pantalla el efecto de un objeto cuando se utiliza
    public IEnumerator showItemDescription(string name)
    {
        itemUse.SetActive(true);
        itemUsedSound.Play();
        Text itemUseText = itemUse.GetComponent<Text>();
        
        switch(name)
        {
            case "Pocion de Salud": itemUseText.text = "+5 vida"; break;
            case "Pocion de Ataque": itemUseText.text = "+2 ataque"; break;
            case "Pocion de Defensa": itemUseText.text = "+2 defensa"; break;
        }

        yield return new WaitForSeconds(2.0f);
        itemUse.SetActive(false);
    }

    //Método que se utiliza cuando el jugador derrota a todos los enemigos y gana
    public IEnumerator battleVictory()
    {
        Vector2 normalScale = victorySprite.transform.localScale;
        victorySprite.transform.localScale = new Vector2(0f, 0f);
        victorySprite.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        backgroundMusic.Stop();
        victorySound.Play();
        revertStats();

        float scaleTime = 0.33f;
        float progress = 0f;
        while (progress < 1)
        {
            progress += Time.deltaTime / scaleTime;
            victorySprite.transform.localScale = Vector2.Lerp(new Vector2(0f, 0f), normalScale, progress);
            yield return null;
        }

        scoreMultiplier += 0.1f;
        statManager.increaseScore(EnemyLists.levelsFinal[currentLevel - 1].expOnCompletion);
        statManager.nextMapLevel();
        yield return new WaitForSeconds(3.5f);
        victorySprite.SetActive(false);

        if(Stats.score >= Stats.nextUpgrade)
        {
            levelUpSound.Play();
            levelUpScreen.SetActive(true);
            if (!(Stats.nextUpgrade % 1000 == 0)) GameObject.Find("MaxVidaSubir").SetActive(false);
            else statManager.increaseMaxHealth(5);

            GameObject.Find("EstadisticaText").GetComponent<Text>().text = 
                "¡Has alcanzado los\n" + Stats.nextUpgrade + " puntos!";

        }

        //Se muestra la pantalla con información de la batalla, y a partir de ella el jugador puede ir directamente
        //al siguiente nivel (recibe más puntuación) o volver a la villa (el multiplicador vuelve a 1)
        victoryInfo.SetActive(true);
        EnemyLoader.loadImageSlots();
        EnemyLoader.loadEnemies();
        GameObject.Find("CurrentMapLevel").GetComponent<Text>().text = Stats.mapLevel.ToString();
        GameObject.Find("CurrentBattle").GetComponent<Text>().text = "Batalla " + Stats.mapLevel.ToString();
        GameObject.Find("AventuraAtaqueText").GetComponent<Text>().text = Stats.attack.ToString();
        GameObject.Find("AventuraDefensaText").GetComponent<Text>().text = Stats.defense.ToString();
        GameObject.Find("ComenzarBatallaText").GetComponent<Text>().text = "Comenzar (x" + (scoreMultiplier + 0.1f) + ")"; 
    }

    //Método que se llama cuando el jugador muere. Debe esperar 12 horas antes de volver a intentar la batalla,
    //o utilizar una poción para revivir.
    public IEnumerator battleDefeat()
    {
        yield return new WaitForSeconds(0.5f);
        defeatSprite.SetActive(true);
        backgroundMusic.Stop();
        defeatSound.Play();
        revertStats();
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene(2);
    }

    //Método para revertir las estadísticas a sus valores iniciales
    public void revertStats()
    {
        Stats.attack = attackAtStart;
        Stats.defense = defenseAtStart;
        Stats.maxHealth = maxHealthAtStart;
    }

    public void upgradeAttack()
    {
        statManager.increaseAttack(1);
        Stats.nextUpgrade += 500;
        levelUpScreen.SetActive(false);
    }

    public void upgradeDefense()
    {
        statManager.increaseDefense(1);
        Stats.nextUpgrade += 500;
        levelUpScreen.SetActive(false);
    }

    //Método para el aviso para volver a la villa
    public void villageWarning()
    {
        villaWarningFinal.SetActive(true);
        showingWarning = true;

        if (battleWon)
            GameObject.Find("VillaWarningText").GetComponent<Text>().text = "El multiplicador volvera a ser 1";
        else
            GameObject.Find("VillaWarningText").GetComponent<Text>().text = "El progreso de la batalla se perdera";
    }

    public void returnToVillage()
    {
        scoreMultiplier = 1.0f;
        SceneManager.LoadScene(2);
    }

    //Métodos para llamar a las diferentes corutinas
    public void showObjectMenuMethod()
    {
        StartCoroutine(showObjectMenu());
    }

    public void hideObjectMenuMethod()
    {
        StartCoroutine(hideObjectMenu());
    }

    //Esconde cualquier aviso que haya en pantalla
    public void hideWarnings()
    {
        objectWarning1Final.SetActive(false);
        objectWarning2Final.SetActive(false);
        villaWarningFinal.SetActive(false);
        showingWarning = false;
    }
}
