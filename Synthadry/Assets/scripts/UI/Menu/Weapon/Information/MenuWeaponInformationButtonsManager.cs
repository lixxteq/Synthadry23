using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuWeaponInformationButtonsManager : MonoBehaviour
{

    public GameObject damageContainer;
    public GameObject ammoContainer;
    public GameObject rateOfFireContainer;
    public GameObject lanternContainer;

    public Color activeUpgradeButton;
    public Color disabledUpgradeButton;

    public Color activeUpgradeText;
    public Color disabledUpgradeText;

    public Color activeRemoveButton;
    public Color disabledRemoveButton;

    public Color activeRemoveText;
    public Color disabledRemoveText;


    public void CheckAllStat(MenuWeaponSlotManager menuWeaponSlotManager)
    {
        CheckStat(damageContainer, "damage", menuWeaponSlotManager);
        CheckStat(ammoContainer, "ammo", menuWeaponSlotManager);
        CheckStat(rateOfFireContainer, "rateOfFire", menuWeaponSlotManager);
        CheckStat(lanternContainer, "lantern", menuWeaponSlotManager);
    }

    void CheckStat(GameObject container, string stat, MenuWeaponSlotManager menuWeaponSlotManager)
    {
        GameObject upgradeButton = container.transform.Find("UpgradeButton").gameObject;
        GameObject downgradeButton = container.transform.Find("RemoveButton").gameObject;

        if (menuWeaponSlotManager.CanUpgradeExtendedWeapon(stat))
        {
            upgradeButton.GetComponent<Image>().color = activeUpgradeButton;
            upgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = activeUpgradeText;
            upgradeButton.GetComponent<Button>().enabled = true;
        } else
        {
            upgradeButton.GetComponent<Image>().color = disabledUpgradeButton;
            upgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = disabledUpgradeText;
            upgradeButton.GetComponent<Button>().enabled = false;
        }

        if (menuWeaponSlotManager.CanDowngradeExtendedWeapon(stat))
        {
            downgradeButton.GetComponent<Image>().color = activeRemoveButton;
            downgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = activeRemoveText;
            downgradeButton.GetComponent<Button>().enabled = true;
        }
        else
        {
            downgradeButton.GetComponent<Image>().color = disabledRemoveButton;
            downgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = disabledRemoveText;
            downgradeButton.GetComponent<Button>().enabled = false;
        }
    }
}
