using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBtnManager : MonoBehaviour
{

    [SerializeField] Image skill1Img;

    [SerializeField] GameObject skill1Panel;
    [SerializeField] GameObject realExitPanel;
    [SerializeField] GameObject skill1ImgOn;
    [SerializeField] GameObject menuPanel;

    public bool skill1on = false;
    public bool skill2on = false;
    public bool skill3on = false;

    private void Awake()
    {

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

}
