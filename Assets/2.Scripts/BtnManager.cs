using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    public static BtnManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }


    /// <summary>
    /// ∏ﬁ¿Œæ¿
    /// </summary>
    public void GameStartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void GameExitBtn()
    {
        
    }

    /// <summary>
    /// ∞‘¿”æ¿
    /// </summary>
    /// 



}
