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

    [SerializeField] GameObject diePanel;

    Animator playeranim;
    Rigidbody2D playerrigid;
    CapsuleCollider2D playercap;

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

    private void Start()
    {
        playeranim = Player.instance.GetComponent<Animator>();
        playerrigid = Player.instance.GetComponent<Rigidbody2D>();
        playercap = Player.instance.GetComponent<CapsuleCollider2D>();
    }

    public void skill1Btn()
    {
        if (DataManager.instance.curPlayer.skillPoint > 0)
        {
            skill1Img.color = new Color(1, 1, 1, 1);
            DataManager.instance.curPlayer.skillPoint -= 1;
            skillAImg.SetActive(true);
            Player.instance.skill1on = true;
        }
    }

    public void skill2Btn()
    {
        if (DataManager.instance.curPlayer.skillPoint > 0)
        {
            skill2Img.color = new Color(1, 1, 1, 1);
            DataManager.instance.curPlayer.skillPoint -= 1;
            skillSImg.SetActive(true);
            Player.instance.skill2on = true;
        }
    }

    public void skill3Btn()
    {
        if (DataManager.instance.curPlayer.skillPoint > 0)
        {
            skill3Img1.color = new Color(1, 1, 1, 1);
            skill3Img2.color = new Color(1, 1, 1, 1);
            skill3Img3.color = new Color(1, 1, 1, 1);
            DataManager.instance.curPlayer.skillPoint -= 1;
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
        playerrigid.simulated = false;
        DataManager.instance.curPlayer.curPos =
            new Vector2(Player.instance.transform.position.x, Player.instance.transform.position.y);
        DataManager.instance.curPlayer.curScene = SceneManager.GetActiveScene().buildIndex;
        DataManager.instance.saveData();
    }

    public void hpBtn()
    {
        if(DataManager.instance.curPlayer.coin >= 5) 
        {
            DataManager.instance.curPlayer.coin -= 5;
            DataManager.instance.curPlayer.hpUp += 1;
        }
    }

    public void mpBtn()
    {
        if (DataManager.instance.curPlayer.coin >= 5)
        {
            DataManager.instance.curPlayer.coin -= 5;
            DataManager.instance.curPlayer.mpUp += 1;
        }
    }

    public void dieBtn()
    {
        Player.instance.isdie = false;
        DataManager.instance.curPlayer.curHp = DataManager.instance.curPlayer.maxHp;
        DataManager.instance.curPlayer.curMp = DataManager.instance.curPlayer.maxMp;
        playeranim.Rebind();
        diePanel.SetActive(false);
        playerrigid.simulated = true;
        playercap.enabled = true;
    }
}
