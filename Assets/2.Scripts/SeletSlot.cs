using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeletSlot : MonoBehaviour
{
    [SerializeField] GameObject slot;
    [SerializeField] TextMeshProUGUI[] slotText;
    [SerializeField] TextMeshProUGUI newPlayerName;

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
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }
        DataManager.instance.clearData();
    }

    public void slotF(int num)
    {
        DataManager.instance.curSlot = num;

        if (saveFile[num])
        {
            DataManager.instance.loadData();
            startGame();
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

    public void startGame()
    {
        if (!saveFile[DataManager.instance.curSlot])
        {
            Player.instance.transform.position = DataManager.instance.curPlayer.curPos.position;
            DataManager.instance.curPlayer.name = newPlayerName.text;
            DataManager.instance.saveData();
        }
        SceneLoad.LoadScene(1);
    }
}
