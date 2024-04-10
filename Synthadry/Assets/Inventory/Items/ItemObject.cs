using TMPro;
using UnityEngine;
using System;
using UnityEditor;
// using UnityEditor.ShaderGraph.Drawing;
using System.Collections;
using System.Xml.Serialization;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class ItemObject : MonoBehaviour
{
    public ItemSO itemStat;
    private GameObject player;
    private GameObject canvas;
    private WeaponSlotManager weaponSlotManager;

    [Header("0 - 100")]
    public float damage;
    public float rateOfFire; //��������� � �������


    public int currentAmmo = 5;
    public int allAmmo = 20;

    [Header("���������")]
    public float range = 50f;

    [Header("����������������")]
    public int maxLevelRateOfFire = 5;
    public int levelRateOfFire = 0;


    [Header("����")]
    public int maxLevelDamage = 5;
    public int levelDamage = 0;

    public bool isWeapon;

    [Header("�������� � �������� (0 - 99)")]
    public int maximumAmmo; //� ��������/��������

    [Header("������ � ��������")]
    public int maxLevelAmmo = 5;
    public int levelAmmo = 0;

    [Header("�����")]
    public TextMeshProUGUI allAmmoInGameUi;
    public TextMeshProUGUI currentAmmoInGameUi;

    [Header("��������")]
    public GameObject bulletPref;
    public Vector3 bulletRotation;
    public Vector3 bulletOutForce;
    public int bulletAlive;
    public Transform bulletSpawnPoint;
    public AudioSource shootSound;

    public ParticleSystem flameFx;
    public ParticleSystem smokeFx;
    public ParticleSystem muzzleFlashFx;


    [Header("������")]
    public GameObject lantern;
    public GameObject light;
    public GameObject aim;

    private ItemsIK itemsIK;

    private bool lanternEnabled = false;
    private Camera mainCamera;


    private void OnEnable()
    {
        StartCoroutine("Attack");
        if (light.GetComponent<Light>().enabled)
        {
            lanternEnabled = true;
        } else
        {
            lanternEnabled = false;
        }

    }

    private void OnDisable()
    {
        StopCoroutine("Attack");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        itemsIK = player.GetComponent<ItemsIK>();
        weaponSlotManager = GameObject.FindGameObjectWithTag("WeaponSlot").GetComponent<WeaponSlotManager>();
        mainCamera = Camera.main;
    }


    void Reload()
    {
        allAmmo = allAmmo + currentAmmo;
        currentAmmo = 0;
        if (allAmmo - maximumAmmo > 0)
        {
            currentAmmo = maximumAmmo;
            allAmmo -= currentAmmo;
        }
        else
        {
            currentAmmo = allAmmo;
            allAmmo = 0;
        }
        weaponSlotManager.ChangeActiveWeapon(this);
    }

    public void Shoot()
    {
        bool onlyOneHit = true;
        
        if (currentAmmo > 0)
        {
           // itemsIK.SetIKPositionShoot(gameObject);
            


            Debug.Log("пиу");

            if (flameFx) flameFx.Play();
            if (smokeFx) smokeFx.Play();
            if (muzzleFlashFx) muzzleFlashFx.Play();

            if (shootSound) shootSound.Play(0);
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            bool isHit = Physics.Raycast(ray, out hit, range);
            if (isHit && onlyOneHit)
            {
                onlyOneHit = false;
                Collider hitObject = hit.collider;
                Debug.Log(hitObject);

                if (hitObject.CompareTag("Enemy"))
                {
                    if (hitObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
                    {
                        enemyHealth.GetDamage(damage);
                    };
                }

                if (hitObject.CompareTag("EnemyHead"))
                {
                    if (hitObject.transform.parent.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
                    {
                        enemyHealth.GetDamage((float)(damage * 1.25));
                    };
                }

            }
            currentAmmo -= 1;
            //SpawnBullet();

            weaponSlotManager.ChangeActiveWeapon(this);
            itemsIK.Recoil(itemStat, 60 / (rateOfFire * itemStat.recoilSpeedMultiplier));
        }
        else
        {
            //���� ������ ��� ����������� (����� ������)
        }


    }

    void ToggleLantern()
    {
        if (lanternEnabled)
        {
            lanternEnabled = false;
            light.GetComponent<Light>().enabled = true;
        } else
        {
            lanternEnabled = true;
            light.GetComponent<Light>().enabled = false;
        }
    }

    void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPref, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.up * 500f;
        
        Destroy(bullet, bulletAlive);

    }

    public void CheckItemParams()
    {
        ItemSO.Type type = itemStat.type;
        switch (type)
        {
            case ItemSO.Type.firearms:
                if (currentAmmo > 0)
                {
                    Shoot();
                }
                break;
            case ItemSO.Type.coldWeapons:
                if (itemStat.name is ItemSO.Name.flashlight)
                {
                    ToggleLantern();
                }

                break;

            default:
                break;
        }


    }

    private IEnumerator Attack()
    {

        while (true)
        {

            if (Input.GetMouseButton(0) && player.GetComponent<InventorySystem>().mainGuns.IndexOf(gameObject) != -1)
            {
                Debug.Log("IEnumerator Attack");
                CheckItemParams();

                yield return new WaitForSeconds(60 / rateOfFire);
            }



            yield return new WaitForEndOfFrame();
        }
    }

    public void AddLantern()
    {
        lantern.SetActive(true);
    }

    public void RemoveLantern()
    {
        lantern.SetActive(false);
    }

    public void AddAim()
    {
        aim.SetActive(true);
    }

    public void RemoveAim()
    {
        aim.SetActive(false);
    }




    public void IncreaseRateOfFire(float num)
    {
        rateOfFire = Math.Min(100, rateOfFire + num);
    }

    public void DecreaseRateOfFire(float num)
    {
        rateOfFire = Math.Max(0, rateOfFire - num);
    }

    public void IncreaseDamage(float num)
    {
        damage = Math.Min(100, damage + num);
    }

    public void DecreaseRateDamage(float num)
    {
        damage = Math.Max(0, damage - num);
    }

    public void IncreaseMaxAmmo(int num)
    {
        maximumAmmo = Math.Min(1, maximumAmmo + num);
    }

    public void DecreaseMaxAmmo(int num)
    {
        maximumAmmo = Math.Max(0, maximumAmmo - num);
    }

    public void IncreaseAllAmmo(int num)
    {
        allAmmo = Math.Min(1000, allAmmo + num);
    }

    public void DecreaseAllAmmo(int num)
    {
        allAmmo = Math.Max(0, allAmmo - num);

    }

    public void IncreaseCurrentAmmo(int num)
    {
        currentAmmo = Math.Min(maximumAmmo, currentAmmo + num);
    }

    public void DecreaseCurrentAmmo(int num)
    {
        currentAmmo = Math.Max(0, currentAmmo - num);

    }
}