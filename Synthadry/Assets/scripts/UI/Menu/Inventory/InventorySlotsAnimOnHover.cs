using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    private ResourcesIneractManager resourcesIneractManager;

    public GameObject lockedImage;
    
    private ItemObject itemObject = null;


    private void Awake()
    {
        inventorySystem = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
        menuInventorySlotManager = GameObject.FindGameObjectWithTag("MenuInventorySlots").GetComponent<MenuInventorySlotManager>();
        resourcesIneractManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ResourcesIneractManager>();
    }

    public void CreateBuff()
    {
        if (inventorySystem.CreateBuff(objectName))
        {
            resourcesIneractManager.DecreaseResources(menuInventorySlotManager.GetPrice(objectName));
            FillPetals();
        };
    }

    public void CreateAmmo()
    {
        if (itemObject != null)
        {
            if (resourcesIneractManager.CheckResources(itemObject.createAmmoPrice))
            {
                itemObject.allAmmo += itemObject.increaseAmmoPerCreate;
                resourcesIneractManager.DecreaseResources(itemObject.createAmmoPrice);
                FillPetalsWeapon(objectName);
            }
        }
    }

    void FillPetals()
    {
        TextMeshProUGUI currentText = current.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI maximumText = maximum.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        switch (slotName)
        {
            case "hp":
                currentText.text = inventorySystem.hpBuffs.ToString();
                maximumText.text = inventorySystem.maximumBaffs.ToString();
                ShowUpgradePrice();
                break;
            case "armor":
                currentText.text = inventorySystem.armorBuffs.ToString();
                maximumText.text = inventorySystem.maximumBaffs.ToString();
                ShowUpgradePrice();
                break;
            case "speed":
                currentText.text = inventorySystem.speedBuffs.ToString();
                maximumText.text = inventorySystem.maximumBaffs.ToString();
                ShowUpgradePrice();
                break;
            case "ak":
                FillPetalsWeapon(objectName);
                break;
            case "revolver":
                FillPetalsWeapon(objectName);
                break;

            default: break;
        };
    }

    void DeactivateSlot()
    {
        HidePetals();
        transform.parent.Find("Icon").GetComponent<Image>().color = Color.grey;
        GetComponent<Button>().enabled = false;
        lockedImage.SetActive(true);
    }

    void ActivateSlot()
    {
        transform.parent.Find("Icon").GetComponent<Image>().color = Color.white;
        GetComponent<Button>().enabled = true;
        lockedImage.SetActive(false);
    }

    void FillPetalsWeapon(string name)
    {
        List<GameObject> weapons = inventorySystem.mainGuns;
        foreach (GameObject weapon in weapons)
        {
            ItemObject itemObject = weapon.GetComponent<ItemObject>();
            if (itemObject.itemStat.type is ItemSO.Type.firearms)
            {

                if (itemObject.itemStat.name.ToString() == name)
                {
                    current.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = itemObject.allAmmo.ToString();
                    maximum.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = itemObject.increaseAmmoPerCreate.ToString();
                    DrawUpgradePrice(itemObject.createAmmoPrice);
                }
            }
        }
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
        ResourcesSO resources = menuInventorySlotManager.GetPrice(objectName);

        if (resources != null)
        {
            DrawUpgradePrice(resources);
        } else
        {
            DeactivateSlot();
        }
    }

    void DrawUpgradePrice(ResourcesSO resources)
    {
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

    bool ForBuffs()
    {
        return objectName == "hp" || objectName == "armor" || objectName == "speed";
    }

    void FindWeapon()
    {
        List<GameObject> weapons = inventorySystem.mainGuns;

        foreach (GameObject weapon in weapons)
        {
            ItemObject item = weapon.GetComponent<ItemObject>();
            if (item.itemStat.type is ItemSO.Type.firearms)
            {
                if (item.itemStat.name.ToString() == objectName)
                {
                    itemObject = item;
                }
            }
        }
    }

    void OpenSlot()
    {
        StopCoroutine("LerpFunction");
        StartCoroutine(LerpFunction(openRotation, duration));
        if (gameObject.activeInHierarchy)
        {
            priceParent.GetComponent<RectTransform>().anchoredPosition = priceParentPosition;
            ShowUpgradePrice();
        }
    }

    void ValidateSlot()
    {
        if (!ForBuffs())
        {
            FindWeapon();
            if (itemObject != null)
            {
                ActivateSlot();
            } else
            {
                DeactivateSlot();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (ForBuffs())
        {
            OpenSlot();
        } else
        {
            if (itemObject != null)
            {
                OpenSlot();
            } else
            {
                DeactivateSlot();
            }
        }

       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine("LerpFunction");
        StartCoroutine(LerpFunction(closedRotation, duration));
        if (gameObject.activeInHierarchy)
        {
            HideUpgradePrice();
        }
    }

    void OnEnable()
    {
        StartCoroutine(LerpFunction(closedRotation, duration));
        HideUpgradePrice();
        ValidateSlot();
    }
}