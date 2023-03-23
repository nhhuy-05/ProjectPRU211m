using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemiesController : MonoBehaviour
{
    // define the path
    [SerializeField]
    Transform[] paths;

    // move support
    int randomPath;
    int pathIndex = 0;
    private float moveUnitsPerSecond;

    // health support
    private int maxHealth;
    private int currentEnemyHealth;

    // health bar of enemies
    public HealthBarContainer healthEnemyBar;

    // Start is called before the first frame update
    void Start()
    {
        // set max health and moveUnitsPerSecond of each enemy
        if (gameObject.tag == "Boss")
        {
            maxHealth = CommonPropeties.healthOfBoss;
            moveUnitsPerSecond = CommonPropeties.speedOfBoss;
        }
        if (gameObject.tag == "Eyes")
        {
            maxHealth = CommonPropeties.healthOfEyes;
            moveUnitsPerSecond = CommonPropeties.speedOfEyes;
        }
        if (gameObject.tag == "Goblin")
        {
            maxHealth = CommonPropeties.healthOfGoblin;
            moveUnitsPerSecond = CommonPropeties.speedOfGoblin;
        }
        if (gameObject.tag == "Mushroom")
        {
            maxHealth = CommonPropeties.healthOfMushroom;
            moveUnitsPerSecond = CommonPropeties.speedOfMushroom;
        }
        if (gameObject.tag == "Skeleton")
        {
            maxHealth = CommonPropeties.healthOfSkeleton;
            moveUnitsPerSecond = CommonPropeties.speedOfSkeleton;
        }
        // set the health
        currentEnemyHealth = maxHealth;
        healthEnemyBar.SetMaxHealth(maxHealth);

        // set the path
        randomPath = Random.Range(0, paths.Length);
        transform.position = paths[randomPath].GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            TakeDamage(CommonPropeties.damageOfArrow);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(CommonPropeties.damageOfBullet);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "RoundShot")
        {
            TakeDamage(CommonPropeties.damageOfRoundShot);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "MagicBall")
        {
            // make enemy move slower a half 
            TakeDamage(CommonPropeties.damageOfMagicBall);
            MakeSlower();
            Destroy(collision.gameObject);
        }
        // check heath and destroy enemies
        if (currentEnemyHealth <= 0)
        {
            if (gameObject.tag == "Boss")
            {
                CommonPropeties.coin += CommonPropeties.coinOfBoss;
                CommonPropeties.currentScore += CommonPropeties.scoreOfBoss;
            }
            if (gameObject.tag == "Eyes")
            {
                CommonPropeties.coin += CommonPropeties.coinOfEyes;
                CommonPropeties.currentScore += CommonPropeties.scoreOfEyes;
            }
            if (gameObject.tag == "Goblin")
            {
                CommonPropeties.coin += CommonPropeties.coinOfGoblin;
                CommonPropeties.currentScore += CommonPropeties.scoreOfGoblin;

            }
            if (gameObject.tag == "MushRoom")
            {
                CommonPropeties.coin += CommonPropeties.coinOfMushroom;
                CommonPropeties.currentScore += CommonPropeties.scoreOfMushroom;

            }
            if (gameObject.tag == "Skeleton")
            {
                CommonPropeties.coin += CommonPropeties.coinOfSkeleton;
                CommonPropeties.currentScore += CommonPropeties.scoreOfSkeleton;
            }
            Destroy(gameObject);
        }
    }

    // make enemy move slower a half in 2s
    private void MakeSlower()
    {
        moveUnitsPerSecond = moveUnitsPerSecond / 2;
        StartCoroutine(ResetSpeed());
    }

    // reset the speed after 2s
    private IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(2f);
        moveUnitsPerSecond = moveUnitsPerSecond * 2;
    }

    // take damage for enemy
    void TakeDamage(int damage)
    {
        currentEnemyHealth -= damage;
        healthEnemyBar.SetHealth(currentEnemyHealth);
    }

    // move enemies follow the path
    private void Move()
    {
        // move enemies to the next waypoint
        transform.position = Vector3.MoveTowards(transform.position, paths[randomPath].GetChild(pathIndex).position, moveUnitsPerSecond * Time.deltaTime);
        // if enemies reaches the waypoint
        if (transform.position == paths[randomPath].GetChild(pathIndex).position)
        {
            // if enemies reaches the last waypoint
            if (pathIndex == paths[randomPath].childCount - 1)
            {
                // destroy enemies
                Destroy(gameObject);

                // reduce the health of village base on the damage of enemies
                if (gameObject.tag == "Boss")
                {
                    CommonPropeties.healthOfVillage -= CommonPropeties.damageOfBoss;
                }
                if (gameObject.tag == "Eyes")
                {
                    CommonPropeties.healthOfVillage -= CommonPropeties.damageOfEyes;
                }
                if (gameObject.tag == "Goblin")
                {
                    CommonPropeties.healthOfVillage -= CommonPropeties.damageOfGoblin;
                }
                if (gameObject.tag == "MushRoom")
                {
                    CommonPropeties.healthOfVillage -= CommonPropeties.damageOfMushroom;
                }
                if (gameObject.tag == "Skeleton")
                {
                    CommonPropeties.healthOfVillage -= CommonPropeties.damageOfSkeleton;
                }
                // load losing scene if village health is 0
                if (CommonPropeties.healthOfVillage <= 0)
                {
                    CommonPropeties.score = CommonPropeties.currentScore;
                    // Reset Json file
                    SaveLoad.ClearDataSaveFile();
                    SaveLoad.ClearHeroesSaveFile();
                                       
                    // Save the score
                    SaveLoad.SaveScore(CommonPropeties.currentScore);
                    
                    // Reset some common properties
                    CommonPropeties.healthOfVillage = 100;
                    CommonPropeties.currentScore = 0;
                    CommonPropeties.currentRound = 1;
                    CommonPropeties.currentWave = 1;
                    CommonPropeties.coin = 1000;
                    SceneManager.LoadScene(2);
                }
                return;
            }
            // move to the next waypoint
            pathIndex++;
        }
    }
}
