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

        rigid.velocity = new Vector2(speed, rigid.velocity.y);
    }
}
