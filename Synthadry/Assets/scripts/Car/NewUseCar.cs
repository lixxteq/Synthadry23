using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class NewUseCar : MonoBehaviour
{
    [SerializeField] private GameObject car;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform outPos;
    [SerializeField] private GameObject carCamera;

    public Transform camTarget;

    [SerializeField] private GameObject aimUiPoint;


    private bool canEnter = false;

    // Start is called before the first frame update
    void Start()
    {
        car.GetComponent<CarUserControl>().enabled = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (canEnter)
            {
                if (player.activeInHierarchy)
                {
                    player.SetActive(false);
                    player.GetComponent<CustomCharacterController>().enabled = false;
                    carCamera.SetActive(true);
                    carCamera.GetComponent<CarCamera>().target = camTarget;
                    car.GetComponent<CarUserControl>().enabled = true;
                    aimUiPoint.SetActive(false);
                }
                canEnter = false;


            }
            else
            {
                player.transform.position = outPos.position;
                player.SetActive(true);
                player.GetComponent<CustomCharacterController>().enabled = true;
                carCamera.SetActive(false);
                car.GetComponent<CarUserControl>().enabled = false;
                car.SetActive(false);
                car.SetActive(true);
                canEnter = false;
                aimUiPoint.SetActive(true);

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canEnter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canEnter = false;
        }
    }
}
