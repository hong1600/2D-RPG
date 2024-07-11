using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public static int nextScene;

    [SerializeField] Slider slider;
    [SerializeField] GameObject loadingText;
    [SerializeField] GameObject pressText;

    GameObject player;
    Rigidbody2D playerrigid;

    private void Start()
    {
        StartCoroutine(loadScene());
        player = GameObject.Find("Player");
        if (player == null)
        {
            return;
        }
        else
        {
            playerrigid = Player.instance.GetComponent<Rigidbody2D>();
        }
    }

    public static void LoadScene(int sceneNum)
    {
        nextScene = sceneNum;
        SceneManager.LoadScene(3);
    }

    IEnumerator loadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
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
                if (player != null)
                {
                    playerrigid.constraints = RigidbodyConstraints2D.None;
                    playerrigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
    }
}
