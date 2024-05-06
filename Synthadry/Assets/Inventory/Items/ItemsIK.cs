using GameCreator.Runtime.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR;

public class ItemsIK : MonoBehaviour
{
    public Transform ItemPoint;
    public float lerpSpeed;
    private GameObject player;
    private List<GameObject> mainGuns;
    private GameObject activeGunGameObject;
    private Animator playerAnimator;
   // public Transform ikRightHandTarget;
   // public Transform ikRightHandHint;
    //public Transform ikLeftHandTarget;
    //public Transform ikLeftHandHint;

    private ItemSO itemInHand;
    private GameObject gameObjectInHand;
    public Transform ikHeadTarget;


    public Transform ikRightHandTarget;
    public Transform ikRightHandHint;
    public Transform ikLeftHandTarget;
    public Transform ikLeftHandHint;

    public Rig weaponIkRig;


    private WeaponSlotManager weaponSlotManager;


    void Start()
    {
        player = gameObject;
        mainGuns = player.GetComponent<InventorySystem>().mainGuns;
        playerAnimator = gameObject.GetComponent<Animator>();
        weaponSlotManager = GameObject.FindGameObjectWithTag("WeaponSlot").GetComponent<WeaponSlotManager>();
        // weaponSlotManager.gameObject.SetActive(false);
        weaponSlotManager.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        ClearHands();
    }

    public void takeMainGun(int activeGun, float endWeight = 1)
    {
        try
        {
            foreach (GameObject gun in mainGuns)
            {
                gun.SetActive(false);
            }
            StopAllCoroutines();

            activeGunGameObject = mainGuns[activeGun];
            ItemSO itemSO = activeGunGameObject.GetComponent<ItemObject>().itemStat;

            itemInHand = itemSO;
            gameObjectInHand = activeGunGameObject;

            SetIKPosition(activeGunGameObject);

            SetAnimation(itemSO, endWeight);

            activeGunGameObject.transform.SetParent(ItemPoint);

            activeGunGameObject.transform.localPosition = itemInHand.positionOffset;
            activeGunGameObject.transform.localRotation = Quaternion.Euler(itemInHand.rotationOffset);



            activeGunGameObject.SetActive(true);

            weaponSlotManager.ChangeActiveWeapon(activeGunGameObject.GetComponent<ItemObject>());
        }
        catch
        {
            ClearHands();
        }
    }

    public void SetIKPosition(GameObject activeGunGameObject)
    {

        ItemSO finalItem = activeGunGameObject.GetComponent<ItemObject>().itemStat;


        ikLeftHandHint.localPosition = finalItem.leftHandIkPositionHint;
        ikLeftHandHint.localRotation = Quaternion.Euler(finalItem.leftHandIkRotationHint);

        ikLeftHandTarget.localPosition = finalItem.leftHandIkPosition;
        ikLeftHandTarget.localRotation = Quaternion.Euler(finalItem.leftHandIkRotation);

        ikRightHandHint.localPosition = finalItem.righHandIkPositionHint;
        ikRightHandHint.localRotation = Quaternion.Euler(finalItem.righHandIkRotationHint);

        ikRightHandTarget.localPosition = finalItem.righHandIkPosition;
        ikRightHandTarget.localRotation = Quaternion.Euler(finalItem.righHandIkRotation);

        activeGunGameObject.transform.localPosition = finalItem.positionOffset;
        activeGunGameObject.transform.localRotation = Quaternion.Euler(finalItem.rotationOffset); 
        SetWeaponIKRig(1);
    }

    void SetAnimation(ItemSO item, float endWeight)
    {
        try
        {
            switch (item.name.ToString())
            {
                case "ak":
                    ClearLayersWeight();
                    StartCoroutine(LerpSetWeight(1, endWeight));
                    break;
                case "revolver":
                    ClearLayersWeight();
                    StartCoroutine(LerpSetWeight(1, endWeight));
                    break;
                case "stick":
                    ClearLayersWeight();
                    StartCoroutine(LerpSetWeight(1, endWeight));
                    break;
                case "axe":
                    ClearLayersWeight();
                    StartCoroutine(LerpSetWeight(1, endWeight));
                    break;
                case "flashlight":
                    ClearLayersWeight();
                    StartCoroutine(LerpSetWeight(1, endWeight));
                    break;
                default:
                    break;

            }
        }
        catch
        {
            ClearHands();
        }
    }

    public void SetWeaponIKRig(int weight)
    {
        weaponIkRig.weight = weight;
    }

    public void ClearHands()
    {
        ClearLayersWeight();
        SetWeaponIKRig(0);
        itemInHand = null;
        gameObjectInHand = null;
    }

    IEnumerator LerpSetWeight(int layerIndex, float endWeight)
    {
        float timeToStart = Time.time;
        float targetValue = endWeight;
        while (playerAnimator.GetLayerWeight(layerIndex) != targetValue)
        {
            playerAnimator.SetLayerWeight(layerIndex, Mathf.Lerp(playerAnimator.GetLayerWeight(layerIndex), endWeight, (Time.time - timeToStart) * lerpSpeed));

            yield return null;
        }
    }

    void ClearLayersWeight()
    {
        for (int i = 1; i < playerAnimator.layerCount; i++)
        {
            playerAnimator.SetLayerWeight(i, 0);
        }
    }


    public void Recoil(ItemSO itemStat, float duration)
    {
        StartCoroutine(RecoilBack(itemStat, duration));
    }

    Vector3 GenerateRecoilVector(float multiplier)
    {
        Vector3 recoilRotation;
        recoilRotation.x = Random.Range(-1.0f, 1.0f) * multiplier;
        recoilRotation.y = Random.Range(-1.0f, 1.0f) * multiplier;
        recoilRotation.z = 0;

        return recoilRotation;
    }

    IEnumerator RecoilBack(ItemSO itemStat, float duration)
    {
        float time = 0;
        Vector3 startPositionLeftHand = ikLeftHandTarget.localPosition;
        Vector3 startPositionRightHand = ikRightHandTarget.localPosition;

        Quaternion startRotationLeftHand = ikLeftHandTarget.localRotation;
        Quaternion startRotationRightHand = ikRightHandTarget.localRotation;
        Vector3 targetPositionLeftHand, targetPositionRightHand;
        Vector3 targetRotationRightHand, targetRotationLeftHand;



        if (!itemStat.hasSecondAttack) {
            targetPositionLeftHand = itemStat.leftHandIkPositionShoot;
            targetPositionRightHand = itemStat.righHandIkPositionShoot;

            targetRotationRightHand = itemStat.righHandIkRotationShoot;
            targetRotationLeftHand = itemStat.leftHandIkRotationShoot;
        }
        else
        {
            if (Random.Range(-1f, 1f) > 0)
            {
                targetPositionLeftHand = itemStat.leftHandIkPositionShoot;
                targetPositionRightHand = itemStat.righHandIkPositionShoot;

                targetRotationRightHand = itemStat.righHandIkRotationShoot;
                targetRotationLeftHand = itemStat.leftHandIkRotationShoot;
            } else
            {
                targetPositionLeftHand = itemStat.leftHandIkPositionShootSecond;
                targetPositionRightHand = itemStat.righHandIkPositionShootSecond;

                targetRotationRightHand = itemStat.righHandIkRotationShootSecond;
                targetRotationLeftHand = itemStat.leftHandIkRotationShootSecond;
            }
        }


        Vector3 startIkHeadTarget = ikHeadTarget.localPosition;
        Vector3 targetIkHeadTarget = startIkHeadTarget + GenerateRecoilVector(itemStat.cameraShakeMultiplier);


        while (time < duration)
        {
            ikLeftHandTarget.localPosition = Vector3.Lerp(startPositionLeftHand, targetPositionLeftHand, time / duration);
            ikRightHandTarget.localPosition = Vector3.Lerp(startPositionRightHand, targetPositionRightHand, time / duration);

            ikLeftHandTarget.localRotation = Quaternion.Lerp(startRotationLeftHand, Quaternion.Euler(targetRotationLeftHand), time / duration);
            ikRightHandTarget.localRotation = Quaternion.Lerp(startRotationRightHand, Quaternion.Euler(targetRotationRightHand), time / duration);

            ikHeadTarget.localPosition = Vector3.Lerp(startIkHeadTarget, targetIkHeadTarget, time / duration);


            time += Time.deltaTime;
            yield return null;
        }
        ikLeftHandTarget.localPosition = targetPositionLeftHand;
        ikRightHandTarget.localPosition = targetPositionRightHand;
        ikLeftHandTarget.localRotation = Quaternion.Euler(itemStat.leftHandIkRotationShoot);
        ikRightHandTarget.localRotation = Quaternion.Euler(itemStat.righHandIkRotationShoot);

        ikHeadTarget.localPosition = targetIkHeadTarget;

        yield return StartCoroutine(RecoilForward(itemStat, duration));
    }

    IEnumerator RecoilForward(ItemSO itemStat, float duration)
    {
        float time = 0;
        Vector3 startPositionLeftHand = ikLeftHandTarget.localPosition;
        Vector3 startPositionRightHand = ikRightHandTarget.localPosition;

        Quaternion startRotationLeftHand = ikLeftHandTarget.localRotation;
        Quaternion startRotationRightHand = ikRightHandTarget.localRotation;

        Vector3 targetPositionLeftHand = itemStat.leftHandIkPosition;
        Vector3 targetPositionRightHand = itemStat.righHandIkPosition;

        Vector3 startIkHeadTarget = ikHeadTarget.localPosition;
        Vector3 targetIkHeadTarget = new Vector3(0, 0, 14);

        while (time < duration)
        {
            ikLeftHandTarget.localPosition = Vector3.Lerp(startPositionLeftHand, targetPositionLeftHand, time / duration);
            ikRightHandTarget.localPosition = Vector3.Lerp(startPositionRightHand, targetPositionRightHand, time / duration);

            ikLeftHandTarget.localRotation = Quaternion.Lerp(startRotationLeftHand, Quaternion.Euler(itemStat.leftHandIkRotation), time / duration);
            ikRightHandTarget.localRotation = Quaternion.Lerp(startRotationRightHand, Quaternion.Euler(itemStat.righHandIkRotation), time / duration);

            ikHeadTarget.localPosition = Vector3.Lerp(startIkHeadTarget, targetIkHeadTarget, time / duration);


            time += Time.deltaTime;
            yield return null;
        }
        ikLeftHandTarget.localPosition = targetPositionLeftHand;
        ikRightHandTarget.localPosition = targetPositionRightHand;
        ikLeftHandTarget.localRotation = Quaternion.Euler(itemStat.leftHandIkRotation);
        ikRightHandTarget.localRotation = Quaternion.Euler(itemStat.righHandIkRotation);

        ikHeadTarget.localPosition = targetIkHeadTarget;

    }

}
