using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourcesSlotManager : MonoBehaviour
{
    public TextMeshProUGUI fuelCount;
    public TextMeshProUGUI clothCount;
    public TextMeshProUGUI metalCount;
    public TextMeshProUGUI plasticCount;
    public TextMeshProUGUI chemicalCount;
    public TextMeshProUGUI wiresCount;

    public TextMeshProUGUI batteriesCount;
    public Image batteryFillImage;
    public TextMeshProUGUI skillPointsCount;

    public void DrawResourcesSlot(int fuel, int cloth, int metal, int plastic, int chemical, int wires, int batteries = 0, int skillPoints = 0)
    {
        fuelCount.text = fuel.ToString();
        clothCount.text = cloth.ToString();
        metalCount.text = metal.ToString();
        plasticCount.text = plastic.ToString();
        chemicalCount.text = chemical.ToString();
        wiresCount.text = wires.ToString();
        batteriesCount.text = batteries.ToString();
        skillPointsCount.text = skillPoints.ToString();
    }

    public void DrawBatteryEnergy(float batteryFill)
    {
        batteryFillImage.fillAmount = batteryFill;
    }

}
