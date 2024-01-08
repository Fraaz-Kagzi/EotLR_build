using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
   

    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthSlider.value = health;
    }

    private void Update()
    {
        
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth;
        healthSlider.maxValue = maxHealth;
    }
}
