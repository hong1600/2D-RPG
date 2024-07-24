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
            "레벨업을 하면 스킬포인트를 얻을 수 있습니다", "레벨업을 해서 스킬을 찍어보세요"});

        talkData.Add(2, new string[] {"I를 눌러서 물약을 구매할 수 있습니다", 
        "구매한 물약은 키보드 Del와 End로 사용할 수 있습니다."});

        talkData.Add(3, new string[] { "몬스터를 잡고 보석을 모아 보스를 소환해서 잡아주세요",
            "보스는 나무아래에서 F키를 누르면 소환할 수 있습니다"
        });
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
