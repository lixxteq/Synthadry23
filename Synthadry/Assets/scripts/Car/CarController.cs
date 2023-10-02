using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float motorForce;
    public float brakeForce;
    public float maxSteerAngle;
    public float rotationSpeed;

    private Rigidbody rb;
    private WheelCollider[] wheelColliders;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        wheelColliders = GetComponentsInChildren<WheelCollider>();
    }

    private void FixedUpdate()
    {
        float motor = motorForce * Input.GetAxis("Vertical");
        float brake = Input.GetKey(KeyCode.Space) ? brakeForce : 0f;
        float steer = maxSteerAngle * Input.GetAxis("Horizontal");

        // Вращение машины
        float rotation = rotationSpeed * steer * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotation, 0));

        foreach (WheelCollider wheel in wheelColliders)
        {
            if (Input.GetKey(KeyCode.Space))
                wheel.brakeTorque = brake;
            else
            {
                wheel.brakeTorque = 0f;
                wheel.motorTorque = motor;
            }
        }
    }
}