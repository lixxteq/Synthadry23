using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour
{
    public Transform target;

    [SerializeField] private Vector3 _offset = new Vector3(0f, 2f, -4f);
    [SerializeField] private float _speed = 10f;

    private void FixedUpdate()
    {
        var targetPosition = target.TransformPoint(_offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);

        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speed * Time.deltaTime);
    }
}
