using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HPAndArmor : MonoBehaviour
{

    [SerializeField] private float hp = 100;
    [SerializeField] private float armor = 0;

    [SerializeField] private GameObject UiHpLine;
    [SerializeField] private GameObject UiArmorLine;

    void KillPlayer()
    {
        return;
    }

    void DrawUiHpArmor()
    {
        UiHpLine.GetComponent<Image>().fillAmount = Convert.ToSingle(hp / 100);
        UiArmorLine.GetComponent<Image>().fillAmount = armor / 100;
        Debug.Log(Convert.ToSingle(hp / 100));
    }

    public void TakeDamage(int damage)
    {
        int tempDamage = damage;
        int armorDamage = Math.Min((int)armor, tempDamage);
        armor = armor - armorDamage;
        tempDamage = tempDamage - armorDamage;

        if ((hp - tempDamage) >= 0)
        {
            hp = hp - tempDamage;
        } else
        {
            hp = 0;
            KillPlayer();
        }
        DrawUiHpArmor();
    }

    public void TakeHeal(int heal, string type)
    {
        if (type == "hp")
        {
            hp = Math.Min(hp + heal, 100);
        } else if (type == "armor")
        {
            armor = Math.Min(armor + heal, 100);
        } else
        {
            return;
        }
        DrawUiHpArmor();
    }

    // Start is called before the first frame update
    void Start()
    {
        DrawUiHpArmor();
    }
}
