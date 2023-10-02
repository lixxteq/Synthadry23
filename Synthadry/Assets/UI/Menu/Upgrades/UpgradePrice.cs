using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePrice : MonoBehaviour
{

    public enum Types
    {
        fuel,
        cloth,
        metal,
        plastic,
        chemical,
        wires
    };

    public List<int> prices;
    public List<Types> typeOfMaterials;

    [SerializeField] private List<TextMeshProUGUI> pricesUi;

    public void UpdatePricesUi()
    {
        for (var i = 0; i < prices.Count; i++)
        {
            pricesUi[i].text = prices[i].ToString();
        }
    } 
}
