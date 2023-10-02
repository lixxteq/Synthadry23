using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemySpawn : MonoBehaviour
{
    private LightDetectionManager m_lightDetectionManager;

    private float m_timerForSpawn = 0f;

    private List<GameObject> m_enemies = new List<GameObject>();

    private int m_currentMaxEnemiesCount;


    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private float frequencyOfSpawn;

    [SerializeField]
    private int maxEnemiesCount;

    [SerializeField]
    private int addEnemySeconds;

    void Start()
    {
        m_lightDetectionManager = GetComponent<LightDetectionManager>();
        m_timerForSpawn = 0f;
    }

    
    void Update()
    {
        m_timerForSpawn += Time.deltaTime;

        ChangeCurrentMaxEnemies();
        SpawnLogic();  
    }

    private void SpawnLogic()
    {
        if (m_timerForSpawn >= frequencyOfSpawn)
        {
            if (m_enemies.Count < m_currentMaxEnemiesCount)
                m_enemies.Add(Instantiate(enemy, transform.position, Quaternion.identity, null));
            m_timerForSpawn = 0f;
        }
    }

    private void ChangeCurrentMaxEnemies()
    {
        int currentEnemiesMaximum = (int)(m_lightDetectionManager.darknessTimer / addEnemySeconds);
        if (currentEnemiesMaximum > maxEnemiesCount)
            m_currentMaxEnemiesCount = maxEnemiesCount;
        else
            m_currentMaxEnemiesCount = currentEnemiesMaximum;
    }
}
