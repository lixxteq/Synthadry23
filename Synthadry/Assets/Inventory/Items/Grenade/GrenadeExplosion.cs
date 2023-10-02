using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    [SerializeField] private float delayBeforeExplosion = 3f;

    [SerializeField] private float explosionRadius = 5f;

    [SerializeField] private float explosionForce = 700f;


    [SerializeField] private GameObject explosionEffect;
    public bool canExplode = false;

    float countdown;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delayBeforeExplosion;
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canExplode)
        {
            countdown -= Time.deltaTime;

            if (countdown <= 0f)
            {
                Explode();
                Destroy(gameObject);
            }
        }
    }
}
