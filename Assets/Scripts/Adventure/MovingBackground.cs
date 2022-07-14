using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    GameObject backgroundNormal;
    GameObject backgroundInverted;

    GameObject checkerboardTotal;
    GameObject checkerboard;

    Vector2 backgroundSpawnNormal;
    Vector2 backgroundSpawnInverted;
    Vector2 backgroundTrigger;
    Vector2 checkerboardSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //Obtenemos los GameObjects asociados a cada mitad del fondo
        backgroundNormal = GameObject.Find("FondoNormal");
        backgroundInverted = GameObject.Find("FondoInvertido");

        //Se elige un color al azar para el fondo de la batalla
        int randomColorId = Random.Range(0, 5);
        switch(randomColorId)
        {
            case 0:
                changeBackgrounds(backgroundNormal, backgroundInverted, new Color(1f, 0f, 0f)); break;
            case 1:
                changeBackgrounds(backgroundNormal, backgroundInverted, new Color(0f, 1f, 0f)); break;
            case 2:
                changeBackgrounds(backgroundNormal, backgroundInverted, new Color(0f, 0f, 1f)); break;
            case 3:
                changeBackgrounds(backgroundNormal, backgroundInverted, new Color(0f, 1f, 1f)); break;
            case 4:
                changeBackgrounds(backgroundNormal, backgroundInverted, new Color(1f, 1f, 0f)); break;
        }

        //Obtenemos las distintas partes del fondo de cuadros
        checkerboardTotal = GameObject.Find("Cuadros");
        checkerboard = GameObject.Find("Checkers");

        //Obtenemos los parámetros para completar la animación del fondo
        backgroundSpawnNormal = backgroundInverted.transform.position;
        backgroundSpawnInverted = backgroundSpawnNormal;
        backgroundSpawnInverted.x = 
            backgroundSpawnInverted.x + backgroundNormal.GetComponent<SpriteRenderer>().bounds.size.x;

        checkerboardSpawn = GameObject.Find("CuadrosPlaceholder").transform.position;

        backgroundTrigger = backgroundNormal.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-2f * Time.deltaTime, 0, 0);
        checkerboardTotal.transform.Translate(-1.92f * Time.deltaTime, -0.96f * Time.deltaTime, 0, 0);

        //Fondo
        if(backgroundInverted.transform.position.x <= backgroundTrigger.x)
        {
            GameObject backgroundNormalAux = GameObject.Instantiate(
                backgroundNormal, backgroundSpawnNormal, Quaternion.identity, this.gameObject.transform);
            GameObject backgroundInvertedAux = GameObject.Instantiate(
                backgroundInverted, backgroundSpawnInverted, Quaternion.identity, this.gameObject.transform);
            backgroundInvertedAux.GetComponent<SpriteRenderer>().flipX = true;

            StartCoroutine(destroyOverTime(new[] { backgroundNormal, backgroundInverted }, 10f));
            backgroundNormal = backgroundNormalAux;
            backgroundInverted = backgroundInvertedAux;
        }

        //Cuadros
        if (checkerboard.transform.position.x <= 2.4f)
        {
            GameObject checkerboardAux = GameObject.Instantiate(
                checkerboard, checkerboardSpawn, Quaternion.identity, checkerboardTotal.transform);

            checkerboardAux.transform.position = new Vector3(checkerboardAux.transform.position.x,
                checkerboardAux.transform.position.y, 0f);

            StartCoroutine(destroyOverTime(new[] { checkerboard }, 11f));
            checkerboard = checkerboardAux;
            checkerboard.SetActive(true);
        }

        //Mantiene el fondo de cuadros siempre por encima del resto del fondo.
        checkerboard.transform.position = new Vector3(checkerboard.transform.position.x,
            checkerboard.transform.position.y, 800f);
    }

    //Función para cambiar el color del fondo, dado el color
    private void changeBackgrounds(GameObject bg1, GameObject bg2, Color col)
    {
        bg1.GetComponent<SpriteRenderer>().color = col;
        bg2.GetComponent<SpriteRenderer>().color = col;
    }

    //Destruye los elementos del fondo que desaparecen de la pantalla
    IEnumerator destroyOverTime(GameObject[] bg, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        foreach (GameObject bgi in bg) Destroy(bgi);
    }
}
