using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBtnManager : MonoBehaviour
{
    [SerializeField] GameObject realExitPanel;

    public void GameStartBtn()
    {
        SceneManager.LoadScene(3);
    }

    public void GameExitBtn()
    {
        realExitPanel.SetActive(true);
    }

    public void RealGameExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void RealGameExitCancleBtn()
    {
        realExitPanel.SetActive(false);
    }
}
