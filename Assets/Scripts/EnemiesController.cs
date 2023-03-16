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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        Debug.Log("Enemy takes " + damage + " damage");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
