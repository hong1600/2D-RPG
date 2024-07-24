using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [SerializeField] GameObject quest1Text;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void quest1()
    {
        if (GameManager.instance.quest1 == true)
        {
            StartCoroutine(quesetText());
        }
    }

    IEnumerator quesetText()
    {
        quest1Text.SetActive(true);

        yield return new WaitForSeconds(1);

        quest1Text.SetActive(false);
    }
}
