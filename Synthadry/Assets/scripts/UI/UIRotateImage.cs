using UnityEngine;

public class UIRotateImage : MonoBehaviour
{
    public float rotationSpeed;

    private RectTransform rectTransform;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.Rotate(0, 0, rotationSpeed);
    }
}
