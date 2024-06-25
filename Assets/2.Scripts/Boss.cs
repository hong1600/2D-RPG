using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    float moveDir;
    int nextMove;
    bool nTracking;
    bool lookRight;

    [SerializeField] GameObject bossSkill1;
    [SerializeField] Transform bossSkill1Trs;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        Invoke("attack", 3);
        Invoke("think", 2);
    }

    void Update()
    {
        checkPlayer();
        turn();
        checkGround();
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        if (tracking == false)
        {
            rigid.velocity = new Vector2(nextMove * speed, rigid.velocity.y);
        }

        if (tracking == true)
        {
            rigid.velocity = new Vector2(moveDir * speed, rigid.velocity.y);
        }
    }

    private void think()
    {
        nextMove = Random.Range(-1, 2);

        Invoke("think", 2);
    }


    private void checkPlayer()
    {
        if (GameManager.instance.player.transform.position.x < gameObject.transform.position.x)
        {
            dirVec = Vector2.left;
            moveDir = -1f;
        }
        else if (GameManager.instance.player.transform.position.x > gameObject.transform.position.x)
        {
            dirVec = Vector2.right;
            moveDir = 1f;
        }

        if (Physics2D.Raycast(transform.position, dirVec, distance, LayerMask.GetMask("Player"))
            && nTracking == false)
        {
            tracking = true;
        }
        else
        {
            tracking = false;
        }

        Debug.DrawRay(transform.position, dirVec, Color.red);
    }

    private void turn()
    {
        if (tracking && GameManager.instance.player.transform.position.x < gameObject.transform.position.x)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            lookRight = false;
        }
        else if (GameManager.instance.player.transform.position.x > gameObject.transform.position.x)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
            lookRight = true;
        }
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

    private void attack()
    {
        //int rand = Random.Range(0, 3);
        int rand = 0;

        if (rand == 0)
        {
            StartCoroutine(attack1());
        }
        else if (rand == 1)
        {
            StartCoroutine(attack2());
        }

        Invoke("attack", 3);
    }

    IEnumerator attack1()
    {
        anim.SetBool("isAttack1", true);

        yield return new WaitForSeconds(1f);

        if (lookRight == true)
        {
            Instantiate(bossSkill1, bossSkill1Trs.position, Quaternion.identity);
        }
        else
        {
            Instantiate(bossSkill1, bossSkill1Trs.position, Quaternion.Euler(0,0,-180));
        }
        anim.SetBool("isAttack1", false);
    }
    IEnumerator attack2()
    {

        yield return null;
    }

}
