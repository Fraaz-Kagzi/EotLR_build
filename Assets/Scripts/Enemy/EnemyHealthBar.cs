using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    public Transform enemyTransform;
    public Camera camera;

    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthSlider.value = health;
    }

    private void Update()
    {
        if (enemyTransform != null)
        {
            transform.rotation = camera.transform.rotation;
        }
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth;
        healthSlider.maxValue = maxHealth;
    }
}
