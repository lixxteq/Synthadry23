using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Torch : MonoBehaviour
{
    [SerializeField] private GameObject UIRemaindCounter;
    [SerializeField] private int percentPerSecond;
    [SerializeField] private double currentRemaind = 100f;

    [SerializeField] private GameObject particles;

    [SerializeField] private List<GameObject> torchConditions;

    void setActiveCondition(int num)
    {
        for (var i = 0; i < torchConditions.Count; i++)
        {
            torchConditions[i].SetActive(false);
        }
        torchConditions[num].SetActive(true);
    }

    void FixedUpdate()
    {
        if (particles.activeInHierarchy && currentRemaind > 0)
        {
            currentRemaind = currentRemaind - (0.02 * percentPerSecond);
            UIRemaindCounter.GetComponent<Image>().fillAmount = Convert.ToSingle(Math.Round(currentRemaind) / 100);
            switch (currentRemaind)
            {
                case <= 0:
                    setActiveCondition(5);
                    break;

                case > 0 and < 20:
                    setActiveCondition(4);
                    break;

                case >= 20 and < 40:
                    setActiveCondition(3);
                    break;

                case >= 40 and < 60:
                    setActiveCondition(2);
                    break;

                case >= 60 and < 80:
                    setActiveCondition(1);
                    break;

                case >= 80 and <= 100:
                    setActiveCondition(0);
                    break;
            }
        }
        if (currentRemaind <= 0)
        {
            particles.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (particles.activeInHierarchy)
            {
                particles.SetActive(false);
            }
            else
            {
                if (currentRemaind > 0)
                {
                    particles.SetActive(true);
                }
            }
        }
    }

    public void addPercentages(int count)
    {
        currentRemaind = Math.Min(currentRemaind + count, 100);
        UIRemaindCounter.GetComponent<Image>().fillAmount = Convert.ToSingle(Math.Round(currentRemaind) / 100);
    }
}
