using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadAsync : MonoBehaviour
{
    public string futureScene;
    public Image loadFill;

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(futureScene);
        asyncOp.allowSceneActivation = false;

        while (asyncOp.progress < 0.9f)
        {
            loadFill.fillAmount = asyncOp.progress / 0.9f;
            yield return null;
        }

        loadFill.fillAmount = 1;

        asyncOp.allowSceneActivation = true;
        //SceneManager.UnloadSceneAsync("Loading");
        yield return null;
    }
}