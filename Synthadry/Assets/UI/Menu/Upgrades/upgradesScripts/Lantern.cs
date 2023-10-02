using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lantern : MonoBehaviour
{

    private InventorySystem PlayerInventory;
    [SerializeField] private MaterialUIController MaterialsUi;

    [SerializeField] private Button DoButton;
    [SerializeField] private TextMeshProUGUI DoButtonText;

    [SerializeField] private Button RemoveButton;
    [SerializeField] private TextMeshProUGUI RemoveButtonText;

    public int fuel = 0;
    public int cloth = 0;
    public int metal = 0;
    public int plastic = 0;
    public int chemical = 0;
    public int wires = 0;

    public int weaponNum = 0;
    private void Start()
    {
        PlayerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
    }
    public void CheckLantern()
    {
        if (!PlayerInventory.mainGuns[weaponNum].transform.Find("lantern")) //≈—“‹ ¬Œ«ÃŒ∆ÕŒ—“‹ œŒ—“¿¬»“‹ ‘ŒÕ¿–» 
        {
            gameObject.SetActive(false);
            return;
        }
        if (PlayerInventory.mainGuns[weaponNum].transform.Find("lantern").gameObject.activeSelf) //‘ŒÕ¿–»  ”—“¿ÕŒ¬À≈Õ
        {
            DoButton.GetComponent<Button>().enabled = false;
            DoButtonText.color = new Color(0.3f, 0.3f, 0.3f, 1);

            RemoveButton.GetComponent<Button>().enabled = true;
            RemoveButtonText.color = new Color(1, 1, 1, 1);
        } else
        {
            if (PlayerInventory.fuel - fuel < 0 || PlayerInventory.cloth - cloth < 0 || PlayerInventory.metal - metal < 0 || PlayerInventory.plastic - plastic < 0 || PlayerInventory.chemical - chemical < 0 || PlayerInventory.wires - wires < 0)
            {
                DoButton.GetComponent<Button>().enabled = false;
                DoButtonText.color = new Color(0.3f, 0.3f, 0.3f, 1);

                RemoveButton.GetComponent<Button>().enabled = false;
                RemoveButtonText.color = new Color(0.3f, 0.3f, 0.3f, 1);
            } else
            {
                DoButton.GetComponent<Button>().enabled = true;
                DoButtonText.color = new Color(1, 1, 1, 1);

                RemoveButton.GetComponent<Button>().enabled = false;
                RemoveButtonText.color = new Color(0.3f, 0.3f, 0.3f, 1);
            }
        }
    }

    public void CreateLantern()
    {
        PlayerInventory.fuel -= fuel;
        PlayerInventory.cloth -= cloth;
        PlayerInventory.metal -= metal;
        PlayerInventory.plastic -= plastic;
        PlayerInventory.chemical -= chemical;
        PlayerInventory.wires -= wires;

        PlayerInventory.mainGuns[weaponNum].transform.Find("lantern").gameObject.SetActive(true);

        DoButton.GetComponent<Button>().enabled = false;
        DoButtonText.color = new Color(0.3f, 0.3f, 0.3f, 1);

        RemoveButton.GetComponent<Button>().enabled = true;
        RemoveButtonText.color = new Color(1, 1, 1, 1);

        MaterialsUi.UpdateMaterialsUI();
    }

    public void RemoveLantern()
    {
        PlayerInventory.fuel += fuel;
        PlayerInventory.cloth += cloth;
        PlayerInventory.metal += metal;
        PlayerInventory.plastic += plastic;
        PlayerInventory.chemical += chemical;
        PlayerInventory.wires += wires;

        PlayerInventory.mainGuns[weaponNum].transform.Find("lantern").gameObject.SetActive(false);

        DoButton.GetComponent<Button>().enabled = true;
        DoButtonText.color = new Color(1, 1, 1, 1);

        RemoveButton.GetComponent<Button>().enabled = false;
        RemoveButtonText.color = new Color(0.3f, 0.3f, 0.3f, 1);

        MaterialsUi.UpdateMaterialsUI();

    }


}
