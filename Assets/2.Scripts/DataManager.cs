using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public string name;
    public int level = 1;
    public float curExp = 0;
    public float curHp = 100;
    public float curMp = 100;
    public int skillPoint = 0;
    public int coin = 0;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData curPlayer = new PlayerData();

    string path;
    string fileName = "save";

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

        path = Application.persistentDataPath + "/";
    }

    public void saveData()
    {
        string data = JsonUtility.ToJson(curPlayer);
        File.WriteAllText(path + fileName, data);
    }

    public void loadData()
    {
        string data = File.ReadAllText(path + fileName);
        curPlayer = JsonUtility.FromJson<PlayerData>(data);
    }
}
