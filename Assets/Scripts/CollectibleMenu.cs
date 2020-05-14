using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMenu : MonoBehaviour
{
    private Vector2 screenBounds;
    private float fallSpeed = 0f;
    GameController gameController;
    void Start()
    {
        // screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        gameController = FindObjectOfType<GameController>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));


    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        if (pos.y <= screenBounds.y * -1)
        {
            Destroy(gameObject);
        }
        else
        {
            pos.y -= fallSpeed * Time.deltaTime;
            transform.position = pos;
        }
    }

    public void SetSpeed(float speed)
    {
        fallSpeed = speed;
    }
}
