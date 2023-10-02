using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraController : MonoBehaviour
{
    [SerializeField] private Lantern lantern;
    [SerializeField] private Aim aim;
    [SerializeField] private Damage damage;
    [SerializeField] private RateOfFire rate;
    [SerializeField] private Ammo ammo;

    private void OnEnable()
    {
        lantern.CheckLantern();
        aim.CheckAim();
        damage.CheckUprageDamage();
        rate.CheckUprageRateOfFire();
        ammo.CheckUprageLevelAmmo();
    }
}
