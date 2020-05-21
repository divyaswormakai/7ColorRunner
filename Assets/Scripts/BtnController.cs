using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnController : MonoBehaviour
{
    public int activeColor = 0;

    public AudioSource btnAudio;

    GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    public void BtnPress()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        switch (name)
        {
            case "RedBtn":
                {
                    activeColor = 1;
                    break;
                }
            case "GreenBtn":
                {
                    activeColor = 2;
                    break;
                }
            case "IndigoBtn":
                {
                    activeColor = 3;
                    break;
                }
            case "OrangeBtn":
                {
                    activeColor = 4;
                    break;
                }
            case "VioletBtn":
                {
                    activeColor = 5;
                    break;
                }
            case "YellowBtn":
                {
                    activeColor = 6;
                    break;
                }
            default:
                {
                    activeColor = 0;
                    break;
                }
        }
        FindObjectOfType<Player>().ChangePlayerSprite(activeColor);
    }

    public void ResumeBtn()
    {
        gameController.isGamePaused = false;
        gameController.isTimePaused = false;
        gameController.DisableBtns(false);
        gameController.pauseMenu.gameObject.SetActive(false);
        FindObjectOfType<Player>().EnableMovement(true);
        btnAudio.Play();
    }

    public void BackToMenuBtn()
    {
        StartCoroutine(FindObjectOfType<LevelLoader>().LoadGameFromMenu("Menu"));
        btnAudio.Play();
    }

    public void PlayBtn()
    {
        StartCoroutine(FindObjectOfType<LevelLoader>().LoadGameFromMenu("Main"));
        btnAudio.Play();
    }

    public void HardBtn()
    {
        StartCoroutine(FindObjectOfType<LevelLoader>().LoadGameFromMenu("Hard"));
        btnAudio.Play();
    }

    public void ExitBtn()
    {
        Application.Quit();
        btnAudio.Play();
    }

    public void ShowAdBtn()
    {
        FindObjectOfType<AdManager>().PlayRewardedVideo();
        btnAudio.Play();
    }

    public void RestartBtn()
    {
        StartCoroutine(FindObjectOfType<LevelLoader>().LoadGameFromMenu(SceneManager.GetActiveScene().name));
        btnAudio.Play();
    }

    public void ResumeFromGameOver()
    {
        gameController.ResumeGame();
        btnAudio.Play();
    }
}
