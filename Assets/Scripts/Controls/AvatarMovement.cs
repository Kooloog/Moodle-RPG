using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarMovement : MonoBehaviour
{
    public static GameObject avatar;
    public static float speed = 3f;

    private Rigidbody2D avatarRB;
    private Vector2 movement;
    private DeathManager deathManager;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Avatar") == null)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        avatar = GameObject.Find("Avatar");
        avatarRB = avatar.GetComponent<Rigidbody2D>();

        //Re-escalando al personaje
        avatar.transform.localScale = 
            new Vector2(4.35f, 4.23f);

        //Reposicionando al personaje
        avatar.transform.position = new Vector2(0, 0);

        //Comprobando si el personaje está vivo o no
        deathManager = GameObject.Find("DeathManager").GetComponent<DeathManager>();
        deathManager.checkAlive();
    }

    // Update is called once per frame
    void Update()
    {
        if (!DeathManager.isDead)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                avatar.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                avatar.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!MapHandler.activated)
        {
            avatarRB.MovePosition(avatarRB.position + movement * speed * Time.fixedDeltaTime);
        }
    }
}
