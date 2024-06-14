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
        talkData.Add(1, new string[] { "�ȳ��ϼ���" , "Ȥ�� �ð������ø� ���� �� ���ؿ� �ֽǷ���?",
            "������ ���͸� ������ ���Ϳ�!"});
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
