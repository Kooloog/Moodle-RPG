using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMovement : MonoBehaviour
{
    public static GameObject avatar;
    public static float speed = 3f;

    private Rigidbody2D avatarRB;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        avatar = GameObject.Find("Avatar");
        avatarRB = avatar.GetComponent<Rigidbody2D>();

        //Re-escalando al personaje
        avatar.transform.localScale = 
            new Vector2(avatar.transform.localScale.x / 5, avatar.transform.localScale.y / 5);

        //Reposicionando al personaje
        avatar.transform.position = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
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

    private void FixedUpdate()
    {
        avatarRB.MovePosition(avatarRB.position + movement * speed * Time.fixedDeltaTime);
    }
}
