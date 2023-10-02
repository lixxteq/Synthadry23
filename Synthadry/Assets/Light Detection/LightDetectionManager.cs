using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightDetectionManager : MonoBehaviour
{
    private bool m_isInRadius = false;

    public float darknessTimer = 0f;

    private void Start()
    {
        m_isInRadius = false;
        darknessTimer = 0f;
    }

    void Update()
    {
        if (!m_isInRadius)
            darknessTimer += Time.deltaTime;
        else if (darknessTimer <= 0f)
            darknessTimer = 0f;
        else
            darknessTimer -= Time.deltaTime;
    }

    public void SetInRadius(bool state)
    {
        m_isInRadius = state;
    }
}
