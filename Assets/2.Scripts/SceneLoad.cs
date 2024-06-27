using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject loadingText;
    [SerializeField] GameObject pressText;

    private void Start()
    {
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;

            if (slider.value < 0.9f)
            {
                slider.value = Mathf.MoveTowards(slider.value, 0.9f, Time.deltaTime);
            }
            else if (slider.value >= 0.9f)
            {
                slider.value = Mathf.MoveTowards(slider.value, 1f, Time.deltaTime);
            }

            if (slider.value >= 1f)
            {
                loadingText.SetActive(false);
                pressText.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Space) && slider.value >= 1f
    && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
