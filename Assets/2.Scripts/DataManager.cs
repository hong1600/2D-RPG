using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerData
{
    public string name = "";
    public string date = "";
    public int level = 1;
    public float curExp = 0;
    public float maxExp = 2;
    public float curHp = 100;
    public float maxHp = 100;
    public float curMp = 100;
    public float maxMp = 100;
    public int skillPoint = 0;
    public int coin = 0;
    public int hpUp = 0;
    public int mpUp = 0;
    public Vector2 curPos = new Vector2(-14.1f, -4.6f);
    public int curScene = 1;
    public float sfxVolume = 1;
    public float bgmVolume = 1;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData curPlayer = new PlayerData();

    public string path;
    public int curSlot;

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

        path = Application.persistentDataPath + "/save";
    }

    public void saveData()
    {
        string data = JsonUtility.ToJson(curPlayer);
        File.WriteAllText(path + curSlot.ToString(), data);
    }

    public void loadData()
    {
        string data = File.ReadAllText(path + curSlot.ToString());
        curPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    public void clearData()
    {
        curSlot -= 1;
        curPlayer = new PlayerData();
    }
}
