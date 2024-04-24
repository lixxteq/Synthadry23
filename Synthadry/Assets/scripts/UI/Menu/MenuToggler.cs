using UnityEngine;

public class MenuToggler : MonoBehaviour
{
    private GameObject canvas;
    private GameObject menu;
    private float fixedDeltaTime;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        menu = transform.Find("Menu").gameObject;
    }

    private void Awake()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) //поменять на ESC после тестов
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        if (menu.activeInHierarchy) { 
            menu.SetActive(false);
            canvas.transform.Find("Main").gameObject.SetActive(true);
            Time.timeScale = 1f;
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
            Cursor.lockState = CursorLockMode.Locked;

        }
        else
        {
            int children = transform.childCount;
            for (int i = 0; i < children; ++i)
                transform.GetChild(i).gameObject.SetActive(false);  
            canvas.SetActive(true);
            menu.SetActive(true);
            Time.timeScale = 0.001f;
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
            Cursor.lockState = CursorLockMode.Confined;

        }





    }
    
}
