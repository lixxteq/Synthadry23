using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeInHand : MonoBehaviour
{

    public Transform ItemPoint;
    public float lerpSpeed;
    private GameObject player;
    private List<GameObject> mainGuns;
    private GameObject activeGunGameObject;
    private Animator playerAnimator;


    void Start()
    {
        player = gameObject;
        mainGuns = player.GetComponent<InventorySystem>().mainGuns;
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    public void takeMainGun(int activeGun, float endWeight = 1)
    {
        try
        {

            foreach (GameObject gun in mainGuns)
            {
                gun.SetActive(false);
            }
            activeGunGameObject = mainGuns[activeGun];
            SetAnimation(activeGunGameObject.GetComponent<ItemObject>().itemStat.name, endWeight);
            activeGunGameObject.transform.SetParent(ItemPoint);
            activeGunGameObject.transform.localPosition = activeGunGameObject.GetComponent<ItemObject>().itemStat.positionOffset;
            activeGunGameObject.transform.localRotation = Quaternion.Euler(activeGunGameObject.GetComponent<ItemObject>().itemStat.rotationOffset);
            activeGunGameObject.SetActive(true);
        }
        catch
        {
            ClearHands();
        }
    }

    void ClearLayersWeight()
    {
        for (int i = 1; i < playerAnimator.layerCount; i++)
        {
            playerAnimator.SetLayerWeight(i, 0);
        }
    }

    void SetAnimation(ItemSO.Name itemName, float endWeight)
    {
        try
        {
            switch (itemName.ToString())
            {
                case "ak":
                    ClearLayersWeight();
                    StartCoroutine(LerpSetWeight(1, endWeight));
                    break;
                case "revolver":
                    ClearLayersWeight();
                    StartCoroutine(LerpSetWeight(2, endWeight));
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
    
    public void ClearHands()
    {
        ClearLayersWeight();
    }

}
