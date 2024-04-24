using TMPro;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public GameObject skillsPage;
    public TextMeshProUGUI skillsButtonText;

    public GameObject weaponPage;
    public TextMeshProUGUI weaponButtonText;

    public GameObject inventoryPage;
    public TextMeshProUGUI inventoryButtonText;

    public GameObject mapPage;
    public TextMeshProUGUI mapButtonText;





    /*public enum Pages
    {
        Skills,
        Weapon,
        Inventory,
        Map
    }*/


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void HideAllPages()
    {
        skillsPage.SetActive(false);
        weaponPage.SetActive(false);
        inventoryPage.SetActive(false);
        mapPage.SetActive(false);
    }
    public void Test()
    {
        Debug.Log("123");
    }


    /*public void OpenPage(Pages page)
     {
         switch (page)
         {
             case Pages.Skills:
                 HideAllPages();
                 skillsPage.SetActive(true);
                 break;
             case Pages.Weapon:
                 HideAllPages();
                 weaponPage.SetActive(true);
                 break;
             case Pages.Inventory:
                 HideAllPages();
                 inventoryPage.SetActive(true);
                 break;
             case Pages.Map:
                 HideAllPages();
                 mapPage.SetActive(true);
                 break;
             default:
                 break;
         }

     }
    */

    private void OnEnable()
    {
        OpenPage("inventory");
    }

    void TintAllButtons()
    {
        skillsButtonText.color = Color.gray;
        weaponButtonText.color = Color.gray;
        inventoryButtonText.color = Color.gray;
        mapButtonText.color = Color.gray;
    }

    public void OpenPage(string page)
    {
        switch (page)
        {
            case "skills":
                HideAllPages();
                TintAllButtons();
                skillsPage.SetActive(true);
                skillsButtonText.color = Color.white;
                break;
            case "weapon":
                HideAllPages();
                TintAllButtons();
                weaponButtonText.color = Color.white;
                weaponPage.SetActive(true);
                break;
            case "inventory":
                HideAllPages();
                TintAllButtons();
                inventoryButtonText.color = Color.white;
                inventoryPage.SetActive(true);
                break;
            case "map":
                HideAllPages();
                TintAllButtons();
                mapButtonText.color = Color.white;
                mapPage.SetActive(true);
                break;
            default:
                break;
        }

    }
}
