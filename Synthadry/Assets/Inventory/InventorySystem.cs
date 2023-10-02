using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class InventorySystem : MonoBehaviour
{
    [SerializeField] private Transform player;
    public MainGunsController MainGunsUi;
    public BuffsController BuffsUi;

    public List<GameObject> mainGuns;
    public List<GameObject> extraGuns;
    public Boolean haveHeadlight;

    [SerializeField] private List<GameObject> UiExtraGuns;
    /*   0 - иконка
         1 - количество*/

    public int activeMainGun = 0;

    public List<GameObject> hpBuffs;
    public List<GameObject> armorBuffs;
    public List<GameObject> speedBuffs;

    private int TypeOfBuffsCount = 0;

    [SerializeField] private int activeBuff = 0;

    private ItemObject itemObject;

    private HPAndArmor hpAndArmor;
/*
    [SerializeField] private GrenadeThrow grenadeThrow;

    [SerializeField] private GameObject mainGunSpawn;*/

    [Header("КОМПОНЕНТЫ")]
    public int fuel = 0;
    public int cloth = 0;
    public int metal = 0;
    public int plastic = 0;
    public int chemical = 0;
    public int wires = 0;

/*    public void GetActiveMainGun()
    {
        foreach (GameObject gun in mainGuns)
        {
            gun.SetActive(false);
        }
        mainGuns[activeMainGun].SetActive(true);
        mainGuns[activeMainGun].transform.SetParent(mainGunSpawn.transform);
        mainGuns[activeMainGun].transform.position = mainGunSpawn.transform.position;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        hpAndArmor = player.GetComponent<HPAndArmor>();
    }

    // Update is called once per frame
    void Update()
    {
        /*        if (Input.GetKeyDown(KeyCode.T)) //БРОСОК ГРАНАТЫ
                {
                    if (extraGuns.Count > 0)
                    {
                        grenadeThrow.ThrowGrenade(extraGuns[extraGuns.Count - 1]);
                        extraGuns.RemoveAt(extraGuns.Count - 1);
                        UpdateInventoryUIItems();
                    }
                }*/


        if (Input.GetKeyDown(KeyCode.Q)) //СМЕНА АКТИВНОГО БАФФА
        {
            activeBuff = (activeBuff + 1) % 3;
            BuffsUi.UpdateBuffsUi(hpBuffs, armorBuffs, speedBuffs, activeBuff);
        }

        if (Input.GetKeyDown(KeyCode.X)) //ПРИМЕНЕНИЕ АКТИВНОГО БАФФА
        {
            UseBuff(activeBuff);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeMainGun = 0;
            UpdateInventoryUIItems(activeMainGun);
            MainGunsUi.UpdateMainGunsUi(activeMainGun);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (mainGuns.Count > 1)
            {
                activeMainGun = 1;
                UpdateInventoryUIItems(activeMainGun);
                MainGunsUi.UpdateMainGunsUi(activeMainGun);
            }


        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (mainGuns.Count > 2)
            {
                activeMainGun = 2;
                UpdateInventoryUIItems(activeMainGun);
                MainGunsUi.UpdateMainGunsUi(activeMainGun);
            }


        }
        if (Input.GetKeyDown(KeyCode.G)) //ВЫКИНУТЬ ПРЕДМЕТ
        {
            DiscardTheItem(activeMainGun);

            activeMainGun = Math.Max(mainGuns.Count - 1, 0);

            UpdateInventoryUIItems(activeMainGun);
            MainGunsUi.UpdateMainGunsUi(activeMainGun);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //КОЛЁСИКОМ ВПЕРЁД
        {
            if (mainGuns.Count > 0)
            {
                activeMainGun = (activeMainGun + 1) % mainGuns.Count;
            } else
            {
                activeMainGun = 0;
            }
            UpdateInventoryUIItems(activeMainGun);
            MainGunsUi.UpdateMainGunsUi(activeMainGun);

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) //КОЛЁСИКОМ НАЗАД
        {
            if (mainGuns.Count > 0)
            {
                activeMainGun = (mainGuns.Count + activeMainGun - 1) % mainGuns.Count;
            } else
            {
                activeMainGun = 0;
            }

            UpdateInventoryUIItems(activeMainGun);
            MainGunsUi.UpdateMainGunsUi(activeMainGun);

        }
    }


    //-------------ПОДОБРАТЬ ИЛИ ВЫБРОСИТЬ ПРЕДМЕТ (ОРУЖИЕ И ГРАНАТЫ)---------------
    public void DiscardTheItem(int active)
    {
        mainGuns[active].transform.position = player.position;
        mainGuns[active].SetActive(true);
        mainGuns.Remove(mainGuns[active]);
        UpdateInventoryUIItems(activeMainGun);
    }

    public void SetActiveMainGun(int active)
    {
        activeMainGun = active;
    }

    public void PickUpItem(GameObject item)
    {
        if (item.GetComponent<ItemObject>().itemStat.type.ToString() is "firearms" || item.GetComponent<ItemObject>().itemStat.type.ToString() is "coldWeapons")
        {
            if (mainGuns.Count < 3)
            {
                Debug.Log("------------");
                Debug.Log("mainGuns:");
                mainGuns.Add(item);
                for (int i = 0; i < mainGuns.Count; i++)
                {
                    Debug.Log(mainGuns[i]);
                }
                MainGunsUi.UpdateMainGunsUi(activeMainGun);
                UpdateInventoryUIItems(activeMainGun);
            } else
            {
                Debug.Log("---------");
                Debug.Log("Основной инвентарь уже полный");
            }

        }
        else if (item.GetComponent<ItemObject>().itemStat.type.ToString() is "extra")
        {
            if (extraGuns.Count < 9)
            {
                Debug.Log("------------");
                Debug.Log("extraGuns:");
                extraGuns.Add(item);

                UpdateInventoryUIItems(activeMainGun);
            }
            else
            {
                Debug.Log("---------");
                Debug.Log("Дополнительный (гранатный) инвентарь уже полный");
            }

        }

    }

    public void UpdateInventoryUIItems(int active = 0)
    {
        if (extraGuns.Count > 0)
        {
            UiExtraGuns[0].SetActive(true);

            UiExtraGuns[0].GetComponent<Image>().sprite = extraGuns[0].GetComponent<ItemObject>().itemStat.iconActive1K;

            UiExtraGuns[1].GetComponent<TextMeshProUGUI>().text = extraGuns.Count.ToString();

            UiExtraGuns[2].SetActive(true);

        } else
        {
            UiExtraGuns[0].SetActive(false);
            UiExtraGuns[1].GetComponent<TextMeshProUGUI>().text = "";
            UiExtraGuns[2].SetActive(false);

        }
    }


    //-------------ПОДОБРАТЬ БАФФЫ (ХП, СИЛА, СКОРОСТЬ)---------------
    public void PickUpBuff(GameObject item)
    {
        if (item.GetComponent<BuffObject>().BuffStat.type.ToString() is "hp")
        {
            if (hpBuffs.Count < 9)
            {
                Debug.Log("------------");
                Debug.Log("hpBuffs:");
                hpBuffs.Add(item);
                for (int i = 0; i < hpBuffs.Count; i++)
                {
                    Debug.Log(hpBuffs[i]);
                }
                BuffsUi.UpdateBuffsUi(hpBuffs, armorBuffs, speedBuffs, activeBuff);
            }
            else
            {
                Debug.Log("---------");
                Debug.Log("ХП инвентарь уже полный");
            }
        }
        else if (item.GetComponent<BuffObject>().BuffStat.type.ToString() is "speed")
        {
            if (speedBuffs.Count < 9)
            {
                Debug.Log("------------");
                Debug.Log("speedBuffs:");
                speedBuffs.Add(item);
                for (int i = 0; i < speedBuffs.Count; i++)
                {
                    Debug.Log(speedBuffs[i]);
                }
                BuffsUi.UpdateBuffsUi(hpBuffs, armorBuffs, speedBuffs, activeBuff);
            }
            else
            {
                Debug.Log("---------");
                Debug.Log("Speed инвентарь уже полный");
            }
        }
        else if (item.GetComponent<BuffObject>().BuffStat.type.ToString() is "armor")
        {
            if (armorBuffs.Count < 9)
            {
                Debug.Log("------------");
                Debug.Log("armorBuffs:");
                armorBuffs.Add(item);
                for (int i = 0; i < armorBuffs.Count; i++)
                {
                    Debug.Log(armorBuffs[i]);
                }
                BuffsUi.UpdateBuffsUi(hpBuffs, armorBuffs, speedBuffs, activeBuff);
            }
            else
            {
                Debug.Log("---------");
                Debug.Log("armor инвентарь уже полный");
            }
        }
    }



    public void UseBuff(int active)
    {
        if (activeBuff == 0 && hpBuffs.Count != 0)
        {
            hpAndArmor.TakeHeal(hpBuffs[0].GetComponent<BuffObject>().BuffStat.increase, hpBuffs[0].GetComponent<BuffObject>().BuffStat.type.ToString());
            hpBuffs.RemoveAt(0);
            BuffsUi.UpdateBuffsUi(hpBuffs, armorBuffs, speedBuffs, activeBuff);
            return;

        }
        else if (activeBuff == 1 && armorBuffs.Count != 0)
        {
            hpAndArmor.TakeHeal(armorBuffs[0].GetComponent<BuffObject>().BuffStat.increase, armorBuffs[0].GetComponent<BuffObject>().BuffStat.type.ToString());
            armorBuffs.RemoveAt(0);
            BuffsUi.UpdateBuffsUi(hpBuffs, armorBuffs, speedBuffs, activeBuff);
            return;

        }
        else if (activeBuff == 2 && speedBuffs.Count != 0)
        {
            speedBuffs.RemoveAt(0);
            BuffsUi.UpdateBuffsUi(hpBuffs, armorBuffs, speedBuffs, activeBuff);
            return;
        }
    }

    public void PickUpComponent(GameObject component)
    {
        for (var i = 0; i < component.GetComponent<ComponentsObject>().componentStat.Count; i++)
        {
            string type = component.GetComponent<ComponentsObject>().componentStat[i].type.ToString();
            switch (type){
                case "fuel":
                    fuel += component.GetComponent<ComponentsObject>().amount[i];
                    break;
                case "cloth":
                    cloth += component.GetComponent<ComponentsObject>().amount[i];
                    break;
                case "metal":
                    metal += component.GetComponent<ComponentsObject>().amount[i];
                    break;
                case "plastic":
                    plastic += component.GetComponent<ComponentsObject>().amount[i];
                    break;
                case "chemical":
                    chemical += component.GetComponent<ComponentsObject>().amount[i];
                    break;
                case "wires":
                    wires += component.GetComponent<ComponentsObject>().amount[i];
                    break;
                case "":
                    break;

            }
        }
        
    }
}
