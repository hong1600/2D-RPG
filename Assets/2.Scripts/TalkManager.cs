using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    public static TalkManager instance;

    Dictionary<int, string[]> talkData;

    bool quest1Clear;

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
        talkData.Add(1, new string[] { "안녕하세요." , "K를 눌러서 스킬창을 확인할 수 있습니다",
            "레벨업을 하면 스킬포인트를 얻을 수 있습니다", "레벨업을 해서 스킬을 찍어보세요"
            });
        talkData.Add(2, new string[] {"감사합니다.", "보상으로 스킬포인트를 드릴게요.",
            "스킬창은 K를 누르면 열려요!"});
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
