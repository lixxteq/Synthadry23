using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class MainGunsController : MonoBehaviour
{
    [SerializeField] private InventorySystem PlayerInventory;

    [SerializeField] private GameObject UiMainGuns;
    /*   0 - иконка
         1 - сейчас партронов
         2 - всего патронов*/

    private CustomCharacterController customCharacterController;

    private void Start()
    {
        customCharacterController = GetComponent<CustomCharacterController>();
    }

    public void UpdateMainGunsUi(int MainGunNum = -1)
    {
 /*       customCharacterController.UpdateMainGunName(PlayerInventory.mainGuns[PlayerInventory.activeMainGun].GetComponent<ItemObject>().itemStat.itemName);*/

        if (MainGunNum == -1)
        {
            MainGunNum = PlayerInventory.activeMainGun;
        }
        if (MainGunNum < PlayerInventory.mainGuns.Count)
        {
            UiMainGuns.transform.GetChild(1).gameObject.SetActive(true);
            UiMainGuns.transform.GetChild(1).GetComponent<Image>().sprite = PlayerInventory.mainGuns[MainGunNum].GetComponent<ItemObject>().itemStat.iconActive1K;

            /*if (PlayerInventory.mainGuns[MainGunNum].GetComponent<ItemObject>().itemStat.typeOfMissile.ToString() is "hand")
            {
                UiMainGuns.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "/∞";
                UiMainGuns.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "∞";
            } else
            {
                UiMainGuns.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "/" + PlayerInventory.mainGuns[MainGunNum].GetComponent<ItemObject>().allAmmo.ToString();
                UiMainGuns.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = PlayerInventory.mainGuns[MainGunNum].GetComponent<ItemObject>().currentAmmo.ToString();
            }*/
        } else
        {
            UiMainGuns.transform.GetChild(1).gameObject.SetActive(false);
            UiMainGuns.transform.GetChild(1).GetComponent<Image>().sprite = null;
            UiMainGuns.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            UiMainGuns.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
