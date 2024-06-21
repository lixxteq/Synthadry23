using UnityEngine;
using System.Collections.Generic;
using EPOOutline;

[RequireComponent(typeof(Outlinable))]
public class ComponentsObject : MonoBehaviour
{
    [SerializeField] private int minAmount = 1;
    [SerializeField] private int maxAmount = 10;
    public Outlinable outlinable;
    

    public List<Component> componentStat;

    public List<int> amount;


    private void Start()
    {
        for (var i = 0; i < componentStat.Count; i++)
        {
            amount.Add(Random.Range(minAmount, maxAmount));
        }

	    outlinable = GetComponent<Outlinable>();
        outlinable.enabled = false;
        outlinable.DrawingMode = OutlinableDrawingMode.Normal;
        outlinable.OutlineLayer = 24;
        outlinable.OutlineParameters.Enabled = true;
        outlinable.OutlineParameters.Color = Color.gray;
	    outlinable.OutlineParameters.FillPass.Shader = Resources.Load<Shader>("Easy performant outline/Shaders/Fills/ColorFill");
	    outlinable.OutlineParameters.FillPass.SetColor("_PublicColor", new Color32(128, 128, 128, 51));
    }
}
