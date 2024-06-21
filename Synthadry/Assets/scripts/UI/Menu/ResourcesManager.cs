using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public TextMeshProUGUI fuel;
    public TextMeshProUGUI cloth;
    public TextMeshProUGUI metal;
    public TextMeshProUGUI plastic;
    public TextMeshProUGUI chemical;
    public TextMeshProUGUI wires;

    private InventorySystem inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        UpdateResources();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
    }

    public void UpdateResources()
    {
        fuel.text = inventory.fuel.ToString();
        cloth.text = inventory.cloth.ToString();
        metal.text = inventory.metal.ToString();
        plastic.text = inventory.plastic.ToString();
        chemical.text = inventory.chemical.ToString();
        wires.text = inventory.wires.ToString();
    }
}
