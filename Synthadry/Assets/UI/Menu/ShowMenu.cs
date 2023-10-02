using UnityEngine;

public class ShowMenu : MonoBehaviour
{

    [SerializeField] private GameObject PlayerMenuCanvas;
    /*[SerializeField] private GameObject ExtraPlayerMenuCanvas;*/

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerMenuCanvas.SetActive(true);
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
}
