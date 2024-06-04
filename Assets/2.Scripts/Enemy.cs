using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D box;
    SpriteRenderer sprite;

    [SerializeField] float speed;
    [SerializeField] float curHp;
    [SerializeField] float maxHp;
    [SerializeField] float kDistance;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject tree;

    int nextMove;
    bool tracking = false;
    bool nTracking;
    bool isHurt;
    bool moving;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        Invoke("think", 2);
    }

    void Update()
    {
        checkPlayer();
        checkGround();
        turn();
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        if (tracking == false && isHurt == false)
        {
            rigid.velocity = new Vector2(nextMove * speed, rigid.velocity.y);
        }
        else if (tracking == true && isHurt == false) 
        {
            rigid.velocity = 
                new Vector2(GameManager.instance.playerPos().normalized.x * speed, rigid.velocity.y);
        }
    }

    private void think()
    {
        nextMove = Random.Range(-1, 2);

        Invoke("think", 2);
    }

    private void checkPlayer()
    {
        if (Physics2D.Raycast(box.bounds.center, Vector2.left, box.size.x * 4,
            LayerMask.GetMask("Player")) && nTracking == false)
        {
            tracking = true;
        }
        else
        {
            tracking = false;
        }


    }

    private void checkGround()
    {
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector2.down, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(frontVec, Vector2.down, 1, LayerMask.GetMask("Ground"));

        if (hit.collider == null) 
        {
            nTracking = true;   
            nextMove *= -1;
            CancelInvoke();
            Invoke("think", 2);
        }
        else if (hit.collider != null)
        {
            nTracking = false;
        }
    }

    private void turn()
    {
        if(rigid.velocity.x > 0) 
        {
            gameObject.transform.localScale = new Vector2(-1, gameObject.transform.localScale.y);
        }
        else if (rigid.velocity.x < 0)
        {
            gameObject.transform.localScale = new Vector2(1, gameObject.transform.localScale.y);
        }
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("PlayerSword") && curHp > 0)
        {
            StartCoroutine(hurt());
            StartCoroutine(knockBack());
        }
        else if (coll.gameObject.CompareTag("PlayerSword") && curHp <= 0)
        {
            box.enabled = false;
            Instantiate(coin, gameObject.transform.position, Quaternion.identity);
            //Instantiate(tree, gameObject.transform.position, Quaternion.Euler(0, 0, -90));
            StartCoroutine(die());
        }
    }

    IEnumerator hurt()
    {
        curHp -= 1;
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
            rigid.velocity = new Vector2(kDistance, 1f);
        }
        else if (gameObject.transform.position.x < GameManager.instance.playerPos().x)
        {
            rigid.velocity = new Vector2(-kDistance, 1f);
        }

        yield return null;
    }

    IEnumerator die()
    {
        sprite.color = Color.red;
        rigid.velocity = new Vector2(0, 2f);

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }
}
