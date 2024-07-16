using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class SeletSlot : MonoBehaviour
{
    [SerializeField] GameObject slot;
    [SerializeField] TextMeshProUGUI[] slotText;
    [SerializeField] TextMeshProUGUI[] dateText;
    [SerializeField] TMP_InputField newPlayerName;

    string curTime;

    bool[] saveFile = new bool[3];

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))
            {
                saveFile[i] = true;
                DataManager.instance.curSlot = i;
                DataManager.instance.loadData();
                slotText[i].text = DataManager.instance.curPlayer.name;
                dateText[i].text = DataManager.instance.curPlayer.date;
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }

        //DataManager.instance.clearData();
    }

    public void slotF(int num)
    {
        DataManager.instance.curSlot = num;

        if (saveFile[num])
        {
            DataManager.instance.loadData();
            goGame();
        }
        else
        {
            create();
        }
    }

    public void create()
    {
        slot.gameObject.SetActive(true);
    }

    public void goGame()
    {
        if (!saveFile[DataManager.instance.curSlot])
        {
            DataManager.instance.curPlayer.name = newPlayerName.text;
            curTime = DateTime.Now.ToString("g");
            DataManager.instance.curPlayer.date = curTime;
            DataManager.instance.saveData();
        }
        int curScene = DataManager.instance.curPlayer.curScene;

        SceneLoad.LoadScene(curScene);
    }
}
