using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuffsController : MonoBehaviour
{

    [SerializeField] private List<GameObject> UiBuffs;

    public void UpdateBuffsUi(List<GameObject> hpBuffs, List<GameObject> armorBuffs, List<GameObject> speedBuffs, int activeBuff)
    {
        UiBuffs[0].SetActive(true);
        UiBuffs[1].SetActive(true);
        if (activeBuff == 0 && hpBuffs.Count != 0)
        {
            UiBuffs[0].GetComponent<Image>().sprite = hpBuffs[0].GetComponent<BuffObject>().BuffStat.iconActive1K;
            UiBuffs[1].GetComponent<TextMeshProUGUI>().text = hpBuffs.Count.ToString();
            return;

        }
        else if (activeBuff == 1 && armorBuffs.Count != 0)
        {
            UiBuffs[0].GetComponent<Image>().sprite = armorBuffs[0].GetComponent<BuffObject>().BuffStat.iconActive1K;
            UiBuffs[1].GetComponent<TextMeshProUGUI>().text = armorBuffs.Count.ToString();
            return;

        }
        else if (activeBuff == 2 && speedBuffs.Count != 0)
        {
            UiBuffs[0].GetComponent<Image>().sprite = speedBuffs[0].GetComponent<BuffObject>().BuffStat.iconActive1K;
            UiBuffs[1].GetComponent<TextMeshProUGUI>().text = speedBuffs.Count.ToString();
            return;
        }
        else
        {
            UiBuffs[0].SetActive(false);
            UiBuffs[1].SetActive(false);
        }

    }
}
