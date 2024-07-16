using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill1 : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D box;

    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] GameObject explosion;

    Vector3 dir;
    Quaternion rotTarget;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        tracking();

        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        }
    }

    private void tracking()
    {
        transform.position =
            Vector3.MoveTowards(transform.position,
            Player.instance.transform.position, speed * Time.deltaTime);
    }
}
