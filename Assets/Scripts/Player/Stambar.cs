using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stambar : MonoBehaviour
{
  

    [SerializeField] private Slider stamSlider;

   
   

    public void UpdateStamBar(float stam, float maxStam)
    {
        stamSlider.value = stam;
    }

    private void Update()
    {
        
    }

    public void SetStam(float stam, float maxStam)
    {
        stamSlider.value = stam;
        stamSlider.maxValue = maxStam;
    }

    public void resetStam()
    {
        stamSlider.value = stamSlider.maxValue;
    }

    
}

