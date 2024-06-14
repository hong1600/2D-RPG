using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public static TalkManager instance;

    Dictionary<int, string[]> talkData;

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

        talkData = new Dictionary<int, string[]>();
        talk();
    }

    private void talk()
    {
        talkData.Add(1, new string[] { "안녕하세요" , "혹시 시간있으시면 나무 좀 구해와 주실래요?",
            "나무는 몬스터를 잡으면 나와요!"});
    }

    public string getTalk(int id, int talkNum)
    {
        if (talkNum == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkNum];
        }
    }

}
