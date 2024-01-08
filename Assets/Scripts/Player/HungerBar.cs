using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{


    [SerializeField] private Slider hungerSlider;
    



    public void UpdateBar(float hunger, float maxStam)
    {
        hungerSlider.value = hunger;
        
    }

    private void Update()
    {

    }

    public void SetHunger(float hunger, float maxHunger)
    {
        hungerSlider.value = hunger;
        hungerSlider.maxValue = maxHunger;
    }

    public void resetHunger()
    {
        hungerSlider.value = hungerSlider.maxValue;
    }



}

