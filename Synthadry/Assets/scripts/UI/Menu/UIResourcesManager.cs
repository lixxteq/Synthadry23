using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIResourcesManager : MonoBehaviour
{
    public TextMeshProUGUI fuelValue;
    public TextMeshProUGUI clothValue;
    public TextMeshProUGUI metalValue;
    public TextMeshProUGUI plasticValue;
    public TextMeshProUGUI chemicalValue;
    public TextMeshProUGUI wiresValue;

    private ResourcesIneractManager resourcesIneractManager;

    // Start is called before the first frame update
    void Start()
    {
        resourcesIneractManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ResourcesIneractManager>();
    }

    private void OnEnable()
    {
        UiUpdateResources();
    }

    public void UiUpdateResources()
    {
        var resources = new Dictionary<string, int>();

        resources = resourcesIneractManager.GetResources();

        fuelValue.text = resources["fuel"].ToString();
        clothValue.text = resources["cloth"].ToString();
        metalValue.text = resources["metal"].ToString();
        plasticValue.text = resources["plastic"].ToString();
        chemicalValue.text = resources["chemical"].ToString();
        wiresValue.text = resources["wires"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
