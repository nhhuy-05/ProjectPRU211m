using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemies : MonoBehaviour
{
    public Image Panel;
    public TextMeshProUGUI Notification;

    public GameObject bossPrefab; // Boss
    public GameObject eyesPrefab;
    public GameObject goblinPrefab;
    public GameObject mushroomPrefab;
    public GameObject skeletonPrefab;


    GameObject[] prefabs;

    //---
    int maxNumberOfBossFirstRound;
    int maxNumberOfEyesFirstRound;
    int maxNumberOfGoblinFirstRound;
    int maxNumberOfMushroomFirstRound;
    int maxNumberOfSkeletonFirstRound;

    int currentNumberOfBoss = 0;
    int currentNumberOfEyes = 0;
    int currentNumberOfGoblin = 0;
    int currentNumberOfMushroom = 0;
    int currentNumberOfSkeleton = 0;

    int enemiesTotalWave = 0;

    int maxWavesPerRound = 3;
    public float timeSpawn = 1f;
    public float spawnRateReduction = 0.9f;


    float eslapsedTime = 0;
    int _enemiesSpawned = 0;
    float _nextSpawnTime = 0f;
    //---

    bool isSpawnStart = false;

    // Start is called before the first frame update
    void Start()
    {
        // Load Data
        SaveLoad.LoadData();
        // Use a coroutine to display the TextMesh of Round, then Wave, and then wait for 5s before starting the enemy spawning
        StartCoroutine(DisplayTextAfterDelay());
        int m = 0;

        if (CommonPropeties.currentWave == 1) m = 3;
        if (CommonPropeties.currentWave == 2) m = 2;
        if (CommonPropeties.currentWave == 3) m = 1;

        maxNumberOfBossFirstRound = (int)Math.Round(CommonPropeties.maxNumberOfBossFirstWave * Math.Pow(1.25, CommonPropeties.currentRound));
        maxNumberOfEyesFirstRound = (int)Math.Round(CommonPropeties.maxNumberOfEyesFirstWave * Math.Pow(1.25, 3 * CommonPropeties.currentRound - m));
        maxNumberOfGoblinFirstRound = (int)Math.Round(CommonPropeties.maxNumberOfGoblinFirstWave * Math.Pow(1.25, 3 * CommonPropeties.currentRound - m));
        maxNumberOfMushroomFirstRound = (int)Math.Round(CommonPropeties.maxNumberOfMushroomFirstWave * Math.Pow(1.25, 3 * CommonPropeties.currentRound - m));
        maxNumberOfSkeletonFirstRound = (int)Math.Round(CommonPropeties.maxNumberOfSkeletonFirstWave * Math.Pow(1.25, 3 * CommonPropeties.currentRound - m));

        //--
        prefabs = new GameObject[] { bossPrefab, eyesPrefab, goblinPrefab, mushroomPrefab, skeletonPrefab };

        // Set the number of enemies to spawn in the first wave
        enemiesTotalWave = calculateEnemiesTotalWave();
        // Initialize the next spawn time based on the min time spawn
        _nextSpawnTime = Time.time + timeSpawn;

    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawnStart)
        {
            eslapsedTime += Time.deltaTime;
            if (eslapsedTime >= 0.1)
            {
                spawnEnemy();
                spawnController();  // run if all enemies're destroyed
                eslapsedTime = 0;
            }
        }

    }

    private void spawnController()
    {
        if (isEnemiesDestroyed() && _enemiesSpawned == enemiesTotalWave)
        {
            _enemiesSpawned = 0;
            isSpawnStart = false;


            if (CommonPropeties.currentWave > maxWavesPerRound || CommonPropeties.currentWave ==3)
            {
                CommonPropeties.currentRound++;
                CommonPropeties.currentWave = 1;
                // Increase enemy spawn rates for next round
                increaseEnemySpawnRates();

                enemiesTotalWave = calculateEnemiesTotalWave();
                StartCoroutine(WaitForHeroesToBeSorted());
            }
            else
            {
                Debug.Log("Next wave");
                CommonPropeties.currentWave++;
                StartCoroutine(DisplayWave());

                if (CommonPropeties.currentWave == 2)
                {
                    increaseEnemySpawnRates();
                    enemiesTotalWave = calculateEnemiesTotalWave();
                    SaveLoad.SaveHeroes();
                    SaveLoad.SaveData();
                    isSpawnStart = true;
                }

                if (CommonPropeties.currentWave == 3)
                {
                    increaseEnemySpawnRates();
                    enemiesTotalWave = calculateEnemiesTotalWave();
                    SaveLoad.SaveHeroes();
                    SaveLoad.SaveData();
                    CommonPropeties.currentWave++; // advance to wave 4
                    isSpawnStart = true;
                }

                float waitTime = 5f;
                StartCoroutine(WaitForNextWave(waitTime));
            }
        }
    }

    // Wait for specified amount of time before starting the next wave/round
    private IEnumerator WaitForNextWave(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    private int calculateEnemiesTotalWave()
    {
        // boss just spawns once per round in wave 3
        if (CommonPropeties.currentWave == 3)
        {
            Debug.Log("Boss wave");
            return maxNumberOfBossFirstRound + maxNumberOfEyesFirstRound + maxNumberOfGoblinFirstRound + maxNumberOfMushroomFirstRound + maxNumberOfSkeletonFirstRound;
        }
        else return maxNumberOfEyesFirstRound + maxNumberOfGoblinFirstRound + maxNumberOfMushroomFirstRound + maxNumberOfSkeletonFirstRound;
    }

    private void increaseEnemySpawnRates()
    {
        Debug.Log("Increasing enemy spawn rates");
        maxNumberOfEyesFirstRound = (int)Mathf.Round(maxNumberOfEyesFirstRound * 1.25f);
        currentNumberOfEyes = 0;
        maxNumberOfGoblinFirstRound = (int)Mathf.Round(maxNumberOfGoblinFirstRound * 1.25f);
        currentNumberOfGoblin = 0;
        maxNumberOfMushroomFirstRound = (int)Mathf.Round(maxNumberOfMushroomFirstRound * 1.25f);
        currentNumberOfMushroom = 0;
        maxNumberOfSkeletonFirstRound = (int)Mathf.Round(maxNumberOfSkeletonFirstRound * 1.25f);
        currentNumberOfSkeleton = 0;
        // check if current wave is 3, if so, increase the spawn rate of the boss
        if (CommonPropeties.currentWave == 3)
        {
            maxNumberOfBossFirstRound = (int)Math.Round(CommonPropeties.maxNumberOfBossFirstWave * Math.Pow(1.25, CommonPropeties.currentRound));
            Debug.Log("Current boss number: " + currentNumberOfBoss);
            Debug.Log("Current wave: " + CommonPropeties.currentWave);
            currentNumberOfBoss = 0;
        }
        else { maxNumberOfBossFirstRound = 0; }

        // Reduce the time between spawns
        if (CommonPropeties.currentRound % 5 == 0)
        {
            timeSpawn *= spawnRateReduction;
        }
    }

    // get random enemy
    private GameObject spawnRandomPrefab()
    {
        GameObject prefabToSpawn = null;
        int randomPrefab = UnityEngine.Random.Range(0, prefabs.Length);
        
        switch (randomPrefab)
        {
            case 0:
                if (currentNumberOfBoss < maxNumberOfBossFirstRound && CommonPropeties.currentWave >=3 )
                {
                    Debug.Log("Spawning boss");
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

    // spawn enemy
    private void spawnEnemy()
    {
        GameObject prefabToSpawn = spawnRandomPrefab(); // Spawn 1 enemy
        if (prefabToSpawn != null && _enemiesSpawned < enemiesTotalWave)
            _enemiesSpawned++;
        Debug.Log("Spawned: " + _enemiesSpawned);
    }

    // check if all enemies're destroyed
    private bool isEnemiesDestroyed()
    {
        List<GameObject> enemies = new List<GameObject>();

        // Add Boss
        foreach (var item in GameObject.FindGameObjectsWithTag("Boss"))
        {
            enemies.Add(item);
            Debug.Log("Boss");
        }

        // Add Eyes
        foreach (var item in GameObject.FindGameObjectsWithTag("Eyes"))
        {
            enemies.Add(item);
        }

        // Add goblin
        foreach (var item in GameObject.FindGameObjectsWithTag("Goblin"))
        {
            enemies.Add(item);
        }

        // Add Mushroom
        foreach (var item in GameObject.FindGameObjectsWithTag("Mushroom"))
        {
            enemies.Add(item);
        }

        // Add Skeleton
        foreach (var item in GameObject.FindGameObjectsWithTag("Skeleton"))
        {
            enemies.Add(item);
        }

        if (enemies.Count() == 0)
        {
            return true;
        }
        return false;
    }

    private IEnumerator WaitForHeroesToBeSorted()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DisplayTextAfterDelay());
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator DisplayWave()
    {
        Notification.text = "Wave " + CommonPropeties.currentWave;
        Panel.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Panel.gameObject.SetActive(false);
    }

    IEnumerator DisplayTextAfterDelay()
    {
        yield return new WaitForSeconds(5);
        Panel.gameObject.SetActive(true);
        Notification.text = "Round " + CommonPropeties.currentRound;
        yield return new WaitForSeconds(2f);
        Panel.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DisplayWave());
        yield return new WaitForSeconds(5f);
        Panel.gameObject.SetActive(true);
        Notification.text = "Start";
        yield return new WaitForSeconds(0.5f);
        Panel.gameObject.SetActive(false);
        SaveLoad.SaveHeroes();
        SaveLoad.SaveData();
        isSpawnStart = true;
    }
}
