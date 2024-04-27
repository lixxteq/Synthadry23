using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuWeaponInformationShowPriceOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI fuel;
    public TextMeshProUGUI cloth;
    public TextMeshProUGUI metal;
    public TextMeshProUGUI plastic;
    public TextMeshProUGUI chemical;
    public TextMeshProUGUI wires;

    public GameObject priceParent;

    public string statName;
    public bool isUpgradeButton;

    public Vector3 priceParentPosition;

    private MenuWeaponSlotManager menuWeaponSlotManager;

    private void Awake()
    {
        menuWeaponSlotManager = GameObject.FindGameObjectWithTag("MenuWeaponSlots").GetComponent<MenuWeaponSlotManager>();
    }

    public void ShowUpgradePrice()
    {
        ResourcesSO resources = menuWeaponSlotManager.GetUpgradePrice(statName);
        DrawUpgradePrice(resources);
    }

    public void ShowDowngradePrice()
    {
        ResourcesSO resources = menuWeaponSlotManager.GetDowngradePrice(statName);
        DrawDowngradePrice(resources);
    }

    void DrawDowngradePrice(ResourcesSO resources)
    {
        if (resources.fuel != 0)
        {
            fuel.text = "+" + resources.fuel.ToString();
            fuel.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            fuel.transform.parent.gameObject.SetActive(false);
        }

        if (resources.cloth != 0)
        {
            cloth.text = "+" + resources.cloth.ToString();
            cloth.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            cloth.transform.parent.gameObject.SetActive(false);
        }

        if (resources.metal != 0)
        {
            metal.text = "+" + resources.metal.ToString();
            metal.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            metal.transform.parent.gameObject.SetActive(false);
        }

        if (resources.plastic != 0)
        {
            plastic.text = "+" + resources.plastic.ToString();
            plastic.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            plastic.transform.parent.gameObject.SetActive(false);
        }

        if (resources.chemical != 0)
        {
            chemical.text = "+" + resources.chemical.ToString();
            chemical.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            chemical.transform.parent.gameObject.SetActive(false);
        }

        if (resources.wires != 0)
        {
            wires.text = "+" + resources.wires.ToString();
            wires.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            wires.transform.parent.gameObject.SetActive(false);
        }
        priceParent.SetActive(true);
    }


    void DrawUpgradePrice(ResourcesSO resources)
    {
        if (resources.fuel != 0)
        {
            fuel.text = "-" + resources.fuel.ToString();
            fuel.transform.parent.gameObject.SetActive(true);
        } else
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
        if (gameObject.activeInHierarchy)
        {
            priceParent.GetComponent<RectTransform>().anchoredPosition = priceParentPosition;
            if (isUpgradeButton)
            {
                ShowUpgradePrice();
            } else
            {
                ShowDowngradePrice();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.activeInHierarchy)
        {
            HideUpgradePrice();
        }
    }
}
