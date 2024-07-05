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
    [SerializeField] Image skill2Img;
    [SerializeField] Image skill3Img1;
    [SerializeField] Image skill3Img2;
    [SerializeField] Image skill3Img3;

    [SerializeField] GameObject skillAImg;
    [SerializeField] GameObject skillSImg;
    [SerializeField] GameObject skillDImg;

    [SerializeField] GameObject realExitPanel;

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
            Player.instance.curSkillPoint -= 1;
            skillAImg.SetActive(true);
            Player.instance.skill1on = true;
        }
    }

    public void skill2Btn()
    {
        if (Player.instance.SkillPoint > 0)
        {
            skill2Img.color = new Color(1, 1, 1, 1);
            Player.instance.curSkillPoint -= 1;
            skillSImg.SetActive(true);
            Player.instance.skill2on = true;
        }
    }

    public void skill3Btn()
    {
        if (Player.instance.SkillPoint > 0)
        {
            skill3Img1.color = new Color(1, 1, 1, 1);
            skill3Img2.color = new Color(1, 1, 1, 1);
            skill3Img3.color = new Color(1, 1, 1, 1);
            Player.instance.curSkillPoint -= 1;
            skillDImg.SetActive(true);
            Player.instance.skill3on = true;
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
