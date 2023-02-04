using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    private int amountOfDifferrentEnemies;
    public List<Transform> spawningPositions;

    private List<GameObject> aliveEnemies;
    private List<GameObject> deadEnemies;

    private int waveCount = 17;
    void Start()
    {
        amountOfDifferrentEnemies = enemyPrefabs.Count;
        aliveEnemies = new List<GameObject>();
        deadEnemies = new List<GameObject>();

        SpawnNextWave();
    }


    public void SpawnNextWave()
    {
        waveCount++;
        //TODO: Make async
        int amountOfPositions = AmountOfPositions();

        List<Transform> currentSpawningPositions;
        if (amountOfPositions == amountOfDifferrentEnemies)
        {
            currentSpawningPositions = spawningPositions;
        }
        else
        {
            currentSpawningPositions = new List<Transform>();
            for(int i = 0; i < amountOfPositions; i++)
            {
                currentSpawningPositions.Add(spawningPositions[i]);
            }
        }

        for (int i = 0; i < waveCount; i++)
        {
            Transform currentSpawningPosition = currentSpawningPositions[Random.Range(0, amountOfPositions)];
            Instantiate(
                enemyPrefabs[Random.Range(0, amountOfDifferrentEnemies)],
                currentSpawningPosition.transform.position + (Vector3.right + Vector3.forward) * i ,
                currentSpawningPosition.rotation);
        }
    }


    public int AmountOfPositions()
    {
        int positionAmount = (int)Mathf.Sqrt(waveCount);
        return positionAmount > spawningPositions.Count ? spawningPositions.Count: positionAmount;
    }

}
