using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using EPOOutline;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Canvas canvas;

    private ItemsIK takeInHand;
    public bool haveHeadlight;

    public List<GameObject> mainGuns;
    public List<GameObject> extraGuns;


    private int mainGunCounterToggler = 0;

    private WeaponSlotManager weaponSlotManager;
    private BuffsSlotManager buffsSlotManager;
    private UIResourcesManager uiResourcesManager;


    public int ActiveMainGun
    {
        get { return activeMainGun; }
        set
        {
            if (activeMainGun == value)
            {
                if (mainGunCounterToggler != 0)
                {
                    activeMainGun = value;
                    mainGunCounterToggler = 0;
                    takeInHand.takeMainGun(activeMainGun);
                }
                else
                {
                    takeInHand.ClearHands();
                    mainGunCounterToggler++;
                }
            }
            else
            {
                if (value <= mainGuns.Count)
                {
                    activeMainGun = value;
                    takeInHand.takeMainGun(activeMainGun);
                    mainGunCounterToggler = 0;
                }
            }

        }
    }


    public int activeMainGun = -1;

    public int hpBuffs;
    public ResourcesSO hpPrice;

    public int armorBuffs;
    public ResourcesSO armorPrice;

    public int speedBuffs;
    public ResourcesSO speedPrice;



    public int maximumBaffs;

    [SerializeField] private int activeBuff = 0;

    private ItemSO itemObject;

    private HPAndArmor hpAndArmor;

    /*
        [SerializeField] private GrenadeThrow grenadeThrow;

        [SerializeField] private GameObject mainGunSpawn;*/

    public float currentBatteryEnergy = 1f;
    public int batteries = 0;

    public int fuel
    {
        get { return Fuel; }
        set
        {
            Fuel = value;
            uiResourcesManager.UiUpdateResources();
        }

    }
    public int Fuel = 0;
    public int cloth
    {
        get { return Cloth; }
        set
        {
            Cloth = value;
            uiResourcesManager.UiUpdateResources();
        }

    }
    public int Cloth = 0;
    public int metal
    {
        get { return Metal; }
        set
        {
            Metal = value;
            uiResourcesManager.UiUpdateResources();
        }

    }
    public int Metal = 0;

    public int plastic
    {
        get { return Plastic; }
        set
        {
            Plastic = value;
            uiResourcesManager.UiUpdateResources();
        }

    }
    public int Plastic = 0;
    public int chemical
    {
        get { return Chemical; }
        set
        {
            Chemical = value;
            uiResourcesManager.UiUpdateResources();
        }

    }
    public int Chemical = 0;
    public int wires
    {
        get { return Wires; }
        set
        {
            Wires = value;
            uiResourcesManager.UiUpdateResources();
        }

    }
    public int Wires = 0;

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
        takeInHand = player.GetComponent<ItemsIK>();
        weaponSlotManager = GameObject.FindGameObjectWithTag("WeaponSlot").GetComponent<WeaponSlotManager>();
        buffsSlotManager = GameObject.FindGameObjectWithTag("BuffsSlot").GetComponent<BuffsSlotManager>();
        buffsSlotManager.DrawBuffs(hpBuffs, armorBuffs, speedBuffs, activeBuff);
        buffsSlotManager.DrawGrenades(extraGuns.Count);
        uiResourcesManager = GameObject.FindGameObjectWithTag("MenuResources").GetComponent<UIResourcesManager>();
    }

    public void OnBuffChange(InputAction.CallbackContext ctx) //СМЕНА АКТИВНОГО БАФФА
    {
        if (ctx.performed)
        {
            activeBuff = (activeBuff + 1) % 3;
            buffsSlotManager.DrawBuffs(hpBuffs, armorBuffs, speedBuffs, activeBuff);
        }
    }

    public void OnBuffUse(InputAction.CallbackContext ctx) //ПРИМЕНЕНИЕ АКТИВНОГО БАФФА
    {
        if (ctx.performed)
        {
            UseBuff(activeBuff);
        }
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Q)) //СМЕНА АКТИВНОГО БАФФА
        // {
        //     activeBuff = (activeBuff + 1) % 3;
        //     buffsSlotManager.DrawBuffs(hpBuffs.Count, armorBuffs.Count, speedBuffs.Count, activeBuff);
        // }

        // if (Input.GetKeyDown(KeyCode.X)) //ПРИМЕНЕНИЕ АКТИВНОГО БАФФА
        // {
        //     UseBuff(activeBuff);
        // }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActiveMainGun = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActiveMainGun = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActiveMainGun = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActiveMainGun = 4;
        }

        if (Input.GetKeyDown(KeyCode.G)) //ВЫКИНУТЬ ПРЕДМЕТ
        {
            DiscardTheItem(mainGuns[activeMainGun]);

            ActiveMainGun = Math.Max(mainGuns.Count - 1, 0);
            mainGuns[ActiveMainGun].layer = 20;




        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //КОЛЁСИКОМ ВПЕРЁД
        {
            if (mainGuns.Count > 0)
            {
                ActiveMainGun = (ActiveMainGun + 1) % mainGuns.Count;
            }
            else
            {
                ActiveMainGun = 0;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) //КОЛЁСИКОМ НАЗАД
        {
            if (mainGuns.Count > 0)
            {
                ActiveMainGun = (mainGuns.Count + ActiveMainGun - 1) % mainGuns.Count;
            }
            else
            {
                ActiveMainGun = 0;
            }
        }
    }

    public void UseBattery()
    {
        batteries--;
        if (batteries > 0)
        {
            currentBatteryEnergy = 1f;
        }
    }


    //-------------ПОДОБРАТЬ ИЛИ ВЫБРОСИТЬ ПРЕДМЕТ (ОРУЖИЕ И ГРАНАТЫ)---------------
    public void DiscardTheItem(GameObject item)
    {
        item.transform.position = player.position + new Vector3(0, 2f, 0);
        item.transform.parent = null;

        item.GetComponent<CustomTooltip>().enabled = true;
        item.GetComponent<SphereCollider>().enabled = true;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.GetComponent<BoxCollider>().enabled = true;

        item.layer = 20;
        //item.GetComponent<Outlinable>().OutlineLayer = 20;
        item.SetActive(true);
        mainGuns.Remove(item);
        takeInHand.ClearHands();
        weaponSlotManager.ChangeActiveWeapon();

    }

    public void SetActiveMainGun(int active)
    {
        activeMainGun = active;
    }

    public void PickUpItem(GameObject item)
    {
        if (item.GetComponent<ItemObject>())
        {
            if (mainGuns.Count < 3)
            {
                mainGuns.Add(item);
                item.SetActive(false);
                item.GetComponent<CustomTooltip>().enabled = false;
                item.GetComponent<SphereCollider>().enabled = false;
                item.GetComponent<Rigidbody>().isKinematic = true;
                item.GetComponent<BoxCollider>().enabled = false;
                item.layer = 0;
                //item.GetComponent<Outlinable>().OutlineLayer = 0;
            }
            else
            {
                Debug.Log("Основной инвентарь уже полный");
            }

        }
        /*        else if (item.GetComponent<ExtraObject>())
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

                }*/

    }



    //-------------ПОДОБРАТЬ БАФФЫ (ХП, СИЛА, СКОРОСТЬ)---------------
    public void PickUpBuff(GameObject item)
    {
        if (item.GetComponent<BuffObject>().BuffStat.type.ToString() is "hp")
        {
            if (hpBuffs < maximumBaffs)
            {
                hpBuffs += 1;
            }
            else
            {
                Debug.Log("---------");
                Debug.Log("ХП инвентарь уже полный");
            }
        }
        else if (item.GetComponent<BuffObject>().BuffStat.type.ToString() is "speed")
        {
            if (speedBuffs < maximumBaffs)
            {
                speedBuffs += 1;
            }
            else
            {
                Debug.Log("---------");
                Debug.Log("Speed инвентарь уже полный");
            }
        }
        else if (item.GetComponent<BuffObject>().BuffStat.type.ToString() is "armor")
        {
            if (armorBuffs < maximumBaffs)
            {
                armorBuffs += 1;
            }
            else
            {
                Debug.Log("---------");
                Debug.Log("armor инвентарь уже полный");
            }
        }

        buffsSlotManager.DrawBuffs(hpBuffs, armorBuffs, speedBuffs, activeBuff);
    }


    public bool CreateBuff(string type)
    {
        bool flag = true;
        if (type == "hp")
        {
            if (hpBuffs < maximumBaffs)
            {
                hpBuffs += 1;
            }
            else
            {
                Debug.Log("---------");
                Debug.Log("ХП инвентарь уже полный");
                flag = false;
            }
        }
        else if (type == "speed")
        {
            if (speedBuffs < maximumBaffs)
            {
                speedBuffs += 1;
            }
            else
            {
                Debug.Log("---------");
                Debug.Log("Speed инвентарь уже полный");
                flag = false;
            }
        }
        else if (type == "armor")
        {
            if (armorBuffs < maximumBaffs)
            {
                armorBuffs += 1;
            }
            else
            {
                Debug.Log("---------");
                Debug.Log("armor инвентарь уже полный");
                flag = false;
            }
        }

        buffsSlotManager.DrawBuffs(hpBuffs, armorBuffs, speedBuffs, activeBuff);
        return flag;
    }



    public void UseBuff(int active)
    {
        if (activeBuff == 0 && hpBuffs != 0)
        {
            hpAndArmor.TakeHeal(20, "hp");
            hpBuffs -= 1;

        }
        else if (activeBuff == 1 && armorBuffs != 0)
        {
            hpAndArmor.TakeHeal(20, "armor");
            armorBuffs -= 1;
        }
        else if (activeBuff == 2 && speedBuffs != 0)
        {
            speedBuffs -= 1;
        }

        buffsSlotManager.DrawBuffs(hpBuffs, armorBuffs, speedBuffs, activeBuff);

    }

    public void PickUpComponent(GameObject component)
    {
        for (int i = 0; i < component.GetComponent<ComponentsObject>().componentStat.Count; i++)
        {
            string type = component.GetComponent<ComponentsObject>().componentStat[i].type.ToString();
            switch (type)
            {
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
