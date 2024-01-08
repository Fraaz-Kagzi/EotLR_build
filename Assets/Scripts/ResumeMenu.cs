using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeMenu : MonoBehaviour
{

    public static bool CheckGame = false;
    public GameObject ResumeMenuCanvas;
    public GameObject Canvas;

    // Update is called once per frame
    void Start()
    {
        ResumeMenuCanvas.SetActive(false);



    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CheckGame == true)
            {
                carryOn();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Stop();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            }
        }

        void carryOn()
        {
            ResumeMenuCanvas.SetActive(false);
            Canvas.SetActive(true);
            Time.timeScale = 1f;
            CheckGame = false;


        }
        void Stop()
        {
            ResumeMenuCanvas.SetActive(true);
            Canvas.SetActive(false);
            Time.timeScale = 0f;
            CheckGame = true;




        }
    }
}
