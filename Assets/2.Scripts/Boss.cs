using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D box;
    Animator anim;
    SpriteRenderer sprite;

    [SerializeField] GameObject attack1Box;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject skill1;
    [SerializeField] Transform skill1Pos;
    [SerializeField] GameObject player;

    [SerializeField] float curHp;
    float maxHp;

    bool isDie;
    bool isHurt;
    [SerializeField] float coolTime;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        think();
    }

    private void think()
    {
        if (coolTime <= 0)
        {
            bossAttack();
            coolTime = 3f;
        }

        coolTime -= Time.deltaTime;
    }

    private void bossAttack()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            StartCoroutine(attack1());
        }
        else if(rand == 1) 
        {
            StartCoroutine(attack2());
        }
    }

    IEnumerator attack1()
    {
        anim.SetBool("Attack1", true);

        yield return new WaitForSeconds(0.5f);

        attack1Box.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        anim.SetBool("Attack1", false);
        attack1Box.SetActive(false);
    }

    IEnumerator attack2()
    {
        anim.SetBool("Attack2", true);

        yield return new WaitForSeconds(1f);

        Instantiate(skill1, skill1Pos.position, Quaternion.identity);
        anim.SetBool("Attack2", false);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("PlayerSword") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(1));
            StartCoroutine(knockBack());
        }
        else if (coll.gameObject.CompareTag("PlayerSword") && curHp <= 0)
        {
            StartCoroutine(die());
            Instantiate(coin, gameObject.transform.position, Quaternion.identity);
            //Instantiate(tree, gameObject.transform.position, Quaternion.Euler(0, 0, -90));
        }
        if (coll.gameObject.CompareTag("PlayerSkill1") && curHp > 0)
        {
            Destroy(coll.gameObject);
            StartCoroutine(hurt(2));
            StartCoroutine(knockBack());
        }
        else if (coll.gameObject.CompareTag("PlayerSkill1") && curHp <= 0)
        {
            StartCoroutine(die());
            Destroy(coll.gameObject);
            Instantiate(coin, gameObject.transform.position, Quaternion.identity);
            //Instantiate(tree, gameObject.transform.position, Quaternion.Euler(0, 0, -90));
        }

    }

    IEnumerator hurt(int _damage)
    {
        curHp -= _damage;
        sprite.color = Color.red;
        isHurt = true;

        yield return new WaitForSeconds(0.3f);

        sprite.color = Color.white;

        yield return new WaitForSeconds(0.3f);

        isHurt = false;
    }

    IEnumerator knockBack()
    {
        if (gameObject.transform.position.x > GameManager.instance.playerPos().x)
        {
            rigid.velocity = new Vector2(1, 1f);
        }
        else if (gameObject.transform.position.x < GameManager.instance.playerPos().x)
        {
            rigid.velocity = new Vector2(-1, 1f);
        }

        yield return null;
    }

    IEnumerator die()
    {
        Player.instance.curExp += 5;
        sprite.color = Color.red;
        rigid.velocity = new Vector2(0, 2f);
        anim.SetTrigger("isDie");

        yield return new WaitForSeconds(1f);

        Destroy(this.gameObject);
    }


}
