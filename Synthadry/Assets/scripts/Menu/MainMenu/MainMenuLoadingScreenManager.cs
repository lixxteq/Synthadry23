using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuLoadingScreenManager : MonoBehaviour
{
    public TextMeshProUGUI loadBarProgress;

    public void SetLoadBarProgress(float progress)
    {
        loadBarProgress.text = progress.ToString() + "%";
    }
}
