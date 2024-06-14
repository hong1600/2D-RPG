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

    [SerializeField] GameObject statPanel;
    [SerializeField] GameObject skillA;

    bool staton;

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
        mpBar.value = player.CurMp / player.MaxMp;
        expBar.value = player.Curexp / player.Maxexp;
        level.text = player.Level.ToString();
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
