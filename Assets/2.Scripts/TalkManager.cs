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
        talkData.Add(1, new string[] { "�ȳ��ϼ���." , "K�� ������ ��ųâ�� Ȯ���� �� �ֽ��ϴ�",
            "�������� �ϸ� ��ų����Ʈ�� ���� �� �ֽ��ϴ�", "�������� �ؼ� ��ų�� ������"
            });
        talkData.Add(2, new string[] {"�����մϴ�.", "�������� ��ų����Ʈ�� �帱�Կ�.",
            "��ųâ�� K�� ������ ������!"});
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
