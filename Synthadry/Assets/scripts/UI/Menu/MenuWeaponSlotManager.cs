using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuWeaponSlotManager : MonoBehaviour
{

    public GameObject[] slots;
    public Vector3[] baseSlotsPosition;
    public Vector3[] extendedSlotsPosition;

    public Vector2 baseSlotsSize;
    public Vector2 extendedSlotsSize;
    private List<GameObject> weapons;

    private void Awake()
    {
        weapons = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>().mainGuns;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

    public void ExtandSlot(int slotId)
    {
        HideAllSlots();

        FillSlots();

        if (weapons.Count > slotId)
        {
            FillExtendedSlot(slotId);
            slots[slotId].GetComponent<RectTransform>().sizeDelta = extendedSlotsSize;
            slots[slotId].transform.localPosition = new Vector3(slots[slotId].transform.localPosition.x - ((extendedSlotsSize.x - baseSlotsSize.x) / 2) + 8, slots[slotId].transform.localPosition.y - ((extendedSlotsSize.y - baseSlotsSize.y) / 2));
            for (int i = slotId + 1; i < slots.Length; i++)
            {
                slots[i].transform.localPosition = new Vector3(slots[i].transform.localPosition.x - 3, slots[i].transform.localPosition.y - (extendedSlotsSize.y - baseSlotsSize.y));
            }
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
