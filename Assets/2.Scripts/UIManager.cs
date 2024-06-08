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

    [SerializeField] TextMeshProUGUI flowerText;
    [SerializeField] GameObject questPanel;

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
    }

    private void playerStat()
    {
    }
}
