using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    private Light m_light;
    private SphereCollider m_sphereCollider;
    private LightDetectionManager m_lightDetectionManager;

    void Start()
    {
        m_light = GetComponent<Light>();
        m_sphereCollider = GetComponent<SphereCollider>();
        m_sphereCollider.radius = m_light.range;
        m_sphereCollider.isTrigger = true;
        m_lightDetectionManager = FindObjectOfType(typeof(LightDetectionManager)) as LightDetectionManager;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            m_lightDetectionManager.SetInRadius(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            m_lightDetectionManager.SetInRadius(false);
    }
}

