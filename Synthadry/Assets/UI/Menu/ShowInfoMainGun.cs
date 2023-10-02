using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class ShowInfoMainGun : MonoBehaviour
{
    [SerializeField] private InventorySystem PlayerInventory;

    [SerializeField] private Image DamageLine;
    [SerializeField] private Image RateOfFireLine;
    [SerializeField] private TextMeshProUGUI AllAmmo;
    [SerializeField] private TextMeshProUGUI CurrentAmmo;
    public int GunNum = 0;

    public void UpdateInfo()
    {
        DamageLine.fillAmount = Convert.ToSingle(PlayerInventory.mainGuns[GunNum].GetComponent<ItemObject>().damage / 100); ;
        RateOfFireLine.fillAmount = Convert.ToSingle(PlayerInventory.mainGuns[GunNum].GetComponent<ItemObject>().rateOfFire / 100); ;
        /*if (PlayerInventory.mainGuns[GunNum].GetComponent<ItemObject>().itemStat.typeOfMissile.ToString() is "hand")
        {
            AllAmmo.text = "/∞";
            CurrentAmmo.text = "∞";
        }
        else
        {
            AllAmmo.text = "/" + PlayerInventory.mainGuns[GunNum].GetComponent<ItemObject>().allAmmo.ToString();
            CurrentAmmo.text = PlayerInventory.mainGuns[GunNum].GetComponent<ItemObject>().currentAmmo.ToString();
        }*/
    }

    private void OnEnable()
    {
        UpdateInfo();
    }
}
