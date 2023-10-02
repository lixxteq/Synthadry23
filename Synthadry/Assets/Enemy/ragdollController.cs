using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdollController : MonoBehaviour
{

    public Animator anim;
    public Rigidbody playerRb;
    public CapsuleCollider capsuleCollider;

    private Rigidbody[] rigidbodies;
    private Collider[] colliders;

    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }

    void SetCollidersEnabled(bool enabled)
    {
        foreach (Collider col in colliders)
        {
            col.enabled = enabled;
        }
    }

    void SetRigidbodyesKinematic(bool kinematic)
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = kinematic;
        }
    }

    public void ActivateRagdoll()
    {
        capsuleCollider.enabled = true;
        playerRb.isKinematic = true;
        anim.enabled = false;

        SetCollidersEnabled(true);
        SetRigidbodyesKinematic(false);
    }
}
