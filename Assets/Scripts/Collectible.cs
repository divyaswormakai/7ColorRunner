using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Vector2 screenBounds;
    private float fallSpeed = 0f;
    Player player;
    GameController gameController;
    void Start()
    {
        // screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        player = FindObjectOfType<Player>();
        gameController = FindObjectOfType<GameController>();

        screenBounds = gameController.getScreenBounds();


    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.isGamePaused)
        {
            if (transform.position.y <= player.transform.position.y + 0.1f)
            {
                Destroy(gameObject);
            }
            else
            {
                if (player.isInvincible)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, fallSpeed * Time.deltaTime);
                }
                else
                {
                    Vector2 pos = transform.position;
                    if (gameController.isTimeSlowed)
                    {
                        pos.y -= (fallSpeed * Time.deltaTime) / 2f;
                    }
                    else
                    {
                        pos.y -= fallSpeed * Time.deltaTime;
                    }
                    transform.position = pos;
                }
            }
        }
    }

    public void SetSpeed(float speed)
    {
        fallSpeed = speed;
    }

}
