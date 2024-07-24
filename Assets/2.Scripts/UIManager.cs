using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] Slider hpBar;
    [SerializeField] Slider mpBar;
    [SerializeField] Slider expBar;
    [SerializeField] Slider bgmVolume;
    [SerializeField] Slider sfxVolume;
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
    [SerializeField] TextMeshProUGUI queset1Text;

    [SerializeField] GameObject bossHpBar;
    [SerializeField] TextMeshProUGUI BossHpText;
 
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
        quest();
    }

    private void playerStat()
    {
        hpBar.value = DataManager.instance.curPlayer.curHp / DataManager.instance.curPlayer.maxHp;
        mpBar.value = DataManager.instance.curPlayer.curMp / DataManager.instance.curPlayer.maxMp;
        expBar.value = DataManager.instance.curPlayer.curExp / DataManager.instance.curPlayer.maxExp;
        level.text = DataManager.instance.curPlayer.level.ToString();
        skillPoint.text = DataManager.instance.curPlayer.skillPoint.ToString();
        curCoin.text = DataManager.instance.curPlayer.coin.ToString();

        
        curHpText.text = $"{DataManager.instance.curPlayer.curHp} / {DataManager.instance.curPlayer.maxHp}";
        curMpText.text = $"{DataManager.instance.curPlayer.curMp} / {DataManager.instance.curPlayer.maxMp}";
        curExpText.text = $"{DataManager.instance.curPlayer.curExp} / {DataManager.instance.curPlayer.maxExp}";

        if (DataManager.instance.curPlayer.hpUp <= 0)
        {
            Hpup.text = "0";
        }
        else
        {
            Hpup.text = DataManager.instance.curPlayer.hpUp.ToString();
        }
        if (DataManager.instance.curPlayer.mpUp <= 0)
        {
            Mpup.text = "0";
        }
        else
        {
            Mpup.text = DataManager.instance.curPlayer.mpUp.ToString();
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

    private void setting()
    {
        DataManager.instance.curPlayer.bgmVolume = bgmVolume.value;
        DataManager.instance.curPlayer.sfxVolume = sfxVolume.value;
    }

    private void quest()
    {
        queset1Text.text = $"º¸¼® ({Player.instance.gemNum.ToString()} / 10)";
    }

}
