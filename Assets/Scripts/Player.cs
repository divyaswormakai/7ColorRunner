using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] sprite;
    public bool isInvincible = false;
    public ParticleSystem particles, wrongCollision;

    private SpriteRenderer spriteRenderer;
    private bool isShiningSound = false;
    private float invincibleTimer = 10f;
    private int currIndx = 0;

    private GameController gameController;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = FindObjectOfType<GameController>();

    }

    private void Update()
    {
        if (isInvincible && !gameController.isGamePaused)
        {
            invincibleTimer -= Time.deltaTime;
            gameController.SetBonusBoardText("Invincible Time: " + invincibleTimer.ToString());
            if (invincibleTimer < 0f)
            {
                ChangePlayerSprite(currIndx);
                isInvincible = false;
                invincibleTimer = 10f;
                gameController.isTimePaused = false;
                gameController.SetBonusBoardText("");
                gameController.DisableBtns(false);
            }
        }
    }

    public void ChangePlayerSprite(int index)
    {
        spriteRenderer.sprite = sprite[index];
        currIndx = index;
        particles.textureSheetAnimation.SetSprite(0, sprite[index]);
    }

    public void StartInvisibleMode()
    {
        spriteRenderer.sprite = sprite[7];
        isInvincible = true;
        gameController.DisableBtns(true);
        gameController.isTimeSlowed = false;
        particles.textureSheetAnimation.SetSprite(0, sprite[7]);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {

        Destroy(collider.gameObject.GetComponent<CircleCollider2D>());
        if (isInvincible)
        {
            //play sound
            isShiningSound = true;
            gameController.IncreaseBonusScore();
            gameController.IncreaseScore();
            particles.Emit(30);
        }
        else
        {
            if (spriteRenderer.sprite.name != collider.gameObject.name.Split('(')[0])
            {
                Destroy(collider.gameObject);
                FindObjectOfType<PlayerMovement>().DecreaseSpeed();
                gameController.IncreaseTime(-1f);
                particles.Stop();
                particles.Clear();
                StartCoroutine(StartEmitting());
                wrongCollision.Emit(30);
            }
            else
            {
                //play sound
                isShiningSound = true;
                gameController.IncreaseScore();
                gameController.IncreaseTime(3f);
                particles.Emit(30);
            }
        }
    }

    public void EnableMovement(bool mode)
    {
        print(mode);
        GetComponent<PlayerMovement>().enabled = mode;
    }

    public void Shrink(bool mode)
    {
        if (mode)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        else
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
    }

    IEnumerator StartEmitting()
    {
        yield return new WaitForSeconds(0.5f);
        particles.Play();
    }
}