using System.Collections;
using System.Collections.Generic;
using EPOOutline;
using UnityEngine;

[RequireComponent(typeof(Outlinable))]
public class BuffObject : MonoBehaviour
{
    public Outlinable outlinable;
    public Buffs BuffStat;
    public int amount;

    private void Start() {
	    outlinable = GetComponent<Outlinable>();
        outlinable.enabled = false;
        outlinable.DrawingMode = OutlinableDrawingMode.Normal;
        outlinable.OutlineLayer = 21;
        outlinable.OutlineParameters.Enabled = true;
        outlinable.OutlineParameters.Color = BuffStat.type == Buffs.Types.hp ? Color.red : BuffStat.type == Buffs.Types.armor ? Color.yellow : Color.blue;
	    outlinable.OutlineParameters.FillPass.Shader = Resources.Load<Shader>("Easy performant outline/Shaders/Fills/ColorFill");
	    outlinable.OutlineParameters.FillPass.SetColor("_PublicColor", BuffStat.type == Buffs.Types.hp ? new Color32(255, 0, 0, 51) : BuffStat.type == Buffs.Types.armor ? new Color32(255, 255, 0, 51) : new Color32(0, 0, 255, 51));
    }
}
