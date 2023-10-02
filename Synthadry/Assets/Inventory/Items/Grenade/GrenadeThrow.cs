using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{

    [SerializeField] private float throwForce = 40f;

    public void ThrowGrenade(GameObject grenade)
    {

        GameObject tempGrenade = Instantiate(grenade, transform.position, transform.rotation);
        tempGrenade.GetComponent<GrenadeExplosion>().canExplode = true;
        Rigidbody rb = tempGrenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce);
        tempGrenade.SetActive(true);
    }
}
