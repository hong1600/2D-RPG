using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] GameObject portal1;
    [SerializeField] GameObject questUpArrow;

    [SerializeField] GameObject questObj1;
    [SerializeField] GameObject questF;

    [SerializeField] float distance;

    private void Awake()
    {
    }

    void Update()
    {
        quest1();
    }


    private void quest1()
    {
        if (Physics2D.Raycast(questObj1.transform.position, Vector2.right,
            distance, LayerMask.GetMask("Player")))
        {
            questF.SetActive(true);

            if(Input.GetKeyDown(KeyCode.F)) 
            {

            }
        }
        else
        {
            questF.SetActive(false);
        }
    }
}
