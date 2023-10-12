using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabController : MonoBehaviour
{
    [Header("First Slot")]
    public GameObject mainParentF;
    public Image weaponImageF;
    public TextMeshProUGUI currentAmmoF;
    public TextMeshProUGUI allAmmoF;
    public Image damageInfoF;

    [Header("Second Slot")]
    public GameObject mainParentS;
    public Image weaponImageS;
    public TextMeshProUGUI currentAmmoS;
    public TextMeshProUGUI allAmmoS;
    public Image damageInfoS;

    [Header("Third Slot")]
    public GameObject mainParentT;
    public Image weaponImageT;
    public TextMeshProUGUI currentAmmoT;
    public TextMeshProUGUI allAmmoT;
    public Image damageInfoT;

    [Header("Fourth Slot")]
    public TextMeshProUGUI hpValue;
    public TextMeshProUGUI armorValue;
    public TextMeshProUGUI boostValue;
    public TextMeshProUGUI grenadesValue;

    [Header("Resources")]
    public TextMeshProUGUI fuelCount;
    public TextMeshProUGUI clothCount;
    public TextMeshProUGUI metalCount;
    public TextMeshProUGUI plasticCount;
    public TextMeshProUGUI chemicalCount;
    public TextMeshProUGUI wiresCount;

    public TextMeshProUGUI batteriesCount;
    public Image batteryFillImage;
    public TextMeshProUGUI skillPointsCount;

    private InventorySystem _playerInventory;

    private void Start()
    {
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
        DrawSlots();
        DrawBuffs();
        DrawResources();
    }

    private void OnEnable()
    {
        DrawResources();
        DrawBuffs();
        DrawSlots();
        DrawBuffs();
    }

    private void OnDisable()
    {
        DrawResources();
        ClearSlots();
        DrawBuffs();
    }

    void DrawBuffs()
    {
        hpValue.text = _playerInventory.hpBuffs.Count.ToString();
        armorValue.text = _playerInventory.armorBuffs.Count.ToString();
        boostValue.text = _playerInventory.speedBuffs.Count.ToString();
        grenadesValue.text = _playerInventory.extraGuns.Count.ToString();
    }

    void DrawResources()
    {
        fuelCount.text = _playerInventory.fuel.ToString();
        clothCount.text = _playerInventory.cloth.ToString();
        metalCount.text = _playerInventory.metal.ToString();
        plasticCount.text = _playerInventory.plastic.ToString();
        chemicalCount.text = _playerInventory.chemical.ToString();
        wiresCount.text = _playerInventory.wires.ToString();
        batteriesCount.text = _playerInventory.batteries.ToString();
        skillPointsCount.text = "0";/*_playerInventory.skillPoints.ToString();*/
        Debug.Log("123");
    }

    void DrawBattery(float batteryFill)
    {
        batteryFillImage.fillAmount = batteryFill;
    }

    void DrawSlots()
    {
        if (_playerInventory.mainGuns[0].TryGetComponent(out ItemObject firstWeapon))
        {
            if (_playerInventory.activeMainGun == 0)
            {
                weaponImageF.sprite = firstWeapon.itemStat.iconActive1K;
                weaponImageF.SetNativeSize();
            }
            else
            {
                weaponImageF.sprite = firstWeapon.itemStat.iconDisable1K;
                weaponImageF.SetNativeSize();
            }
            currentAmmoF.text = firstWeapon.currentAmmo.ToString();
            allAmmoF.text = firstWeapon.allAmmo.ToString();
            damageInfoF.fillAmount = firstWeapon.damage / 100;
            mainParentF.SetActive(true);
        }

        if (_playerInventory.mainGuns[1].TryGetComponent(out ItemObject secondWeapon))
        {
            if (_playerInventory.activeMainGun == 1)
            {
                weaponImageS.sprite = secondWeapon.itemStat.iconActive1K;
                weaponImageS.SetNativeSize();

            }
            else
            {
                weaponImageS.sprite = secondWeapon.itemStat.iconDisable1K;
                weaponImageS.SetNativeSize();

            }
            currentAmmoS.text = secondWeapon.currentAmmo.ToString();
            damageInfoS.fillAmount = secondWeapon.damage / 100;
            allAmmoS.text = secondWeapon.allAmmo.ToString();
            mainParentS.SetActive(true);
        }

        if (_playerInventory.mainGuns[2].TryGetComponent(out ItemObject thirdWeapon))
        {
            Debug.Log(thirdWeapon);
            if (_playerInventory.activeMainGun == 3)
            {
                weaponImageT.sprite = thirdWeapon.itemStat.iconActive1K;
                weaponImageT.SetNativeSize();
            }
            else
            {
                weaponImageT.sprite = thirdWeapon.itemStat.iconDisable1K;
                weaponImageT.SetNativeSize();

            }
            currentAmmoT.text = thirdWeapon.currentAmmo.ToString();
            damageInfoT.fillAmount = thirdWeapon.damage / 100;
            allAmmoT.text = thirdWeapon.allAmmo.ToString();
            mainParentT.SetActive(true);
        }
    }

    void ClearSlots()
    {
        mainParentF.SetActive(false);
        mainParentS.SetActive(false);
        mainParentT.SetActive(false);

    }

}
