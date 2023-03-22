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

    int RandomNumberOfEnemies()
    {
        return (int)Random.Range(3, 6);
    }
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

    //----
    int currentRound = 1;
    int currentWave = 1;
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
        // Use a coroutine to display the TextMesh of Round, then Wave, and then wait for 5s before starting the enemy spawning
        StartCoroutine(DisplayTextAfterDelay());

        maxNumberOfBossFirstRound = 0;
        maxNumberOfEyesFirstRound = CommonPropeties.maxNumberOfEyesFirstWave;
        maxNumberOfGoblinFirstRound = CommonPropeties.maxNumberOfGoblinFirstWave;
        maxNumberOfMushroomFirstRound = CommonPropeties.maxNumberOfMushroomFirstWave;
        maxNumberOfSkeletonFirstRound = CommonPropeties.maxNumberOfSkeletonFirstWave;

        //--
        prefabs = new GameObject[] { bossPrefab, eyesPrefab, goblinPrefab, mushroomPrefab, skeletonPrefab };

        // Set the number of enemies to spawn in the first wave
        enemiesTotalWave = maxNumberOfBossFirstRound + maxNumberOfEyesFirstRound + maxNumberOfGoblinFirstRound + maxNumberOfMushroomFirstRound + maxNumberOfSkeletonFirstRound;
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

            if (currentWave > maxWavesPerRound)
            {
                currentRound++;
                currentWave = 1;
                // Increase enemy spawn rates for next round
                increaseEnemySpawnRates();

                enemiesTotalWave = calculateEnemiesTotalWave();
                StartCoroutine(WaitForHeroesToBeSorted());
            }
            else
            {
                Debug.Log("Next wave");
                currentWave++;
                StartCoroutine(DisplayWave());

                if (currentWave == 2)
                {
                    increaseEnemySpawnRates();
                    isSpawnStart = true;
                }

                if (currentWave == 3)
                {
                    increaseEnemySpawnRates();
                    isSpawnStart = true;
                    currentWave++; // advance to wave 4
                }

                enemiesTotalWave = calculateEnemiesTotalWave();
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
        return maxNumberOfBossFirstRound + maxNumberOfEyesFirstRound + maxNumberOfGoblinFirstRound + maxNumberOfMushroomFirstRound + maxNumberOfSkeletonFirstRound;
    }

    private void increaseEnemySpawnRates()
    {
        maxNumberOfEyesFirstRound = (int)Mathf.Round(maxNumberOfEyesFirstRound * 1.25f);
        currentNumberOfEyes = 0;
        maxNumberOfGoblinFirstRound = (int)Mathf.Round(maxNumberOfGoblinFirstRound * 1.25f);
        currentNumberOfGoblin = 0;
        maxNumberOfMushroomFirstRound = (int)Mathf.Round(maxNumberOfMushroomFirstRound * 1.25f);
        currentNumberOfMushroom = 0;
        maxNumberOfSkeletonFirstRound = (int)Mathf.Round(maxNumberOfSkeletonFirstRound * 1.25f);
        currentNumberOfSkeleton = 0;

        if (currentRound % 5 == 0)
        {
            timeSpawn *= spawnRateReduction;
            if (currentRound > 5)
            {
                maxNumberOfBossFirstRound = (int)Mathf.Round(maxNumberOfBossFirstRound * 1.25f);
                currentNumberOfBoss = 0;
            }
        }
    }

    // get random enemy
    private GameObject spawnRandomPrefab()
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

    // spawn enemy
    private void spawnEnemy()
    {
        GameObject prefabToSpawn = spawnRandomPrefab(); // Spawn 1 enemy
        if (prefabToSpawn != null && _enemiesSpawned < enemiesTotalWave)
            _enemiesSpawned++;
    }

    // check if all enemies're destroyed
    private bool isEnemiesDestroyed()
    {
        List<GameObject> enemies = new List<GameObject>();

        // Add Boss
        foreach (var item in GameObject.FindGameObjectsWithTag("Boss"))
        {
            enemies.Add(item);
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
        Notification.text = "Wave " + currentWave;
        Panel.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Panel.gameObject.SetActive(false);
    }

    IEnumerator DisplayTextAfterDelay()
    {
        yield return new WaitForSeconds(5);
        Panel.gameObject.SetActive(true);
        Notification.text = "Round " + currentRound;
        yield return new WaitForSeconds(2f);
        Panel.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DisplayWave());
        yield return new WaitForSeconds(5f);
        Panel.gameObject.SetActive(true);
        Notification.text = "Start";
        yield return new WaitForSeconds(0.5f);
        Panel.gameObject.SetActive(false);
        isSpawnStart = true;
    }
}
