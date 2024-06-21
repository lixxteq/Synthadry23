using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MenuInventorySlotManager : MonoBehaviour
{
    private InventorySystem inventorySystem;

    private void Awake()
    {
        inventorySystem = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
    }

    ResourcesSO FindWeapon(string title)
    {
        List<GameObject> weapons = inventorySystem.mainGuns;

        foreach (GameObject weapon in weapons)
        {
            ItemObject itemObject = weapon.GetComponent<ItemObject>();
            if (itemObject.itemStat.type is ItemSO.Type.firearms)
            {
                if (itemObject.itemStat.name.ToString() == title)
                {
                   return itemObject.createAmmoPrice;
                }
            }
            else
            {
                return null;
            }
        }
        return null;
    }

    public ResourcesSO GetPrice(string title)
    {
        switch (title)
        {
            case "hp":
                return inventorySystem.hpPrice;
            case "armor":
                return inventorySystem.armorPrice;
            case "speed":
                return inventorySystem.speedPrice;
            case "ak":
                return FindWeapon("ak");
            case "revolver":
                return FindWeapon("revolver");
        }
        return null;
    }
}
