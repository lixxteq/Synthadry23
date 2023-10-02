using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;

public class ShowOutline : MonoBehaviour
{

    [SerializeField] private GameObject thing;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            thing.GetComponent<Outlinable>().enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            thing.GetComponent<Outlinable>().enabled = false;
        }
    }
}
