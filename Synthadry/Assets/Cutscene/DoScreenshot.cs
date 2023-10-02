using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class DoScreenshot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ScreenCapture.CaptureScreenshot("screenshot_" + SceneManager.GetActiveScene().name + "_" + DateTime.Now.ToString("HH_mm_ss") + ".png");
            Debug.Log("A screenshot was taken!");
        }
    }
}
