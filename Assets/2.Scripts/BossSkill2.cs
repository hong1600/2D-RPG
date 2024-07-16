using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill2 : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D box;

    [SerializeField] float speed;
    [SerializeField] GameObject attack;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StartCoroutine(coolTime());
    }

    IEnumerator coolTime()
    {
        if (Player.instance.transform.position.x < transform.position.x)//¿ÞÂÊ
        {
            rigid.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
        }
        else
        {
            rigid.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
        Instantiate(attack, transform.position, Quaternion.identity);
    }
}
