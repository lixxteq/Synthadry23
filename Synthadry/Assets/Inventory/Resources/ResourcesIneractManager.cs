using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ResourcesIneractManager : MonoBehaviour
{
    private InventorySystem inventorySystem;

    // Start is called before the first frame update
    void Start()
    {
            inventorySystem = gameObject.GetComponent<InventorySystem>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<string, int> GetResources()
    {
        var resources = new Dictionary<string, int>()
        {
             {"fuel", inventorySystem.fuel },
            { "cloth", inventorySystem.cloth },
            { "metal", inventorySystem.metal },
            { "plastic", inventorySystem.plastic },
            { "chemical", inventorySystem.chemical },
            { "wires", inventorySystem.wires },
        };
        return resources;
    }

    public void DecreaseResources(ResourcesSO resourcesPrice)
    {
        inventorySystem.fuel -= resourcesPrice.fuel;
        inventorySystem.cloth -= resourcesPrice.cloth;
        inventorySystem.metal -= resourcesPrice.metal;
        inventorySystem.plastic -= resourcesPrice.plastic;
        inventorySystem.chemical -= resourcesPrice.chemical;
        inventorySystem.wires -= resourcesPrice.wires;
    }

    public void IncreaseResources(ResourcesSO resourcesPrice)
    {
        inventorySystem.fuel += resourcesPrice.fuel;
        inventorySystem.cloth += resourcesPrice.cloth;
        inventorySystem.metal += resourcesPrice.metal;
        inventorySystem.plastic += resourcesPrice.plastic;
        inventorySystem.chemical += resourcesPrice.chemical;
        inventorySystem.wires += resourcesPrice.wires;
    }

    public bool CheckResources(ResourcesSO resourcesPrice)
    {
        if (inventorySystem.fuel - resourcesPrice.fuel < 0)
        {
            return false;
        }

        if (inventorySystem.cloth - resourcesPrice.cloth < 0)
        {
            return false;
        }

        if (inventorySystem.metal - resourcesPrice.metal < 0)
        {
            return false;
        }

        if (inventorySystem.plastic - resourcesPrice.plastic < 0)
        {
            return false;
        }

        if (inventorySystem.chemical - resourcesPrice.chemical < 0)
        {
            return false;
        }

        if (inventorySystem.wires - resourcesPrice.wires < 0)
        {
            return false;
        }

        return true;
    }
}
