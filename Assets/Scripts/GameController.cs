﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public TextMeshProUGUI scoreBoard, timeBoard, bonusBoard, instruction;
    public Button[] btns;
    public Canvas pauseMenu;
    public bool isGamePaused = false;
    public bool isTimePaused = false;
    public bool isTimeSlowed = false;


    private Vector2 screenBounds;
    private int score = 0;
    private int bonusScore = 0;
    private float timeRem = 20f;
    private int currActiveIndx = 3;
    private int tenMultiplier = 0;
    private float slowTimeDivider = 1f;
    private float slowTimeRem = 5f;
    private int invincibleDigit = 10;
    private Player player;

    // Start is called before the first frame update
    void Awake()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Start()
    {
        scoreBoard.text = "Score: " + score.ToString() + "\nBonus: " + bonusScore.ToString();
        player = FindObjectOfType<Player>();
    }

    //fix bonus during pause menu
    void Update()
    {
        if (timeRem < 0)
        {
            //SceneManager.LoadScene("Main");
        }

        //pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            if (isGamePaused)
            {
                isTimePaused = true;
                DisableBtns(true);
                pauseMenu.gameObject.SetActive(true);
                player.EnableMovement(false);
            }
            else
            {
                print("Pause complete mf");
                isTimePaused = false;
                DisableBtns(false);
                pauseMenu.gameObject.SetActive(false);
                player.EnableMovement(true);
            }
        }

        if (!isTimePaused)
        {
            timeRem -= Time.deltaTime / slowTimeDivider;
            timeBoard.text = "Time:\n " + timeRem.ToString();
        }


        //For slowMotion part
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bonusScore >= 15)
            {
                StartSlowMo();
            }
        }
        foreach (Touch t in Input.touches)
        {
            if (t.tapCount >= 2)
            {
                if (bonusScore >= 15)
                {
                    StartSlowMo();
                    bonusScore -= 15;
                    scoreBoard.text = "Score: " + score.ToString() + "\nBonus: " + bonusScore.ToString();
                }
            }
        }
        if (isTimeSlowed && !isGamePaused)
        {
            print("time Slowed");
            slowTimeRem -= Time.deltaTime;
            SetBonusBoardText("Slow Time Remainig:" + slowTimeRem.ToString());
            if (slowTimeRem < 0f)
            {
                isTimeSlowed = false;
                slowTimeRem = 5f;
                slowTimeDivider = 1f;
                SetBonusBoardText("");
                player.Shrink(false);
            }
        }
    }

    public Vector2 getScreenBounds()
    {
        return screenBounds;
    }

    public void IncreaseScore()
    {
        score += 1;
        scoreBoard.text = "Score: " + score.ToString() + "\nBonus: " + bonusScore.ToString();

        if (score % invincibleDigit == 0)
        {
            player.StartInvisibleMode();
            isTimePaused = true;

        }
        if (currActiveIndx < 7 && score % (10 + 10 * tenMultiplier) == 0)
        {
            if (!player.isInvincible)
            {
                btns[currActiveIndx].gameObject.SetActive(true);
            }
            FindObjectOfType<Spawner>().IncreaseDifficulty();

            currActiveIndx++;
            tenMultiplier += 2;

        }
    }

    public void IncreaseBonusScore()
    {
        bonusScore += 1;
        if (bonusScore > 15)
        {
            instruction.gameObject.SetActive(true);
        }
        else
        {
            instruction.gameObject.SetActive(false);
        }
    }

    public void StartSlowMo()
    {
        if (!player.isInvincible)
        {
            isTimeSlowed = true;
            slowTimeDivider = 4f;
            player.Shrink(true);

        }
    }

    public void DisableBtns(bool mode)
    {
        if (mode)
        {
            foreach (Button btn in btns)
            {
                btn.gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < currActiveIndx; i++)
            {
                btns[i].gameObject.SetActive(true);
            }
        }

    }
    public void IncreaseTime(float time)
    {
        timeRem += time;
    }

    public void SetBonusBoardText(string text)
    {
        bonusBoard.text = text;
    }

}
