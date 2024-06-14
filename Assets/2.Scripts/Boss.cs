using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D box;
    Animator anim;

    [SerializeField] GameObject attack1Box;

    [SerializeField] float curHp;
    float maxHp;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        bossAttack();
    }

    private void bossAttack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(attack1());
        }

    }

    IEnumerator attack1()
    {
        anim.SetBool("Attack1", true);
        attack1Box.SetActive(true);

        yield return new WaitForSeconds(0.8f);

        anim.SetBool("Attack1", false);
        attack1Box.SetActive(false);
    }




}
