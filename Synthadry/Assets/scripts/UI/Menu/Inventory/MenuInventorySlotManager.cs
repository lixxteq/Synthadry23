using UnityEngine;

public class MenuInventorySlotManager : MonoBehaviour
{
    private InventorySystem inventorySystem;

    private void Awake()
    {
        inventorySystem = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
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
        }
        return null;
    }
}
