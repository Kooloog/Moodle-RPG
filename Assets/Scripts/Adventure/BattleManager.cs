using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("Avatar")) SceneManager.LoadScene(1);

        GameObject placeholder = GameObject.Find("AvatarPlaceholder");
        GameObject avatar = GameObject.Find("Avatar");

        avatar.transform.position = placeholder.transform.position;
        avatar.transform.localScale = new Vector2(
            avatar.transform.localScale.x * 2, avatar.transform.localScale.y * 2);

        placeholder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            SceneManager.LoadScene(1);
        }
    }
}
