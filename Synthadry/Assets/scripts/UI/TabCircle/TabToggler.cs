using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabToggler : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject tabUI;

    private float fixedDeltaTime;

    private CameraController cameraController;
    private float oldSensitivityX;
    private float oldSensitivityY;


    private void Awake()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Start()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        oldSensitivityX = cameraController.sensitivityX;
        oldSensitivityY = cameraController.sensitivityY;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            mainUI.SetActive(false);
            tabUI.SetActive(true);
            Time.timeScale = 0.05f;
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
            cameraController.sensitivityX = 0.002f;
            cameraController.sensitivityY = 0.002f;
            Cursor.lockState = CursorLockMode.Confined;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            mainUI.SetActive(true);

            tabUI.SetActive(false);
            Time.timeScale = 1f;
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
            cameraController.sensitivityX = oldSensitivityX;
            cameraController.sensitivityY = oldSensitivityY;
            Cursor.lockState = CursorLockMode.Locked;


        }
    }
}
