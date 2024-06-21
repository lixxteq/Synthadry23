using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAsyncPlay : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject mainMenu;
    public void Load()
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("First Scene");
        MainMenuLoadingScreenManager loadingScreenManager = loadingScreen.GetComponent<MainMenuLoadingScreenManager>();
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            loadingScreenManager.SetLoadBarProgress(asyncLoad.progress * 100);
            yield return null;
        }
    }
}
