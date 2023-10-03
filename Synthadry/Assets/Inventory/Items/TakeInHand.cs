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

    public void takeMainGun(int activeGun)
    {
        try
        {

            foreach (GameObject gun in mainGuns)
            {
                gun.SetActive(false);
            }
            activeGunGameObject = mainGuns[activeGun];
            SetAnimation(activeGunGameObject.GetComponent<ItemObject>().itemStat.name);
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

    void SetAnimation(ItemSO.Name itemName)
    {
        try
        {
            switch (itemName.ToString())
            {
                case "ak":
                    ClearLayersWeight();
                    StartCoroutine(LerpSetWeight(1));
                    break;
                case "revolver":
                    ClearLayersWeight();
                    StartCoroutine(LerpSetWeight(2));
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

    IEnumerator LerpSetWeight(int layerIndex)
    {
        float timeToStart = Time.time;
        while (playerAnimator.GetLayerWeight(layerIndex) != 1f)
        {
            playerAnimator.SetLayerWeight(layerIndex, Mathf.Lerp(playerAnimator.GetLayerWeight(layerIndex), 1, (Time.time - timeToStart) * lerpSpeed)); //Here speed is the 1 or any number which decides the how fast it reach to one to other end.

            yield return null;
        }
    }
    
    public void ClearHands()
    {
        ClearLayersWeight();
    }

}
