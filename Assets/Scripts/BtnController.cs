using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnController : MonoBehaviour
{
    public int activeColor = 0;

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
    }

    public void BackToMenuBtn()
    {
        StartCoroutine(FindObjectOfType<LevelLoader>().LoadGameFromMenu("Menu"));
    }

    public void PlayBtn()
    {
        StartCoroutine(FindObjectOfType<LevelLoader>().LoadGameFromMenu("Main"));
    }

    public void HardBtn()
    {
        StartCoroutine(FindObjectOfType<LevelLoader>().LoadGameFromMenu("Hard"));
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
