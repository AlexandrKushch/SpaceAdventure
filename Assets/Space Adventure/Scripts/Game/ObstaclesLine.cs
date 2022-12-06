using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesLine : MonoBehaviour
{
    public static float speed;

    private List<GameObject> obstacles;
    private GameObject coin;

    private float duration;
    private Vector3 startPos, endPos;
    private bool newLineSpawned;
    
    private static int count = 0;
    private static int count1 = 0;
    private static int count2 = 0;
    private static int count3 = 0;
    private static int count4 = 0;
    private static int count5 = 0;

    private int c;

    void Awake()
    {
        // Set new line position to the top of the screen before anything else.
        transform.position = new Vector3(transform.position.x, 7, transform.position.z);
        c = 1;
    }

    void Start()
    {
        // Load obstacles from resources.
        LoadObstacles();
        // Load coin from resources.
        LoadCoin();
        // Spawn obstacles and coin.
        SpawnLineOfObstacles();
        // Get line start position.
        startPos = transform.position;
        // Set line end position.
        endPos = new Vector3(transform.position.x, -7, transform.position.z);
    }

    void Update()
    {
        // If line hasn't reached end position.
        if(transform.position != endPos)
        {
            // If player speed is higher than 0.
            if(speed != 0)
            {
                // How long line will travel to the bottom of the screen.
                duration += Time.deltaTime/(10 - speed);
                // Move line to the bottom of the screen.
                transform.position = Vector3.Lerp(startPos, endPos, duration);

                // How much line has to travel to spawn the new line.
                if(!newLineSpawned && duration > ObstacleLineSpawner.instance.spawnPlace)
                {
                    // Spawn new line.
                    ObstacleLineSpawner.instance.SpawnLine();
                    newLineSpawned = true;
                }
            }
        }    
        else
        {
            // Destroy line when it reaches endPos.
            Destroy(gameObject);
        } 
    }

    // Load obstacles from resources.
    private void LoadObstacles()
    {
        obstacles = new List<GameObject>();
        Object[] objects = Resources.LoadAll("Obstacles") as Object[];
        foreach(Object item in objects)
        {
            obstacles.Add(item as GameObject);
        }
    }

    // Load coin from resources.
    private void LoadCoin()
    {
        coin = Resources.Load("Coin") as GameObject;
    }

    // Spawn obstacle in one of five lanes.
    private void SpawnObstacle(int lane)
    {
        // Debug.Log(obstacles.Count);
        int randomObstacleIndex = Random.Range(0, 100);

        if (randomObstacleIndex <= 15) {
            randomObstacleIndex = 0;
            count1++;
        } else if (randomObstacleIndex <= 50) {
            randomObstacleIndex = 1;
            count2++;
        } else if (randomObstacleIndex <= 70) {
            randomObstacleIndex = 2;
            count3++;
        } else if (randomObstacleIndex <= 80) {
            randomObstacleIndex = 3;
            count4++;
        } else if (randomObstacleIndex <= 100) {
            randomObstacleIndex = 4;
            count5++;
        }

        float randomObstacleOffest = Random.Range(0, ObstacleLineSpawner.instance.randomizeObstaclesOffest);

        Instantiate(obstacles[randomObstacleIndex], new Vector3(lane, 7 + randomObstacleOffest, 0), Quaternion.identity, transform);
        count++;
        Debug.Log(count);
        if (count == 100) {
            Debug.Log("Було згенеровано 100 метеоритів");
            Debug.Log("1 meteorit (15%): " + count1);
            Debug.Log("2 meteorit (35%): " + count2);
            Debug.Log("3 meteorit (20%): " + count3);
            Debug.Log("4 meteorit (10%): " + count4);
            Debug.Log("5 meteorit (20%): " + count5);
        }
    }

    // Spawn coin in one of five lanes.
    private void SpawnCoin(int lane)
    {
        float randomCoinOffest = Random.Range(0, ObstacleLineSpawner.instance.randomizeObstaclesOffest);

        Instantiate(coin, new Vector3(lane, 7 + randomCoinOffest, 0), Quaternion.identity, transform);
    }

    // Spawn obstacles into the line.
    private void SpawnLineOfObstacles()
    {
        int minObstacles = ObstacleLineSpawner.instance.minObstacles;
        int maxObstacles = ObstacleLineSpawner.instance.maxObstacles;

        // Get random amount of obstacles that should be spawned into the line.
        int obstaclesAmount = Random.Range(minObstacles, maxObstacles);
        // Get all available lanes.
        List<int> availableLanes = new List<int>() {-2, -1, 0, 1, 2};
        for(int i = 0; i < obstaclesAmount; i++)
        {
            // Get random lane index.
            int randomLaneIndex = Random.Range(0, availableLanes.Count);
            // Spawn obstacle in available line.
            SpawnObstacle(availableLanes[randomLaneIndex]);
            // Remove line, in which new obstacle was spawned, from available lines list.
            availableLanes.RemoveAt(randomLaneIndex);
        }

        // Check if coin should be spawned in the line.
        if(Random.value < ObstacleLineSpawner.instance.coinSpawnRate)
        {
            //Get random available line index.
            int randomLaneIndex = Random.Range(0, availableLanes.Count);
            // Spawn coin in available line.
            SpawnCoin(availableLanes[randomLaneIndex]);
        }
    }
}
