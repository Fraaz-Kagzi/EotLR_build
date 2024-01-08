using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this for TextMeshPro functionality


public class EnemyCounterUI : MonoBehaviour
{
    public EnemySpawner spawner;
    public TextMeshProUGUI enemyCountText;

    private void Start()
    {
        int remainingEnemies = 5; // Change this to the actual total number of enemies.
        UpdateEnemyCount(remainingEnemies);
    }


    

    public void UpdateEnemyCount(int enemies)
    {
        if (enemies <= 0) { enemyCountText.text = ""; }
        // Update the UI text with the remaining enemy count.
        enemyCountText.text = "Enemies Remaining: " + enemies;

        Debug.Log("Updated Enemy Count: " + enemies);
    }

}
