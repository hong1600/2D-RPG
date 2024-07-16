using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill1 : MonoBehaviour
{
    Rigidbody2D rigid;
    CircleCollider2D circle;
    SpriteRenderer sprite;

    [SerializeField] float fireSpeed;
    [SerializeField] GameObject explosion;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        if (Player.instance.transform.localScale.x > 0)
        {
            rigid.velocity = new Vector2(fireSpeed, rigid.velocity.y);
        }
        else if (Player.instance.transform.localScale.x < 0)
        {
            rigid.velocity = new Vector2(-fireSpeed, rigid.velocity.y);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -40);
            sprite.flipX = true;
        }

        Destroy(this.gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
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
