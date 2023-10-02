using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;
using UnityEngine.UI;

public class CustomTooltip : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private GameObject pointerParent;


    [SerializeField] private Sprite image;
    [SerializeField] private Vector2 imageSize =  new Vector2(40, 40);
    [SerializeField] private float lerpTime = 50;

    private GameObject newPointer;

    void Start()
    {

    }
    void Update()
    {
        if (newPointer != null)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(gameObject.transform.position);
            if (screenPos.z < 0)
            {
                newPointer.SetActive(false);
            } else
            {
                newPointer.SetActive(true);
                newPointer.transform.position = Vector3.Lerp(newPointer.transform.position, screenPos, lerpTime * Time.deltaTime);
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            if (newPointer == null)
            {
                newPointer = new GameObject("pointer");
                newPointer.transform.SetParent(pointerParent.transform);

                newPointer.transform.position = new Vector3(0, 0, 0);

                newPointer.AddComponent<Image>();
                newPointer.GetComponent<RectTransform>().sizeDelta = imageSize;
                newPointer.GetComponent<Image>().sprite = image;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(newPointer);
        }
    }

    void OnDestroy()
    {
        Destroy(newPointer);
    }
}
