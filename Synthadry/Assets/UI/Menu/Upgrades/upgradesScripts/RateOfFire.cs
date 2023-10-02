using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RateOfFire : MonoBehaviour
{
    private InventorySystem PlayerInventory;
    [SerializeField] private MaterialUIController MaterialsUi;
    [SerializeField] private ShowInfoMainGun InfoMainGun;

    [SerializeField] private Button DoButton;
    [SerializeField] private Text DoButtonText;

    [SerializeField] private Button RemoveButton;
    [SerializeField] private TextMeshProUGUI RemoveButtonText;

    [SerializeField] private TextMeshProUGUI Levels;

    [SerializeField] private TextMeshProUGUI Increase;
    [SerializeField] private int IncreaseInt = 5;



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
    public void CheckUprageRateOfFire()
    {
        if (PlayerInventory.mainGuns[weaponNum].GetComponent<ItemObject>().levelRateOfFire == PlayerInventory.mainGuns[weaponNum].GetComponent<ItemObject>().maxLevelRateOfFire)
        {
            DoButton.GetComponent<Button>().enabled = false;
            DoButtonText.color = new Color(0.3f, 0.3f, 0.3f, 1);

            RemoveButton.GetComponent<Button>().enabled = true;
            RemoveButtonText.color = new Color(1, 1, 1, 1);
        }
        else
        {
            if (PlayerInventory.fuel - fuel < 0 || PlayerInventory.cloth - cloth < 0 || PlayerInventory.metal - metal < 0 ||
                PlayerInventory.plastic - plastic < 0 || PlayerInventory.chemical - chemical < 0 || PlayerInventory.wires - wires < 0)
            {
                if (PlayerInventory.mainGuns[weaponNum].GetComponent<ItemObject>().levelRateOfFire > 0)
                {
                    DoButton.GetComponent<Button>().enabled = false;
                    DoButtonText.color = new Color(0.3f, 0.3f, 0.3f, 1);

                    RemoveButton.GetComponent<Button>().enabled = true;
                    RemoveButtonText.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    DoButton.GetComponent<Button>().enabled = false;
                    DoButtonText.color = new Color(0.3f, 0.3f, 0.3f, 1);

                    RemoveButton.GetComponent<Button>().enabled = false;
                    RemoveButtonText.color = new Color(0.3f, 0.3f, 0.3f, 1);
                }
            }
            else if (PlayerInventory.mainGuns[weaponNum].GetComponent<ItemObject>().levelRateOfFire == 0)
            {
                DoButton.GetComponent<Button>().enabled = true;
                DoButtonText.color = new Color(1, 1, 1, 1);

                RemoveButton.GetComponent<Button>().enabled = false;
                RemoveButtonText.color = new Color(0.3f, 0.3f, 0.3f, 1);
            }
            else
            {
                DoButton.GetComponent<Button>().enabled = true;
                DoButtonText.color = new Color(1, 1, 1, 1);

                RemoveButton.GetComponent<Button>().enabled = true;
                RemoveButtonText.color = new Color(1, 1, 1, 1);
            }
        }

        Levels.text = PlayerInventory.mainGuns[weaponNum].GetComponent<ItemObject>().levelRateOfFire.ToString() + " / " + PlayerInventory.mainGuns[weaponNum].GetComponent<ItemObject>().maxLevelRateOfFire.ToString();
        Increase.text = "+" + IncreaseInt.ToString();

    }

    public void UpgraderateOfFire()
    {
        Debug.Log("up");
        PlayerInventory.fuel -= fuel;
        PlayerInventory.cloth -= cloth;
        PlayerInventory.metal -= metal;
        PlayerInventory.plastic -= plastic;
        PlayerInventory.chemical -= chemical;
        PlayerInventory.wires -= wires;

        PlayerInventory.mainGuns[weaponNum].GetComponent<ItemObject>().levelRateOfFire += 1;
        MaterialsUi.UpdateMaterialsUI();
        PlayerInventory.mainGuns[weaponNum].GetComponent<ItemObject>().rateOfFire += IncreaseInt;
        CheckUprageRateOfFire();
        InfoMainGun.UpdateInfo();

    }

    public void ReducerateOfFire()
    {
        PlayerInventory.fuel += fuel;
        PlayerInventory.cloth += cloth;
        PlayerInventory.metal += metal;
        PlayerInventory.plastic += plastic;
        PlayerInventory.chemical += chemical;
        PlayerInventory.wires += wires;

        PlayerInventory.mainGuns[weaponNum].transform.Find("aim").gameObject.SetActive(false);

        DoButton.GetComponent<Button>().enabled = true;
        DoButtonText.color = new Color(1, 1, 1, 1);

        RemoveButton.GetComponent<Button>().enabled = false;
        RemoveButtonText.color = new Color(0.3f, 0.3f, 0.3f, 1);

        PlayerInventory.mainGuns[weaponNum].GetComponent<ItemObject>().levelRateOfFire -= 1;

        MaterialsUi.UpdateMaterialsUI();

        PlayerInventory.mainGuns[weaponNum].GetComponent<ItemObject>().rateOfFire -= IncreaseInt;
        CheckUprageRateOfFire();
        InfoMainGun.UpdateInfo();

    }
}
