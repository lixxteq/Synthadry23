using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BeaconBeam : MonoBehaviour
{
    public Transform beamStartPos;
    public Vector2[] points;
    public Color wireColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Debug.Log(mousePos);

    }

}
