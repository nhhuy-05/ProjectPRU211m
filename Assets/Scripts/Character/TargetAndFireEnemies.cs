using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to the Hero and it is used to target and fire at enemies.
/// </summary>
public class TargetAndFireEnemies : MonoBehaviour
{
    [SerializeField]
    public GameObject weaponPrefabs;
    public AudioClip shootingSound;

    private float fireRate;
    private float nextFireTime;
    private List<GameObject> targetsInRange = new List<GameObject>();
    private ParticleSystem sleepingEffect;
    private AudioSource aus;

    private void Start()
    {
        // set the fire rate
        if (gameObject.tag == "Archer")
        {
            fireRate = CommonPropeties.fireRateOfArcher;
        }
        if (gameObject.tag == "Cowboy")
        {
            fireRate = CommonPropeties.fireRateOfCowboy;
        }
        if (gameObject.tag == "Tank")
        {
            fireRate = CommonPropeties.fireRateOfTank;
        }
        if (gameObject.tag == "Wizard")
        {
            fireRate = CommonPropeties.fireRateOfWizard;
        }
        sleepingEffect = transform.Find("SleepingEffect").GetComponent<ParticleSystem>();
        aus = GameObject.FindGameObjectsWithTag("audiosource")[0].GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // just cowboy and archer can shoot all enemies, another hero can only shoot Boss, Goblin, Skeleton and Mushroom
        if (gameObject.tag == "Archer" || gameObject.tag == "Cowboy")
        {
            if (other.CompareTag("Boss") || other.CompareTag("Eyes") || other.CompareTag("Goblin") || other.CompareTag("Mushroom") || other.CompareTag("Skeleton"))
            {
                targetsInRange.Add(other.gameObject);
            }
        }
        if (gameObject.tag == "Tank" || gameObject.tag == "Wizard")
        {
            if (other.CompareTag("Boss") || other.CompareTag("Goblin") || other.CompareTag("Mushroom") || other.CompareTag("Skeleton"))
            {
                targetsInRange.Add(other.gameObject);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.tag == "Archer" || gameObject.tag == "Cowboy")
        {
            if (other.CompareTag("Boss") || other.CompareTag("Eyes") || other.CompareTag("Goblin") || other.CompareTag("Mushroom") || other.CompareTag("Skeleton"))
            {
                targetsInRange.Remove(other.gameObject);
            }
        }
        if (gameObject.tag == "Tank" || gameObject.tag == "Wizard")
        {
            if (other.CompareTag("Boss") || other.CompareTag("Goblin") || other.CompareTag("Mushroom") || other.CompareTag("Skeleton"))
            {
                targetsInRange.Remove(other.gameObject);
            }
        }
    }

    private void Update()
    {
        if (sleepingEffect.isPlaying == false)
        {
            if (targetsInRange.Count > 0 && Time.time >= nextFireTime)
            {
                GameObject target = targetsInRange[0];
                if (target != null)
                {
                    FireAt(target.transform.position);
                }
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void FireAt(Vector2 targetPosition)
    {
        // instantiate weapon prefab
        GameObject weapon = Instantiate(weaponPrefabs, transform.position, Quaternion.identity);

        // rotate the weapon to face the target
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // shoot the weapon
        //if ( gameObject.tag == "Tank")
        //{
        //    // TODO: shoot follow rainbow line
        //    // calculate the rainbow direction by rotating the original direction vector
        //    Vector3 rainbowDirection = Quaternion.AngleAxis(Time.time * 50f, Vector3.forward) * direction;

        //    // calculate the rainbow displacement vector using a sine wave formula
        //    float rainbowFrequency = 2f;
        //    float rainbowAmplitude = 0.5f;
        //    float rainbowOffset = Time.time * rainbowFrequency;
        //    Vector3 rainbowDisplacement = rainbowAmplitude * Mathf.Sin(rainbowOffset) * Vector3.up;

        //    // add the rainbow displacement vector to the original direction vector to get the final direction vector
        //    Vector3 finalDirection = direction;
        //    finalDirection += (Vector3)rainbowDirection;
        //    finalDirection += rainbowDisplacement;

        //    // set the velocity of the weapon to the final direction vector
        //    weapon.GetComponent<Rigidbody2D>().velocity = finalDirection.normalized * 10f;
        //}
        if (gameObject.tag == "Wizard" || gameObject.tag == "Archer" || gameObject.tag == "Cowboy" || gameObject.tag == "Tank")
        {
            weapon.GetComponent<Rigidbody2D>().velocity = direction * 100f;
        }

        // play shooting sound
        if (aus && shootingSound)
        {
            aus.PlayOneShot(shootingSound);
        }

        // rotate game object to face the targe if gameobject is archer and wizard
        if (gameObject.tag == "Archer" || gameObject.tag == "Wizard" || gameObject.tag == "Tank")
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (gameObject.tag == "Cowboy")
        {
            // Get game object child 1 of game object
            GameObject child1 = gameObject.transform.GetChild(1).gameObject;

            // Get game object "Gun" of child 1 of game object
            GameObject gun = child1.transform.Find("Gun").gameObject;

            // Rotate the gun to face the target
            gun.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        }
    }
}
