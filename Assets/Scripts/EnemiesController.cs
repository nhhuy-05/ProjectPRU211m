using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBarContainer healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow" || collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "MagicBall" || collision.gameObject.tag == "RoundShot")
        {
            TakeDamage(20);
            Destroy(collision.gameObject);
            // Detroy enemy if health is 0
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void TakeDamage(int damage)
    {
        Debug.Log("Enemy takes " + damage + " damage");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
