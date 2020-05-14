using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 10f;
    private Vector2 screenBounds;
    private float objWidth;

    private bool isSlowedDown = false;
    private float slowTime;

    void Start()
    {
        screenBounds = FindObjectOfType<GameController>().getScreenBounds();
        objWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
    }

    void FixedUpdate()
    {
        PlayerMove();
        if (isSlowedDown)
        {
            slowTime -= Time.deltaTime;
            if (slowTime < 0f)
            {
                isSlowedDown = true;
                slowTime = 4f;
                speed = 10f;
            }
        }


    }

    private void PlayerMove()
    {

        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }

        Vector2 tilt = Input.acceleration;

        if (Mathf.Abs(tilt.x) > 0.05f)
        {
            Vector3 pos = transform.position;
            pos.x += tilt.x * 15 * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, screenBounds.x * -1 + objWidth, screenBounds.x - objWidth);
            transform.position = pos;
        }



    }

    public void MoveLeft()
    {
        Vector3 pos = transform.position;
        pos.x -= speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, screenBounds.x * -1 + objWidth, screenBounds.x - objWidth);
        transform.position = pos;
    }

    public void MoveRight()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, screenBounds.x * -1 + objWidth, screenBounds.x - objWidth);
        transform.position = pos;
    }

    public void DecreaseSpeed()
    {
        isSlowedDown = true;
        slowTime = 4f;
        speed /= 2f;
        speed = Mathf.Clamp(speed, 2f, 10f);
    }
}