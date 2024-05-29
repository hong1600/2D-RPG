using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody2D rigid;
    CircleCollider2D circle;
    SpriteRenderer sprite;

    [SerializeField] float fireSpeed;

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

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
