using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] collectibles;

    private float currSpawnTime = 2f;   //Spawn at current interval of time
    private int spawnNumber = 2;        //No of collectible to spawn
    private int collectibleIndRange = 7;   //Range of color to spawn
    private float currTime;             //For spawn time
    private Vector2 screenBounds;
    private float currFallSpeed = 4f;       //FallSpeed for the collectible
    private List<float> laneList;


    void Start()
    {
        currTime = currSpawnTime;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        laneList = new List<float>();
        CreateLanePosition();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnObject();
    }

    void CreateLanePosition()
    {
        float totalWidth = (screenBounds.x * 2) - 4f;
        float laneWidth = totalWidth / 4;
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
                gameobj.GetComponent<CollectibleMenu>().SetSpeed(currFallSpeed);
            }

        }
    }

    private float GetRandomXPos()
    {
        float xPos = Random.Range((screenBounds.x - 2f) * -1, screenBounds.x - 2f);

        return xPos;
    }
}
