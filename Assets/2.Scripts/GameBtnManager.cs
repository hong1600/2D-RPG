using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBtnManager : MonoBehaviour
{

    public static GameBtnManager Instance;

    [SerializeField] Image skill1Img;

    [SerializeField] GameObject skill1Panel;
    [SerializeField] GameObject realExitPanel;
    [SerializeField] GameObject skill1ImgOn;
    [SerializeField] GameObject menuPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    public void skill1Btn()
    {
        if (Player.instance.SkillPoint > 0)
        {
            skill1Img.color = new Color(1, 1, 1, 1);
            skill1ImgOn.SetActive(true);
            Player.instance.curSkillPoint -= 1;
        }
    }

    public void MenuPanelOnBtn()
    {
        menuPanel.SetActive(true);
    }

    public void MenuPanelOffBtn()
    {
        menuPanel.SetActive(false);
    }

    public void MainMenuOnBtn()
    {
        realExitPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void MainMenuOffBtn()
    {
        realExitPanel.SetActive(false);
    }

    public void GameExitBtn()
    {
        SceneManager.LoadScene(0);
        realExitPanel.SetActive(false);
    }

    public void hpBtn()
    {
        if(Player.instance.coin >= 5) 
        {
            Player.instance.coin -= 5;
            Player.instance.hpup += 1;
        }
    }

    public void mpBtn()
    {
        if (Player.instance.coin >= 5)
        {
            Player.instance.coin -= 5;
            Player.instance.mpup += 1;
        }
    }
}
