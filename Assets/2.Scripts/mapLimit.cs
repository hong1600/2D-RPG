using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapLimit : MonoBehaviour
{
    BoxCollider2D box;

    [SerializeField] GameObject player;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            player.transform.position = new Vector2(-3, -4);
        }
    }
}
