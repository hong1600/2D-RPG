using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] Player player;

    [SerializeField] Slider hpBar;
    [SerializeField] Slider mpBar;
    [SerializeField] Slider expBar;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI skillPoint;

    [SerializeField] GameObject statPanel;
    [SerializeField] GameObject skillA;
    [SerializeField] GameObject skillPanel;

    [SerializeField] TextMeshProUGUI skill1Text;

    [SerializeField] TextMeshProUGUI curHpText;
    [SerializeField] TextMeshProUGUI curMpText;
    [SerializeField] TextMeshProUGUI curExpText;
    [SerializeField] TextMeshProUGUI curCoin;
    [SerializeField] TextMeshProUGUI Hpup;
    [SerializeField] TextMeshProUGUI Mpup;
 
    bool staton;
    bool SkillA;

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

    private void Update()
    {
        playerStat();
        statPanelOn();
    }

    private void playerStat()
    {
        hpBar.value = player.Curhp / player.Maxhp;
        mpBar.value = player.Curmp / player.Maxmp;
        expBar.value = player.Curexp / player.Maxexp;
        level.text = player.Level.ToString();
        skillPoint.text = player.SkillPoint.ToString();
        curCoin.text = player.playerCoin.ToString();

        
        curHpText.text = $"{player.Curhp} / {player.Maxhp}";
        curMpText.text = $"{player.Curmp} / {player.Maxmp}";
        curExpText.text = $"{player.Curexp} / {player.Maxexp}";

        if (player.hpup <= 0)
        {
            Hpup.text = "0";
        }
        else
        {
            Hpup.text = player.hpup.ToString();
        }
        if (player.mpup <= 0)
        {
            Mpup.text = "0";
        }
        else
        {
            Mpup.text = player.mpup.ToString();
        }

    }

    private void statPanelOn()
    {
        if(Input.GetKeyDown(KeyCode.K) && staton == false) 
        {
            statPanel.SetActive(true);
            staton = true;
        }

        else if (Input.GetKeyDown(KeyCode.K) && staton == true)
        {
            statPanel.SetActive(false);
            staton = false;
        }
    }
}
