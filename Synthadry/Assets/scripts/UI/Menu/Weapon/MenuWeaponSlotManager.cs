using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuWeaponSlotManager : MonoBehaviour
{

    public GameObject[] slots;
    public Vector3[] baseSlotsPosition;


    public Vector2 baseSlotsSize;
    public Vector2 extendedSlotsSize;
    private List<GameObject> weapons;

    private MenuWeaponInfoManager menuWeaponInfoManager;
    private GameObject informationMenu;

    private int extendedSlotId;

    public TextMeshProUGUI weaponDescription;


    private void Awake()
    {
        weapons = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>().mainGuns;
        informationMenu = gameObject.transform.parent.Find("Information").gameObject;
        menuWeaponInfoManager = gameObject.transform.parent.Find("Information").gameObject.GetComponent<MenuWeaponInfoManager>();
    }

    public GameObject GetExtendedSlotWeapon()
    {
        return weapons[extendedSlotId];
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    

    public ResourcesSO GetDowngradePrice(string stat)
    {
        return weapons[extendedSlotId].GetComponent<ItemObject>().GetDowngradePrice(stat);
    }

    public ResourcesSO GetUpgradePrice(string stat)
    {
        return weapons[extendedSlotId].GetComponent<ItemObject>().GetUpgradePrice(stat);
    }


    public void UpgradeExtendedWeapon(string stat)
    {
        weapons[extendedSlotId].GetComponent<ItemObject>().UpgradeStat(stat);
        ExtandSlot(extendedSlotId);
    }

    public void DowngradeExtendedWeapon(string stat)
    {
        weapons[extendedSlotId].GetComponent<ItemObject>().DowngradeStat(stat);
        ExtandSlot(extendedSlotId);
    }


    private void OnEnable()
    {
        ExtandSlot(0);
    }

    void FillSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Image image = slots[i].transform.GetChild(0).gameObject.GetComponent<Image>();
            if (weapons.Count > i)
            {
                Sprite icon = weapons[i].GetComponent<ItemObject>().itemStat.iconDisable1K;
                image.sprite = icon;
                image.rectTransform.sizeDelta = icon.rect.size;
                image.rectTransform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                image.color = Color.white;
            }
            else
            {
                image.color = Color.clear;
            }
        }
    }

    void FillExtendedSlot(int slotId)
    {
        Image image = slots[slotId].transform.GetChild(0).gameObject.GetComponent<Image>();
        Sprite icon = weapons[slotId].GetComponent<ItemObject>().itemStat.iconActive1K;
        image.sprite = icon;
        image.rectTransform.sizeDelta = icon.rect.size;
        image.rectTransform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        image.color = Color.white;
    }

    public int GetExtandedSlotId()
    {
        return extendedSlotId;
    }

    public void ExtandSlot(int slotId)
    {
        HideAllSlots();

        FillSlots();

        if (weapons.Count > slotId)
        {
            extendedSlotId = slotId;

            FillExtendedSlot(slotId);
            slots[slotId].GetComponent<RectTransform>().sizeDelta = extendedSlotsSize;
            slots[slotId].transform.localPosition = new Vector3(slots[slotId].transform.localPosition.x - ((extendedSlotsSize.x - baseSlotsSize.x) / 2) + 8, slots[slotId].transform.localPosition.y - ((extendedSlotsSize.y - baseSlotsSize.y) / 2));
            for (int i = slotId + 1; i < slots.Length; i++)
            {
                slots[i].transform.localPosition = new Vector3(slots[i].transform.localPosition.x - 3, slots[i].transform.localPosition.y - (extendedSlotsSize.y - baseSlotsSize.y));
            }
            menuWeaponInfoManager.FillCells(weapons[slotId].GetComponent<ItemObject>());
            weaponDescription.text = weapons[slotId].GetComponent<ItemObject>().itemStat.description;
            informationMenu.SetActive(true);
        }
        else
        {
            informationMenu.SetActive(false);
        }
    }

    public void HideAllSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<RectTransform>().sizeDelta = baseSlotsSize;
            slots[i].transform.localPosition = baseSlotsPosition[i];
        }
    }
}
