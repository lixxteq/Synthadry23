using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//GetComponent<TextMeshProUGUI>().text

public class TargetSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject targetScript; //Canvas
    [SerializeField]
    private Transform newTarget; //pointer1, pointer2,..., pointerN

    [SerializeField] private int crossOutText = 1;

    public string newTitleText;
    public string newDescText;

    [SerializeField] private TextMeshProUGUI title;

    [SerializeField] private TextMeshProUGUI description;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            changeRightTopText();

            /*        StartCoroutine(ChangeText());*/

            newTarget.gameObject.SetActive(true);
            targetScript.GetComponent<TargetPointer>().Target = newTarget;
            Destroy(gameObject);
        }
       
    }

    /*    IEnumerator ChangeText()
        {

            yield return new WaitForSeconds(crossOutText);
            changeRightTopText();

        }*/

    void changeRightTopText()
    {
        title.text = newTitleText;
        description.text = newDescText;
    }

}
