using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    BoxCollider2D box;
    [SerializeField] GameObject F;
    bool bossSpawn;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(bossSpawn && Input.GetKeyDown(KeyCode.F)) 
        {
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            boss.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            F.SetActive(true);
            bossSpawn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            F.SetActive(false);
        }
    }

}
