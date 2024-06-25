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

        if (GameManager.instance.playerScale().x > 0)
        {
            rigid.velocity = new Vector2(fireSpeed, rigid.velocity.y);
        }
        else if (GameManager.instance.playerScale().x < 0)
        {
            rigid.velocity = new Vector2(-fireSpeed, rigid.velocity.y);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -40);
            sprite.flipX = true;
        }

        Destroy(this.gameObject, 1f);
    }

    private void Update()
    {
        Explosion();
    }

    private void Explosion()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, 
            Vector2.right, 0.1f, LayerMask.GetMask("Wall"));

        Instantiate(explosion, hit.point, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if (coll.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

    }

}
