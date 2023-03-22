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
        if (isEnemiesDestroyed() && _enemiesSpawned==enemiesTotalWave)
        {
            _enemiesSpawned = 0;
            // Wait 2s before starting the next wave
            StartCoroutine(WaitForNextWave());
            if (currentWave < 4)
            {
                StartCoroutine(DisplayWave());
                StartCoroutine(WaitForNextWave());
            }


            if (currentWave == 2)
            {
                Debug.Log("Wave 2");
                if (currentRound >= 5)
                {
                    if (currentRound==5)
                    {
                        maxNumberOfBossFirstRound = CommonPropeties.maxNumberOfBossFirstWave;
                    }
                    else maxNumberOfBossFirstRound = (int)Mathf.Round(maxNumberOfBossFirstRound * 1.3f);
                    currentNumberOfBoss = 0;
                }
                maxNumberOfEyesFirstRound = (int)Mathf.Round(maxNumberOfEyesFirstRound * 1.3f);
                currentNumberOfEyes = 0;
                maxNumberOfGoblinFirstRound = (int)Mathf.Round(maxNumberOfGoblinFirstRound * 1.3f);
                currentNumberOfGoblin = 0;
                maxNumberOfMushroomFirstRound = (int)Mathf.Round(maxNumberOfMushroomFirstRound * 1.3f);
                currentNumberOfMushroom = 0;
                maxNumberOfSkeletonFirstRound = (int)Mathf.Round(maxNumberOfSkeletonFirstRound * 1.3f);
                currentNumberOfSkeleton = 0;
                enemiesTotalWave = maxNumberOfBossFirstRound + maxNumberOfEyesFirstRound + maxNumberOfGoblinFirstRound + maxNumberOfMushroomFirstRound + maxNumberOfSkeletonFirstRound;
                isSpawnStart = true;
            }

            if (currentWave == 3)
            {
                Debug.Log("Wave 3");
                if (currentRound >= 5)
                {
                    if (currentRound == 5)
                    {
                        maxNumberOfBossFirstRound = CommonPropeties.maxNumberOfBossFirstWave;
                    }
                    else maxNumberOfBossFirstRound = (int)Mathf.Round(maxNumberOfBossFirstRound * 1.5f);
                    currentNumberOfBoss = 0;
                }
                maxNumberOfEyesFirstRound = (int)Mathf.Round(maxNumberOfEyesFirstRound * 1.5f);
                currentNumberOfEyes = 0;
                maxNumberOfGoblinFirstRound = (int)Mathf.Round(maxNumberOfGoblinFirstRound * 1.5f);
                currentNumberOfGoblin = 0;
                maxNumberOfMushroomFirstRound = (int)Mathf.Round(maxNumberOfMushroomFirstRound * 1.5f);
                currentNumberOfMushroom = 0;
                maxNumberOfSkeletonFirstRound = (int)Mathf.Round(maxNumberOfSkeletonFirstRound * 1.5f);
                currentNumberOfSkeleton = 0;
                Debug.Log(maxNumberOfBossFirstRound + "Wave2");
                enemiesTotalWave = maxNumberOfBossFirstRound + maxNumberOfEyesFirstRound + maxNumberOfGoblinFirstRound + maxNumberOfMushroomFirstRound + maxNumberOfSkeletonFirstRound;
                isSpawnStart = true;
            }

            if (currentWave > maxWavesPerRound)
            {
                // Start the next round
                currentRound++;
                currentWave = 1;

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
                    timeSpawn = timeSpawn * spawnRateReduction;
                    if (currentRound > 5)
                    {
                        maxNumberOfBossFirstRound = (int)Mathf.Round(maxNumberOfBossFirstRound * 1.25f);
                        currentNumberOfBoss = 0;
                    }
                }
                Debug.Log(maxNumberOfBossFirstRound + "Wave3");

                enemiesTotalWave = maxNumberOfBossFirstRound + maxNumberOfEyesFirstRound + maxNumberOfGoblinFirstRound + maxNumberOfMushroomFirstRound + maxNumberOfSkeletonFirstRound;
                // Call a coroutine to wait for 5s before starting the enemy spawning
                StartCoroutine(WaitForHeroesToBeSorted());
                isSpawnStart = true;
            }
        }
    }

    // wait for 2s before starting the next wave
    private IEnumerator WaitForNextWave()
    {
        // Wait for 2s
        yield return new WaitForSeconds(2f);
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
        if (prefabToSpawn != null)
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

        //--
        if (enemies.Count() == 0)
        {
            currentWave++;
            isSpawnStart = false;
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


    // display the round number
    IEnumerator DisplayWave()
    {
        Debug.Log("Hello");
        // Display the wave number
        Notification.text = "Wave " + currentWave;
        Panel.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Panel.gameObject.SetActive(false);
    }

    // display the text of the round and the wave
    IEnumerator DisplayTextAfterDelay()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5);

        // Display the TextMesh of Round
        Panel.gameObject.SetActive(true);
        Notification.text = "Round " + currentRound;

        // Wait for 0.5s
        yield return new WaitForSeconds(2f);

        // Close the TextMesh of Round
        Panel.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(DisplayWave());

        // Wait for 5s before starting the enemy spawning
        yield return new WaitForSeconds(5f);

        // Display Start Spawn
        Panel.gameObject.SetActive(true);
        Notification.text = "Start";

        // Wait for another 0.5s
        yield return new WaitForSeconds(0.5f);

        // Close the TextMesh of Start Spawn
        Panel.gameObject.SetActive(false);

        // Start the enemy spawning
        isSpawnStart = true;
    }
}
