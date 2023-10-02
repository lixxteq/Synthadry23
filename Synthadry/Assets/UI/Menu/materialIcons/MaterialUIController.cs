using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MaterialUIController : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> UiMaterialCounter;
    [SerializeField] private InventorySystem PlayerInventory;

    public void UpdateMaterialsUI()
    {
        UiMaterialCounter[0].text = PlayerInventory.fuel.ToString();
        UiMaterialCounter[1].text = PlayerInventory.cloth.ToString();
        UiMaterialCounter[2].text = PlayerInventory.metal.ToString();
        UiMaterialCounter[3].text = PlayerInventory.plastic.ToString();
        UiMaterialCounter[4].text = PlayerInventory.chemical.ToString();
        UiMaterialCounter[5].text = PlayerInventory.wires.ToString();
    }

    private void OnEnable()
    {
        UpdateMaterialsUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
