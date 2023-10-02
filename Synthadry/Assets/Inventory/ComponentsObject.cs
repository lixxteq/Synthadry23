using UnityEngine;
using System.Collections.Generic;


public class ComponentsObject : MonoBehaviour
{
    [SerializeField] private int minAmount = 1;
    [SerializeField] private int maxAmount = 10;
    

    public List<Component> componentStat;

    public List<int> amount;


    private void Start()
    {
        for (var i = 0; i < componentStat.Count; i++)
        {
            amount.Add(Random.Range(minAmount, maxAmount));
        }
    }
}
