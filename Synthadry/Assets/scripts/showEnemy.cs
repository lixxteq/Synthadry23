using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;

public class showEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            gameObject.GetComponent<Outlinable>().enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            gameObject.GetComponent<Outlinable>().enabled = false;
        }
    }
}
