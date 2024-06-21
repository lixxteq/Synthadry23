using UnityEngine;
using EPOOutline;

[RequireComponent(typeof(Outlinable))]
public class EnemyController : MonoBehaviour
{
    public EnemySO enemyStat;

    private EnemyHealth enemyHealth;
    private EnemyDamage enemyDamage;
    private Outlinable outlinable;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyDamage = GetComponent<EnemyDamage>();

        outlinable = GetComponent<Outlinable>();
        outlinable.enabled = false;
        outlinable.DrawingMode = OutlinableDrawingMode.Normal;
        outlinable.OutlineLayer = 11;
        outlinable.OutlineParameters.Enabled = true;
        outlinable.OutlineParameters.Color = Color.red;
        outlinable.OutlineParameters.FillPass.Shader = Resources.Load<Shader>("Easy performant outline/Shaders/Fills/ColorFill");
	    outlinable.OutlineParameters.FillPass.SetColor("_PublicColor", new Color32(255, 0, 0, 51));
    }
}
