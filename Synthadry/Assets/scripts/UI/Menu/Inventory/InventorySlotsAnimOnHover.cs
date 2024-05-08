using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotsAnimOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public TextMeshProUGUI fuel;
    public TextMeshProUGUI cloth;
    public TextMeshProUGUI metal;
    public TextMeshProUGUI plastic;
    public TextMeshProUGUI chemical;
    public TextMeshProUGUI wires;

    public GameObject priceParent;

    public string objectName;

    public Vector3 priceParentPosition;

    public Vector3 openRotation;
    public Vector3 closedRotation;

    public float duration;

    public GameObject current;
    public GameObject maximum;

    public string slotName;

    private InventorySystem inventorySystem;
    private MenuInventorySlotManager menuInventorySlotManager;


    private void Awake()
    {
        inventorySystem = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
        menuInventorySlotManager = GameObject.FindGameObjectWithTag("MenuInventorySlots").GetComponent<MenuInventorySlotManager>();

    }

    void FillPetals()
    {
        TextMeshProUGUI currentText = current.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI maximumText = maximum.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        switch (slotName)
        {
            case "hp":
                Debug.Log(maximumText.text);
                currentText.text = inventorySystem.hpBuffs.Count.ToString();
                maximumText.text = inventorySystem.maximumBaffs.ToString();
                break;
            case "armor":
                currentText.text = inventorySystem.armorBuffs.Count.ToString();
                maximumText.text = inventorySystem.maximumBaffs.ToString();

                break;
            case "speed":
                currentText.text = inventorySystem.speedBuffs.Count.ToString();
                maximumText.text = inventorySystem.maximumBaffs.ToString();

                break;

            default: break;
        };
    }



    void ShowPetals()
    {
        current.SetActive(true);
        maximum.SetActive(true);
    }

    void HidePetals()
    {
        current.SetActive(false);
        maximum.SetActive(false);
    }

    IEnumerator LerpFunction(Vector3 endValue, float duration)
    {
        if (endValue.z == 0)
        {
            HidePetals();
        }
        RectTransform rectTransform = GetComponent<RectTransform>();
        
        float time = 0;
        
        Quaternion startValue = rectTransform.rotation;

        while (time < duration)
        {
            rectTransform.rotation = Quaternion.Lerp(startValue, Quaternion.Euler(endValue), time / duration);
            time += 0.2f;
            yield return null;
        }
        rectTransform.rotation = Quaternion.Euler(endValue);
        if (endValue.z != 0)
        {
            FillPetals();
            ShowPetals();
        }
    }
    public void ShowUpgradePrice()
    {
        DrawUpgradePrice(menuInventorySlotManager.GetPrice(objectName));
    }

    void DrawUpgradePrice(ResourcesSO resources)
    {
        Debug.Log(resources);
        if (resources.fuel != 0)
        {
            fuel.text = "-" + resources.fuel.ToString();
            fuel.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            fuel.transform.parent.gameObject.SetActive(false);
        }

        if (resources.cloth != 0)
        {
            cloth.text = "-" + resources.cloth.ToString();
            cloth.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            cloth.transform.parent.gameObject.SetActive(false);
        }

        if (resources.metal != 0)
        {
            metal.text = "-" + resources.metal.ToString();
            metal.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            metal.transform.parent.gameObject.SetActive(false);
        }

        if (resources.plastic != 0)
        {
            plastic.text = "-" + resources.plastic.ToString();
            plastic.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            plastic.transform.parent.gameObject.SetActive(false);
        }

        if (resources.chemical != 0)
        {
            chemical.text = "-" + resources.chemical.ToString();
            chemical.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            chemical.transform.parent.gameObject.SetActive(false);
        }

        if (resources.wires != 0)
        {
            wires.text = "-" + resources.wires.ToString();
            wires.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            wires.transform.parent.gameObject.SetActive(false);
        }
        priceParent.SetActive(true);
    }

    void HideUpgradePrice()
    {
        priceParent.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(LerpFunction(openRotation, duration));
        if (gameObject.activeInHierarchy)
        {
            priceParent.GetComponent<RectTransform>().anchoredPosition = priceParentPosition;
            ShowUpgradePrice();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(LerpFunction(closedRotation, duration));
        if (gameObject.activeInHierarchy)
        {
            HideUpgradePrice();
        }
    }
}