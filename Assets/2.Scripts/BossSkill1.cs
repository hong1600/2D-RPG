using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill1 : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D box;

    [SerializeField] float speed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();

        if (transform.position.x < GameManager.instance.transform.position.x)
        {
            rigid.velocity = new Vector2(-speed, rigid.velocity.y);

        }
        else
        {
            rigid.velocity = new Vector2(speed, rigid.velocity.y);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
