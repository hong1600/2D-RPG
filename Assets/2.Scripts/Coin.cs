using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    CircleCollider2D circle;
    Rigidbody2D rigid;

    private void Awake()
    {
        circle = GetComponent<CircleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

        int rand = Random.Range(-3, 3);
        rigid.velocity = new Vector2(rand, 3);
    }
}
