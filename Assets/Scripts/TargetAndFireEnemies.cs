using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to the Hero and it is used to target and fire at enemies.
/// </summary>
public class TargetAndFireEnemies : MonoBehaviour
{
    [SerializeField]
    PlayerController _player;
    public float maxRange = 5f;
    public float fireRate = 1f;
    [SerializeField]
    public GameObject weaponPrefabs;

    private float nextFireTime;
    private List<GameObject> targetsInRange = new List<GameObject>();

    private ParticleSystem sleepingEffect;

    // Minh (sound effect)

    private AudioSource aus;

    public AudioClip shootingSound;

    private void Start()
    {
        GameObject body = GameObject.Find("Body");
        // get the ParticleSystem from the Sleeping Effect GameObject
        sleepingEffect = body.transform.Find("SleepingEffect").GetComponent<ParticleSystem>();
        aus = GameObject.FindGameObjectsWithTag("audiosource")[0].GetComponent<AudioSource>();
    }

    //--

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        if (sleepingEffect.isPlaying==false)
        {
            if (targetsInRange.Count > 0 && Time.time >= nextFireTime)
            {
                GameObject target = targetsInRange[0];
                FireAt(target.transform.position);
                nextFireTime = Time.time + fireRate;
            }

        }
        
    }

    private void FireAt(Vector2 targetPosition)
    {

        // Minh (sound effect)

        if (aus && shootingSound)
        {
            aus.PlayOneShot(shootingSound);
        }

        //
        GameObject weapon = Instantiate(weaponPrefabs, transform.position, Quaternion.identity);

        // rotate the arrow to face the target
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // shoot the arrow
        weapon.GetComponent<Rigidbody2D>().velocity = direction * 100f;

        // rotate game object to face the targe if gameobject is archer and wizard
        if (gameObject.tag == "Archer" || gameObject.tag == "Wizard" || gameObject.tag == "Tank")
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        // rotate the gun to face the target if gameobject is cowboy
        if (gameObject.tag == "Cowboy")
        {
            // Get gun gameobject from cowboy
            //GameObject gun = gameObject.transform.Find("Gun").gameObject;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
