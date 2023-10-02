using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStationController : MonoBehaviour
{

    [SerializeField] private GameObject InventoryButton;
    [SerializeField] private GameObject UpgradeButton;

    [SerializeField] private PageController pageController;

    [SerializeField] private GameObject PlayerMainCanvas;
    [SerializeField] private GameObject PlayerMenuCanvas;



    private bool CanOpenMenuF = false;


    // Start is called before the first frame update
    void Start()
    {
        InventoryButton.SetActive(false);
        UpgradeButton.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("123123123123");
        if (other.tag == "Player")
        {
            CanOpenMenuF = true;
            InventoryButton.SetActive(true);
            UpgradeButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanOpenMenuF = false;
            InventoryButton.SetActive(false);
            UpgradeButton.SetActive(false);
        }
    }

    private void Update()
    {
        if (CanOpenMenuF)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PlayerMainCanvas.SetActive(false);
                PlayerMenuCanvas.SetActive(true);
                pageController.LoadPage(2);
            }
        }
    }
}
