using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTarget : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private float damage = 5;

    public void Kill()
    {
        Destroy(this);
    }

    public void GiveDamage(float givenDamage)
    {
        if (health - givenDamage <= 0)
        {
            Kill();
        }
        else
        {
            health -= givenDamage;
        }
    }
}
