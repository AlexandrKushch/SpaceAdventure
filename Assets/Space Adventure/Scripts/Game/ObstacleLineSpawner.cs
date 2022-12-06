using System.Collections.Generic;
using UnityEngine;

public class ObstacleLineSpawner : MonoBehaviour
{
    [Range(0, 4)]
    public int minObstacles, maxObstacles;
    [Range(0.2f, 0.8f)]
    public float spawnPlace;
    [Range(0, 1.5f)]
    public float randomizeObstaclesOffest;
    [Range(0, 1.0f)]
    public float coinSpawnRate;

    public static ObstacleLineSpawner instance;

    private int lineIndex;
    private GameObject spin;

    void Start()
    {
        instance = this;
        // Spawn line when game starts.
        

        if (Highscore.GetAmount() > 40)
        {
            GenerateMaze();
        } 
        else if (Highscore.GetAmount() > 30) {
            spin = Instantiate((GameObject)Resources.Load("CenterOfYBullets"), new Vector3(0, 3, 0), Quaternion.identity);
            SpawnLine();
        }
        else {
            SpawnLine();
        }
    }

    // Used to spawn obstacle line.
    public void SpawnLine()
    {
        // Create new line gameobject.
        GameObject line = new GameObject("Line-" + lineIndex);
        // Make line gameobject child of current gameobject.
        line.transform.parent = transform;
        // Add ObstaclesLine script to the gameobject.
        line.AddComponent<ObstaclesLine>();
        // Start counting when player reaches first line of rocks.
        if (lineIndex > 1)
        {
            // Increase player score.
            IncreaseScore();
        }
        // Increase line index.
        lineIndex++;
    }

    // Increase player score by one.
    private void IncreaseScore()
    {
        Score.SetAmount(Score.GetAmount() + 1);
    }

    private void GenerateMaze() {
        List<GameObject> mazes = new List<GameObject>();
        Object[] objects = Resources.LoadAll("Mazes") as Object[];
        foreach(Object item in objects)
        {
            mazes.Add(item as GameObject);
        }

        int index = Random.Range(0, mazes.Count);

        Instantiate(mazes[index], Vector3.zero, Quaternion.identity);
    }
}
