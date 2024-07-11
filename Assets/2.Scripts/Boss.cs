using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D box;
    SpriteRenderer sprite;
    Animator anim;

    Vector2 dirVec;
    [SerializeField] float distance;
    [SerializeField] bool tracking;
    [SerializeField] float speed;
    [SerializeField] float curHp;
    [SerializeField] float knockDistance;
    bool isHurt;
    float moveDir;
    int nextMove;
    bool nTracking;
    bool attackReady;
    bool isAttack;
    float coolTime;

    [SerializeField] GameObject bossSkill1;
    [SerializeField] Transform bossSkill1Trs;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        Invoke("think", 2);
    }

    void Update()
    {
        move();
        checkPlayer();
        turn();
        checkGround();
        cooltime();
    }

    private void FixedUpdate()
    {
    }

    private void move()
    {
        if (tracking == false && isAttack == false && isHurt == false)
        {
            rigid.velocity = new Vector2(nextMove * speed, rigid.velocity.y);
        }
        else if (tracking == true && isAttack == false && isHurt == false)
        {
            rigid.velocity = new Vector2(moveDir * speed, rigid.velocity.y);
        }
        else if (isAttack == true || isHurt == true) 
        {
            rigid.velocity = new Vector2(0, 0);
        }
    }
    private void turn()
    {
        if (rigid.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1.5f, transform.localScale.y, 1);
            anim.SetBool("isMove", true);
        }
        else if (rigid.velocity.x > 0)
        {
            transform.localScale = new Vector3(1.5f, transform.localScale.y, 1);
            anim.SetBool("isMove", true);
        }
        else if (rigid.velocity.x == 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
            anim.SetBool("isMove", false);
        }
    }

    private void think()
    {
        nextMove = Random.Range(-1, 2);

        Invoke("think", 2);
    }

    private void checkPlayer()
    {
        if (GameManager.instance.player.transform.position.x < transform.position.x)
        {
            dirVec = Vector2.left;
            moveDir = -1f;
        }
        else if (GameManager.instance.player.transform.position.x > transform.position.x)
        {
            dirVec = Vector2.right;
            moveDir = 1f;
        }
        else if (GameManager.instance == null)
        { return; }

        if (Physics2D.Raycast(transform.position, dirVec, distance, LayerMask.GetMask("Player"))
            && nTracking == false)
        {
            tracking = true;
            attackReady = true;
        }
        else
        {
            tracking = false;
            attackReady = false;
        }

        Debug.DrawRay(transform.position, dirVec, Color.red);
    }


    private void checkGround()
    {
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector2.down * 0.6f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(frontVec, Vector2.down * 0.6f, 1,
            LayerMask.GetMask("Ground"));

        if (hit.collider == null)
        {
            nTracking = true;
            CancelInvoke();
            nextMove *= -1;
            Invoke("think", 2);
        }
        else if (hit.collider != null)
        {
            nTracking = false;
        }
    }

    private void cooltime()
    {
        if (coolTime <= 0 && attackReady == true)
        {
            attack();
            coolTime = 3f;
        }

        coolTime -= Time.deltaTime;
    }


    private void attack()
    {
        //int rand = Random.Range(0, 2);
        int rand = 0;

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
        anim.SetBool("isAttack1", true);
        isAttack = true;

        yield return new WaitForSeconds(1f);

        if (dirVec == Vector2.right)
        {
            Instantiate(bossSkill1, bossSkill1Trs.position, Quaternion.identity);
        }
        else if (dirVec == Vector2.left)
        {
            Instantiate(bossSkill1, bossSkill1Trs.position, Quaternion.Euler(0,0,-180));
        }
        anim.SetBool("isAttack1", false);
        isAttack = false;
    }

    IEnumerator attack2()
    {

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("PlayerSword") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(1));
            StartCoroutine(knockBack(coll.gameObject));
            if (curHp <= 0)
            {
                StartCoroutine(die());
            }
        }
        if (coll.gameObject.CompareTag("PlayerSkill1") && curHp > 0 && isHurt == false)
        {
            Destroy(coll.gameObject);
            StartCoroutine(hurt(2));
            StartCoroutine(knockBack(coll.gameObject));
            if (curHp <= 0)
            {
                StartCoroutine(die());
            }
        }
        if (coll.gameObject.CompareTag("PlayerSkill2") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(1));
            StartCoroutine(knockBack(coll.gameObject));
            if (curHp <= 0)
            {
                StartCoroutine(die());
            }
        }
        if (coll.gameObject.CompareTag("PlayerSkill3") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(1));
            StartCoroutine(knockBack(coll.gameObject));
            if (curHp <= 0)
            {
                StartCoroutine(die());
            }
        }
        if (coll.gameObject.CompareTag("PlayerSkill4") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(1));
            StartCoroutine(knockBack(coll.gameObject));
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
        anim.SetTrigger("isHurt");

        yield return new WaitForSeconds(0.3f);

        sprite.color = Color.white;

        yield return new WaitForSeconds(0.3f);

        isHurt = false;
    }

    IEnumerator knockBack(GameObject coll)
    {
        if (gameObject.transform.position.x > coll.transform.position.x)
        {
            rigid.velocity = new Vector2(knockDistance, 1f);
        }
        else if (gameObject.transform.position.x < coll.transform.position.x)
        {
            rigid.velocity = new Vector2(-knockDistance, 1f);
        }

        yield return null;
    }

    IEnumerator die()
    {
        anim.SetTrigger("isDie");
        sprite.color = Color.red;
        rigid.velocity = new Vector2(0, 2f);
        SceneManager.LoadScene(4);

        yield return new WaitForSeconds(1.5f);
    }

}
