using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretRoom : MonoBehaviour
{
    Rigidbody2D rigid;
    TilemapCollider2D tile;

    [SerializeField] GameObject room;

    bool inroom;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        tile = GetComponent<TilemapCollider2D>();
    }

    private void Update()
    {
        if (inroom)
        {
            room.SetActive(false);
        }
        else
        {
            room.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            inroom = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            inroom = false;
        }
    }
}
