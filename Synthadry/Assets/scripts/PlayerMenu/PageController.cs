using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    [SerializeField] private List<GameObject> Pages;
/*
    0 - карта
    1 - инвентарь
    2 - модификация
 */

    private void OnEnable()
    {
        foreach (GameObject page in Pages)
        {
            page.SetActive(false);
        }
        Pages[0].SetActive(true);
    }



    public void LoadPage(int num)
    {
        foreach (GameObject page in Pages)
        {
            page.SetActive(false);
        }
        Pages[num].SetActive(true);
    }
}
