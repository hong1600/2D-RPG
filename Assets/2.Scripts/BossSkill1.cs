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
    }

    private void Update()
    {
        transform.position =
            Vector3.MoveTowards(transform.position,
            GameManager.instance.playerPos(), speed * Time.deltaTime);

        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
