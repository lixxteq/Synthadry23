using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{

    [SerializeField] private GameObject MainCanvas;
    [SerializeField] private GameObject PlayerMenu;

    [SerializeField] private GameObject InventoryPage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowMainCanvas()
    {
        MainCanvas.SetActive(true);
        PlayerMenu.SetActive(false);
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMainCanvas();
            InventoryPage.SetActive(false);
        }
    }
}
