using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    GameObject player;
    Player playerr;
    Animator playeranim;

    [SerializeField] GameObject talkPanel;
    [SerializeField] GameObject scanObject;
    [SerializeField] TextMeshProUGUI talkText;
    [SerializeField] Image NpcImg;

    public bool isAction = false;
    public int talkIndex;

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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        {
            playeranim = player.GetComponent<Animator>();
        }
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    private void talk(int id, bool isNpc)
    {
        string talkData = TalkManager.instance.getTalk(id, talkIndex);

        if(talkData == null) 
        { 
            isAction = false;
            talkIndex = 0;
            return;
        }

        if (isNpc)
        {
            talkText.text = talkData;
            NpcImg.color = new Color(1, 0, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            NpcImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}
