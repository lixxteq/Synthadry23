using UnityEngine.UI;
using UnityEngine;

public class HeadlightSystem : MonoBehaviour
{
    public GameObject _headLight;
    public float _maxEnergy = 100f;
    public float _currentEnergy = 100f;
    public float _energyLostPerFixedUpdate = 5f;
    private InventorySystem inventorySystem;
    public GameObject _energyUIBar;

    private void Start()
    {
        inventorySystem = GetComponent<InventorySystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (inventorySystem.haveHeadlight)
            {
                if (_headLight.activeInHierarchy)
                {
                    _headLight.SetActive(false);
                    _energyUIBar.SetActive(false);
                }
                else
                {
                    if (_currentEnergy > 0)
                    {
                        _headLight.SetActive(true);
                        _energyUIBar.SetActive(true);
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (_headLight.activeInHierarchy)
        {
            _currentEnergy = Mathf.Max(_currentEnergy - _energyLostPerFixedUpdate, 0);
            Image _UIBar = _energyUIBar.transform.GetChild(1).GetComponent<Image>();
            _UIBar.fillAmount = _currentEnergy / 100;
            if (_currentEnergy <= 0)
            {
                _headLight.SetActive(false);
            }
        }
    }
}
