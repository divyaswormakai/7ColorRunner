using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject[] collectibles;

    private float currSpawnTime = 3f;   //Spawn at current interval of time
    private int spawnNumber = 1;        //No of collectible to spawn
    private int collectibleIndRange = 3;   //Range of color to spawn
    private float currTime;             //For spawn time
    private Vector2 screenBounds;
    private float currFallSpeed = 4f;       //FallSpeed for the collectible
    GameController gameController;
    private List<float> laneList;

    void Start()
    {
        currTime = currSpawnTime;
        gameController = FindObjectOfType<GameController>();
        screenBounds = gameController.getScreenBounds();

        laneList = new List<float>();
        CreateLanePosition();

        //set collectibleIndRange to no of available btns if scene hard
        if (SceneManager.GetActiveScene().name == "Hard") collectibleIndRange = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.isGamePaused)
        {
            SpawnObject();
        }
    }

    void CreateLanePosition()
    {
        float totalWidth = (screenBounds.x * 2) - 4f;
        float laneWidth = totalWidth / 8;
        float startPoint = (screenBounds.x * -1) + 3f;
        laneList.Add(startPoint);
        for (int i = 0; i < 8; i++)
        {
            startPoint += laneWidth;
            laneList.Add(startPoint);
        }
    }
    private void SpawnObject()
    {
        if (currTime >= 0f)
        {
            currTime -= Time.deltaTime;
        }
        else
        {
            List<float> tempList = new List<float>(laneList.ToArray());  //Similar to deep copy
            for (int i = 0; i < spawnNumber; i++)
            {
                currTime = currSpawnTime;
                int randomInt = UnityEngine.Random.Range(0, collectibleIndRange);

                GameObject gameobj = Instantiate(collectibles[randomInt]);
                Vector2 spawnPos = new Vector2(0, 0);
                spawnPos.y = screenBounds.y + 2f;
                int indx = Random.Range(0, tempList.Count - 1);
                spawnPos.x = tempList[indx];
                tempList.Remove(spawnPos.x);
                gameobj.transform.position = spawnPos;
                gameobj.GetComponent<Collectible>().SetSpeed(currFallSpeed);
            }

        }
    }

    private float GetRandomXPos()
    {
        float xPos = Random.Range((screenBounds.x - 2f) * -1, screenBounds.x - 2f);

        return xPos;
    }

    public void IncreaseDifficulty()
    {

        currSpawnTime -= .5f;
        currSpawnTime = Mathf.Clamp(currSpawnTime, 2f, 5f);

        if (collectibleIndRange < 6)
        {
            collectibleIndRange += 1;
        }

        if (spawnNumber < 4)
        {
            spawnNumber += 1;
        }

        currFallSpeed += 0.2f;
        currFallSpeed = Mathf.Clamp(currFallSpeed, 4f, 6f);
    }
}
