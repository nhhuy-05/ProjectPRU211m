using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject bossPrefab; // Boss
    public GameObject eyesPrefab;
    public GameObject goblinPrefab;
    public GameObject mushroomPrefab;
    public GameObject skeletonPrefab;


    GameObject[] prefabs;

    //---

    int RandomNumberOfEnemies()
    {
        return (int)Random.Range(3, 6);
    }
    int maxNumberOfBossFirstRound = 1;
    int maxNumberOfEyesFirstRound;
    int maxNumberOfGoblinFirstRound;
    int maxNumberOfMushroomFirstRound;
    int maxNumberOfSkeletonFirstRound;

    int currentNumberOfBoss = 0;
    int currentNumberOfEyes = 0;
    int currentNumberOfGoblin = 0;
    int currentNumberOfMushroom = 0;
    int currentNumberOfSkeleton = 0;

    //----
    int currentRound = 1;
    int currentWave = 1;
    
    int maxWavesPerRound = 3;
    int enemiesPerWave;
    public float timeSpawn = 1f;
    public float spawnRateReduction = 0.9f;


    float eslapsedTime = 0;
    int _enemiesSpawned = 0;
    float _nextSpawnTime = 0f;

    int randomNumberOfEnemies()
    {
        return (int)Random.Range(3, 6);
    }

    // Start is called before the first frame update
    void Start()
    {

        maxNumberOfBossFirstRound = 1;
        maxNumberOfEyesFirstRound = 20;
        maxNumberOfGoblinFirstRound = 20;
        maxNumberOfMushroomFirstRound = 20;
        maxNumberOfSkeletonFirstRound = 20;


        // 
        prefabs = new GameObject[] { bossPrefab, eyesPrefab, goblinPrefab, mushroomPrefab, skeletonPrefab };

        // Initialize the next spawn time based on the min time spawn
        _nextSpawnTime = Time.time + timeSpawn;

        // Enemies per wave
        enemiesPerWave = maxNumberOfBossFirstRound + maxNumberOfEyesFirstRound + maxNumberOfGoblinFirstRound + maxNumberOfGoblinFirstRound + maxNumberOfMushroomFirstRound;
    }

    // Update is called once per frame
    void Update()
    {
        eslapsedTime += Time.deltaTime;
            if (eslapsedTime >= _nextSpawnTime)
            {
                SpawnEnemy();
                eslapsedTime = 0;
            }
        
    }

    GameObject SpawnRandomPrefab()
    {
        GameObject prefabToSpawn = null;
        int randomPrefab = Random.Range(0, prefabs.Length);

        switch (randomPrefab)
        {
            case 0:
                if (currentNumberOfBoss < maxNumberOfBossFirstRound && currentRound >= 5)
                {
                    prefabToSpawn = bossPrefab;
                    currentNumberOfBoss++;
                }
                break;
            case 1:
                if (currentNumberOfEyes < maxNumberOfEyesFirstRound)
                {
                    prefabToSpawn = eyesPrefab;
                    currentNumberOfEyes++;
                }
                break;
            case 2:
                if (currentNumberOfGoblin < maxNumberOfGoblinFirstRound)
                {
                    prefabToSpawn = goblinPrefab;
                    currentNumberOfGoblin++;
                }
                break;
            case 3:
                if (currentNumberOfMushroom < maxNumberOfMushroomFirstRound)
                {
                    prefabToSpawn = mushroomPrefab;
                    currentNumberOfMushroom++;
                }
                break;
            case 4:
                if (currentNumberOfSkeleton < maxNumberOfSkeletonFirstRound)
                {
                    prefabToSpawn = skeletonPrefab;
                    currentNumberOfSkeleton++;
                }
                break;
        }

        // Spawn the prefab if there is one to spawn
        if (prefabToSpawn != null)
        {
           return Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }

        return prefabToSpawn;
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = SpawnRandomPrefab(); // Spawn 1 prefab
        

        if (_enemiesSpawned >= enemiesPerWave) // add condition if all enemies were destroyed
        {
            // Start the next wave
            currentWave++;
            _enemiesSpawned = 0;

            if (currentWave == 2)
            {
                if (currentRound >= 5)
                {
                    maxNumberOfBossFirstRound = (int)Mathf.Round(maxNumberOfBossFirstRound * 1.3f);
                }
                maxNumberOfEyesFirstRound = (int)Mathf.Round(maxNumberOfEyesFirstRound * 1.3f);
                maxNumberOfGoblinFirstRound = (int)Mathf.Round(maxNumberOfGoblinFirstRound * 1.3f);
                maxNumberOfMushroomFirstRound = (int)Mathf.Round(maxNumberOfMushroomFirstRound * 1.3f);
                maxNumberOfSkeletonFirstRound = (int)Mathf.Round(maxNumberOfSkeletonFirstRound * 1.3f);
            }

            if (currentWave == 3)
            {
                if (currentRound >= 5)
                {
                    maxNumberOfBossFirstRound = (int)Mathf.Round(maxNumberOfBossFirstRound * 1.5f);
                }
                maxNumberOfEyesFirstRound = (int)Mathf.Round(maxNumberOfEyesFirstRound * 1.5f);
                maxNumberOfGoblinFirstRound = (int)Mathf.Round(maxNumberOfGoblinFirstRound * 1.5f);
                maxNumberOfMushroomFirstRound = (int)Mathf.Round(maxNumberOfMushroomFirstRound * 1.5f);
                maxNumberOfSkeletonFirstRound = (int)Mathf.Round(maxNumberOfSkeletonFirstRound * 1.5f);
            }

            if (currentWave > maxWavesPerRound)
            {
                // Start the next round
                currentRound++;
                currentWave = 1;

                maxNumberOfEyesFirstRound = (int)Mathf.Round(maxNumberOfEyesFirstRound * 1.25f);
                maxNumberOfGoblinFirstRound = (int)Mathf.Round(maxNumberOfGoblinFirstRound * 1.25f);
                maxNumberOfMushroomFirstRound = (int)Mathf.Round(maxNumberOfMushroomFirstRound * 1.25f);
                maxNumberOfSkeletonFirstRound = (int)Mathf.Round(maxNumberOfSkeletonFirstRound * 1.25f);


                if (currentRound % 5 == 0)
                {
                    timeSpawn = timeSpawn * spawnRateReduction;
                    if (currentRound > 5)
                    {
                        maxNumberOfBossFirstRound = (int)Mathf.Round(maxNumberOfBossFirstRound * 1.25f);
                    }
                }

            }
        }

        if(prefabToSpawn != null)
            _enemiesSpawned++;
    }
}
