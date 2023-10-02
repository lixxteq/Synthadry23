using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateHeadFlashlight : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (flashlight.activeSelf == false)
            {
                flashlight.SetActive(true);
            } else
            {
                flashlight.SetActive(false);
            }
        }
    }
}
