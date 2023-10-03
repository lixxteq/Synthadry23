using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;

public class MouseOverPart : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> UiElems;
    /*   0 - иконка
         1 - сейчас партронов
         2 - всего патронов
         3 - номер клавиши
    */
    [SerializeField] private int indexOfUi;

    [SerializeField] private Image damageLine;
    [SerializeField] private Image rateLine;
    [SerializeField] private TextMeshProUGUI typeOfBulletValue;
    [SerializeField] private TextMeshProUGUI rarityValue;

    [SerializeField] private GameObject center;

    private InventorySystem inventorySystem;
    private ItemSO item;

    void ClearInfoCenter()
    {
        center.SetActive(false);
    }

    void SetInfoCenter()
    {
        center.SetActive(true);

        item = inventorySystem.mainGuns[indexOfUi].GetComponent<ItemObject>().itemStat;
        damageLine.fillAmount = Convert.ToSingle(inventorySystem.mainGuns[indexOfUi].GetComponent<ItemObject>().damage / 100);
        rateLine.fillAmount = Convert.ToSingle(inventorySystem.mainGuns[indexOfUi].GetComponent<ItemObject>().rateOfFire / 100);

        switch (item.rarity.ToString())
        {
            case "standart":
                rarityValue.text = "ОБЫЧНЫЙ";
                rarityValue.color = new Color(0.6f, 0.4f, 0.4f);
                break;
            case "rare":
                rarityValue.text = "РЕДКИЙ";
                rarityValue.color = new Color(0.9f, 1f, 0.3f);
                break;
            case "epic":
                rarityValue.text = "ЭПИЧЕСКИЙ";
                rarityValue.color = new Color(1f, 0f, 1f);
                break;
        }

/*        switch (item.typeOfMissile.ToString())
        {
            case "bullet":
                typeOfBulletValue.text = "ОБЫЧНЫЙ";
                typeOfBulletValue.color = new Color(1f, 1f, 0f);
                break;
            case "electro":
                typeOfBulletValue.text = "ЗАРЯД";
                typeOfBulletValue.color = new Color(0f, 0f, 1f);
                break;
            case "hand":
                typeOfBulletValue.text = "РУЧНОЙ";
                typeOfBulletValue.color = new Color(1f, 1f, 1f);
                break;
        }*/

    }

    public void ShowActivePart(int number)
    {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        if (inventorySystem.mainGuns[indexOfUi] != null)
        {
            if (gameObject.GetComponent<Image>() != null)
            {
                if (indexOfUi <= 3)
                {
                    inventorySystem.SetActiveMainGun(indexOfUi);
                    inventorySystem.MainGunsUi.UpdateMainGunsUi(indexOfUi);
                    SetInfoCenter();

                }

            }
        }
        
    }

    void ClearBuffs()
    {
        if (indexOfUi >= 20)
        {
            for (var i = 0; i < UiElems.Count; i++)
            {
                UiElems[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = null;
                Debug.Log(UiElems[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite);
                UiElems[i].transform.GetChild(0).gameObject.SetActive(false);
                UiElems[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
            }
        }

    }

    void OnDisable()
    {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
        UiElems[0].GetComponent<Image>().sprite = null;
        UiElems[0].SetActive(false);
        UiElems[3].SetActive(false);
        UiElems[1].GetComponent<TextMeshProUGUI>().text = "";
        UiElems[2].GetComponent<TextMeshProUGUI>().text = "";
        ClearBuffs();
        center.SetActive(false);
    }

    void OnEnable()
    {
        inventorySystem = player.GetComponent<InventorySystem>();
        
        if (indexOfUi < 10)
        {
            if (inventorySystem.mainGuns[indexOfUi] != null)
            {
                UiElems[0].SetActive(true);
                UiElems[3].SetActive(true);
                UiElems[0].GetComponent<Image>().sprite = inventorySystem.mainGuns[indexOfUi].GetComponent<ItemObject>().itemStat.iconActive1K;
                if (inventorySystem.mainGuns[indexOfUi].GetComponent<ItemObject>().itemStat.type.ToString() is "coldWeapons")
                {
                    UiElems[1].GetComponent<TextMeshProUGUI>().text = "∞";
                    UiElems[2].GetComponent<TextMeshProUGUI>().text = "/ ∞";
                } else
                {
                    UiElems[1].GetComponent<TextMeshProUGUI>().text = inventorySystem.mainGuns[indexOfUi].GetComponent<ItemObject>().currentAmmo.ToString();
                    UiElems[2].GetComponent<TextMeshProUGUI>().text = "/ " + inventorySystem.mainGuns[indexOfUi].GetComponent<ItemObject>().allAmmo.ToString();
                }
        }

        } else if (indexOfUi >= 10 && indexOfUi < 20) {
            if (inventorySystem.extraGuns.Count > 0)
            {
                UiElems[0].SetActive(true);
                UiElems[0].GetComponent<Image>().sprite = inventorySystem.extraGuns[0].GetComponent<ItemObject>().itemStat.iconActive1K;
                UiElems[1].GetComponent<TextMeshProUGUI>().text = inventorySystem.extraGuns.Count.ToString();
            }
        } else if (indexOfUi >= 20)
        {
            if (inventorySystem.hpBuffs.Count > 0)
            {
                UiElems[0].transform.GetChild(0).gameObject.SetActive(true);
                UiElems[0].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = inventorySystem.hpBuffs.Count.ToString();
            } else
            {
                UiElems[0].transform.GetChild(0).gameObject.SetActive(false);
                UiElems[0].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
            }
            if (inventorySystem.speedBuffs.Count > 0)
            {
                UiElems[1].transform.GetChild(0).gameObject.SetActive(true);
                UiElems[1].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = inventorySystem.speedBuffs.Count.ToString();
            } else
            {
                UiElems[1].transform.GetChild(0).gameObject.SetActive(false);
                UiElems[1].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
            }
            if (inventorySystem.armorBuffs.Count > 0)
            {
                UiElems[2].transform.GetChild(0).gameObject.SetActive(true);
                UiElems[2].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = inventorySystem.armorBuffs.Count.ToString();
            } else
            {
                UiElems[2].transform.GetChild(0).gameObject.SetActive(false);
                UiElems[2].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "";
            }

        }
            
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(gameObject);
        ShowActivePart(indexOfUi);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
        center.SetActive(false);
    }
}