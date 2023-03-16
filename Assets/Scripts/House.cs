using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class House : MonoBehaviour
{
    [SerializeField]
    HealthHouseScript _healthBar;

    private void Update()
    {
        if (_healthBar.GetHealth() == 0)
        {
            SceneManager.LoadScene(2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _healthBar.SetHealth(-10);
        }
    }
}
