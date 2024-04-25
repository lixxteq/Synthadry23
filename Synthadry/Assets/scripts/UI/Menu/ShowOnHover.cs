using UnityEngine;
using UnityEngine.EventSystems;

public class ShowOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject objectToShow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.activeInHierarchy)
        {
            objectToShow.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.activeInHierarchy)
        {
            objectToShow.SetActive(false);
        }
    }



    public void OnMouseOver()
    {

    }

    public void OnMouseExit()
    {

    }
}
