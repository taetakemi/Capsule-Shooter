using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

    public Slider slider;
    public Text percentage;

    public void loading (string sceneName)
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadAsynchonously(sceneName));
    }

    IEnumerator LoadAsynchonously (string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            percentage.text = (Mathf.Round(progress * 100f)).ToString() + "%";
            yield return null;
        }
    }
}
