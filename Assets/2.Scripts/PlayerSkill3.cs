using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill3 : MonoBehaviour
{
    BoxCollider2D box;
    Rigidbody2D rigid;

    [SerializeField] GameObject explosion;
    [SerializeField] GameObject napalm;
    [SerializeField] float speed;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (Player.instance.transform.localScale.x < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
            rigid.AddForce(new Vector3(-1, -1, 0) * speed, ForceMode2D.Impulse);
        }
        else
        {
            rigid.AddForce(new Vector3(1, -1, 0) * speed, ForceMode2D.Impulse);
        }

        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            Vector3 h = new Vector3(transform.position.x, transform.position.y - 0.05f);

            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Instantiate(napalm, h, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (coll.gameObject.CompareTag("Wall"))
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (coll.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosion, coll.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (coll.gameObject.CompareTag("Enemy2"))
        {
            Instantiate(explosion, coll.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (coll.gameObject.CompareTag("Boss"))
        {
            Instantiate(explosion, coll.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
