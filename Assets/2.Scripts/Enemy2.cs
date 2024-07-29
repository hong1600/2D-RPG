using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D box;
    Animator anim;
    SpriteRenderer sprite;

    [SerializeField] GameObject attack1Box;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject skill1;
    [SerializeField] GameObject gem;
    [SerializeField] GameObject hpPotion;
    [SerializeField] GameObject mpPotion;


    [SerializeField] Transform skill1Pos1;
    [SerializeField] Transform skill1Pos2;
    [SerializeField] Transform skill1Pos3;
    [SerializeField] Transform skill1Pos4;
    [SerializeField] Transform skill1Pos5;
    [SerializeField] Transform skill1Pos6;

    [SerializeField] float curHp;
    float maxHp;
    [SerializeField] float coolTime;
    [SerializeField] float distance;

    bool isDie;
    bool isHurt;
    bool isAttack;

    Vector2 dirVec;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        cooltime();
        turn();
    }

    private void cooltime()
    {
        if (coolTime <= 0 && isAttack == true)
        {
            bossAttack();
            coolTime = 4f;
        }

        coolTime -= Time.deltaTime;
    }

    private void turn() 
    {
        if (Player.instance.transform.position.x < transform.position.x)
        {
            dirVec = Vector2.left;
        }
        else
        {
            dirVec = Vector2.right;
        }

        if (Physics2D.Raycast(box.bounds.center, dirVec, distance, LayerMask.GetMask("Player")))
        {
            isAttack = true;
        }
        else
            isAttack = false;
    }

    private void bossAttack()
    {
        //int rand = Random.Range(0, 2);
        int rand = 1;

        if (rand == 0)
        {
            StartCoroutine(attack1());
        }
        else if (rand == 1)
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

        Instantiate(skill1, skill1Pos1.position, Quaternion.identity);
        Instantiate(skill1, skill1Pos4.position, Quaternion.identity);
        anim.SetBool("Attack2", false);

        yield return new WaitForSeconds(0.5f);

        Instantiate(skill1, skill1Pos2.position, Quaternion.identity);
        Instantiate(skill1, skill1Pos5.position, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        Instantiate(skill1, skill1Pos3.position, Quaternion.identity);
        Instantiate(skill1, skill1Pos6.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("PlayerSword") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(1));
            StartCoroutine(knockBack());
            if (curHp <= 0)
            {
                StartCoroutine(die());
            }
        }
        if (coll.gameObject.CompareTag("PlayerSkill1") && curHp > 0)
        {
            Destroy(coll.gameObject);
            StartCoroutine(hurt(2));
            StartCoroutine(knockBack());
            if (curHp <= 0)
            {
                StartCoroutine(die());
            }
        }
        if (coll.gameObject.CompareTag("PlayerSkill2") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(1));
            StartCoroutine(knockBack());
            if (curHp <= 0)
            {
                StartCoroutine(die());
            }
        }
        if (coll.gameObject.CompareTag("PlayerSkill3") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(1));
            StartCoroutine(knockBack());
            if (curHp <= 0)
            {
                StartCoroutine(die());
            }
        }
        if (coll.gameObject.CompareTag("PlayerSkill4") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(1));
            StartCoroutine(knockBack());
            if (curHp <= 0)
            {
                StartCoroutine(die());
            }
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
        if (gameObject.transform.position.x > Player.instance.transform.position.x)
        {
            rigid.velocity = new Vector2(1, 1f);
        }
        else if (gameObject.transform.position.x < Player.instance.transform.position.x)
        {
            rigid.velocity = new Vector2(-1, 1f);
        }

        yield return null;
    }

    IEnumerator die()
    {
        DataManager.instance.curPlayer.curExp += 5;
        sprite.color = Color.red;
        rigid.velocity = new Vector2(0, 2f);
        anim.SetTrigger("isDie");
        int rand = Random.Range(0, 4);

        if (rand == 0)
        {
            Instantiate(coin, gameObject.transform.position, Quaternion.identity);
            Instantiate(hpPotion, gameObject.transform.position, Quaternion.identity);
            Instantiate(mpPotion, gameObject.transform.position, Quaternion.identity);
            Instantiate(gem, gameObject.transform.position, Quaternion.identity);
        }
        else if (rand == 1)
        {
            Instantiate(coin, gameObject.transform.position, Quaternion.identity);
            Instantiate(hpPotion, gameObject.transform.position, Quaternion.identity);
        }
        else if (rand == 2)
        {
            Instantiate(coin, gameObject.transform.position, Quaternion.identity);
            Instantiate(mpPotion, gameObject.transform.position, Quaternion.identity);
        }
        else if (rand == 3)
        {
            Instantiate(coin, gameObject.transform.position, Quaternion.identity);
            Instantiate(gem, gameObject.transform.position, Quaternion.identity);
        }


        yield return new WaitForSeconds(1f);

        Destroy(this.gameObject);
    }


}
