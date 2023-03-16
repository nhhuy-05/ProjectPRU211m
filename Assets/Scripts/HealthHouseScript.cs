using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHouseScript : MonoBehaviour
{
    Slider _healthSlider;
    float currentHealth;

    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
        currentHealth = _healthSlider.value;
    }
    public void SetHealth(int minusHealth)
    {
        currentHealth += minusHealth;
        _healthSlider.value = currentHealth;
    }
    public float GetHealth()
    {
        return currentHealth;
    }
}
