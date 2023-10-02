using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private List<GameObject> headLight;
    [SerializeField] private GameObject headLight3d;

    [SerializeField] private List<GameObject> rearLight;
    [SerializeField] private GameObject rearLight3d;

/*    [SerializeField] private List<GameObject> topLight;
    [SerializeField] private List<GameObject> topLight3d;*/

    [SerializeField] private GameObject player;

    [SerializeField] private int phaseCount = 3;

    public int phase = 0;

/*    Например с балкой:
        0 - всё выкл
        1 - только ближний
        2 - ближний + балка*/


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!player.activeInHierarchy)
            {
                phase = (phase + 1) % phaseCount;
            }
            phaseController(phase);

        }
    }

    void phaseController(int num)
    {
        switch (num)
        {
            case 0:
                for (var i = 0; i < headLight.Count; i++)
                {
                    headLight[i].SetActive(false);
                    rearLight[i].SetActive(false);
                }
                headLight3d.SetActive(false);
                rearLight3d.SetActive(false);
                break;

            case 1:
                for (var i = 0; i < headLight.Count; i++)
                {
                    headLight[i].SetActive(true);
                    rearLight[i].SetActive(true);
                }
                headLight3d.SetActive(true);
                rearLight3d.SetActive(true);
                break;
/*            case 2:
                if (topLight.Count > 0)
                {
                    for (var i = 0; i < topLight.Count; i++)
                    {
                        topLight[i].SetActive(true);
                        topLight3d[i].SetActive(true);
                    }
                }
                break;*/

        }
    }
}
