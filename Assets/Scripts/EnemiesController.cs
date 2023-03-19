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
    public float moveUnitsPerSecond = 1f;

    // health support
    public int maxHealth = 100;
    public int currentEnemyHealth;
    public int currentVillageHouse;

    // health bar of enemies
    public HealthBarContainer healthEnemyBar;
    // health bar of village
    //public HealthBarContainer healthVillageBar;


    // Start is called before the first frame update
    void Start()
    {
        // set the health
        currentEnemyHealth = maxHealth;
        currentVillageHouse = CommonPropeties.healthOfVillage;
        
        healthEnemyBar.SetMaxHealth(maxHealth);
        //healthVillageBar.SetMaxHealth(currentVillageHouse);

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
        if (collision.gameObject.tag == "Arrow" || collision.gameObject.tag == "Bullet")
        {
            TakeDamage(20);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "RoundShot")
        {
            TakeDamage(50);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "MagicBall")
        {
            // make enemy move slower a half 
            MakeSlower();
            Destroy(collision.gameObject);
        }
        // check heath and destroy enemies
        if (currentEnemyHealth <= 0)
        {
            if (gameObject.tag == "Boss")
            {
                CommonPropeties.coin += CommonPropeties.coinOfBoss;
            }
            if (gameObject.tag == "Eyes")
            {
                CommonPropeties.coin += CommonPropeties.coinOfEyes;
            }
            if (gameObject.tag == "Goblin")
            {
                CommonPropeties.coin += CommonPropeties.coinOfGoblin;

            }
            if (gameObject.tag == "MushRoom")
            {
                CommonPropeties.coin += CommonPropeties.coinOfMushroom;

            }
            if (gameObject.tag == "Skeleton")
            {
                CommonPropeties.coin += CommonPropeties.coinOfSkeleton;
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
                    CommonPropeties.healthOfVillage = 100;
                    CommonPropeties.coin = 300;
                    SceneManager.LoadScene(2);
                }
                return;
            }
            // move to the next waypoint
            pathIndex++;
        }
    }
}
