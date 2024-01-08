using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumecontroller : MonoBehaviour
{
    [SerializeField] Slider volumecontrol;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("BGVOLUME"))
        {
            PlayerPrefs.SetFloat("BGVOLUME",1);
            Get();
        }
        else
        {
            Get();
        }
    }

    // Update is called once per frame
    public void updateVolume()
    {
        AudioListener.volume = volumecontrol.value;
        Set();
    }

    public void Get()
    {
        volumecontrol.value = PlayerPrefs.GetFloat("BGVOLUME");
    }

    public void Set()
    {
        PlayerPrefs.SetFloat("BGVOLUME",volumecontrol.value);
    }

}
