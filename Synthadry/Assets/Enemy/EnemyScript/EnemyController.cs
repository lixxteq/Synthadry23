using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemySO enemyStat;

    private EnemyHealth enemyHealth;
    private EnemyDamage enemyDamage;

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyDamage = GetComponent<EnemyDamage>();
    }
}
