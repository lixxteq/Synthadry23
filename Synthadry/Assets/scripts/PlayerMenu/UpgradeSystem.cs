using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{


    [SerializeField] private InventorySystem PlayerInventory;

    [SerializeField] private List<GameObject> UpgradesUi;

    [SerializeField] private List<GameObject> BacksUi;

    public void ShowUpgrades(int num)
    {
        for (var i = 0; i < UpgradesUi.Count; i++)
        {
            UpgradesUi[i].SetActive(false);
            BacksUi[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
        Debug.Log(num);
        UpgradesUi[num].SetActive(true);
        BacksUi[num].GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
    }

    private void OnEnable()
    {
        for (var i = 0; i < UpgradesUi.Count; i++)
        {
            UpgradesUi[i].SetActive(false);
            BacksUi[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
