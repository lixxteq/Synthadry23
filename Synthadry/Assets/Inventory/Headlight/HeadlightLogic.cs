using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadlightLogic : MonoBehaviour
{
    private InventorySystem inventory;
    public GameObject headlightLight;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) 
        {
            if (inventory.haveHeadlight)
            {
                if (headlightLight.activeInHierarchy)
                {
                    headlightLight.SetActive(false);
                } else
                {
                    headlightLight.SetActive(true);
                }
            }
        }
    }
}
