using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    CapsuleCollider2D cap;
    SpriteRenderer sprite;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float dJumpForce;
    [SerializeField] float knockBackPower;
    [SerializeField] GameObject swordBox;
    [SerializeField] GameObject fireBall;
    [SerializeField] Transform fireBallTrs;
    [SerializeField] Slider hpSlider;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] float fallSpeed;
    [SerializeField] float walldistance;
    [SerializeField] float wallJump;
    [SerializeField] float dashSpeed;

    public bool isGround;
    bool isMove;
    bool isAttack;
    bool isSkill1;
    bool isDie = false;
    bool isHurt;
    bool isJump;
    bool doubleJump;
    bool isWall;
    bool isWallJump;
    float isRight;
    //bool isdash = false;

    [SerializeField] float maxHp;
    [SerializeField] float curHp;
    public float playerScore = 0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        move();
    }

    private void Update()
    {
        turn();
        checkGround();
        jump();
        attack();
        checkWall();
        fallWall();
        jumpWall();
        hpBar();
        score();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.right * isRight * 0.13f);
    }

    private void move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        //Vector2 movePos = transform.position;

        if (moveInput != 0 && isAttack == false && isDie == false)
        {
            isMove = true;
            anim.SetBool("isWalk", true);
        }
        else
        {
            isMove = false;
            anim.SetBool("isWalk", false);
        }

        //if(isMove == true) 
        //{
        //    movePos.x += moveInput.x * moveSpeed * Time.deltaTime;
        //}

        //transform.position = movePos;

        Vector2 movePos = rigid.velocity;

        if(isMove == true && isWallJump == false)
        {
            movePos.x = moveInput * moveSpeed;
        }

        rigid.velocity = movePos;
    }

    private void turn()
    {
        if (Input.GetAxis("Horizontal") < 0 && isDie == false && isAttack == false)
        {
            gameObject.transform.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            //sprite.flipX = true;
            isRight = -1f;
        }
        else if (Input.GetAxis("Horizontal") > 0 && isDie == false && isAttack == false)
        {
            gameObject.transform.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            //sprite.flipX = false;
            isRight = 1f;
        }

        //if (sprite.flipX == true)
        //{
        //    swordBox.transform.transform.localScale = new Vector3(-0.6666667f, 0.6666667f, 1);
        //}
        //else if (sprite.flipX == false)
        //{
        //    swordBox.transform.transform.localScale = new Vector3(0.6666667f, 0.6666667f, 1);
        //}

    }

    private void checkGround()
    {
        if (Physics2D.Raycast(cap.bounds.center, Vector2.down,
            cap.size.y * 0.8f, LayerMask.GetMask("Ground")))
        {
            isGround = true;
            anim.SetBool("isJump", false);
        }
        else
        {
            isGround = false;
            anim.SetBool("isJump", true);
        }
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true &&
            isAttack == false && isDie == false && isWall == false)
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            doubleJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJump == true)
        {
            rigid.AddForce(Vector2.up * dJumpForce, ForceMode2D.Impulse);
            doubleJump = false;
        }
    }

    private void attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isSkill1 == false &&
            isGround == true && isAttack == false && isDie == false)
        {
            StartCoroutine(sword());
        }
        else if (Input.GetKeyDown(KeyCode.A) && isSkill1 == false &&
            isGround == true && isAttack == false && isDie == false)
        {
            StartCoroutine(skill1());
        }
    }

    IEnumerator sword()
    {
        anim.SetBool("isSword", true);
        isAttack = true;
        isMove = false;

        yield return new WaitForSeconds(0.3f);

        swordBox.SetActive(true);

        yield return new WaitForSeconds(0.7f);
        
        swordBox.SetActive(false);
        anim.SetBool("isSword", false);
        isAttack = false;
        isMove = true;
    }

    IEnumerator skill1()
    {
        anim.SetBool("isSkill1", true);
        isSkill1 = true;
        Instantiate(fireBall, fireBallTrs.position, Quaternion.Euler(0, 0, 40));
        isMove = false;

        yield return new WaitForSeconds(0.5f);

        anim.SetBool("isSkill1", false);
        isSkill1 = false;
        isMove = true;
    }

    private void checkWall()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right * isRight,
            walldistance, LayerMask.GetMask("Wall")))
        {
            isWall = true;
        }
        else isWall = false;
    }

    private void fallWall()
    {
        float h = Input.GetAxis("Horizontal");

        if (isWall == true && h != 0 && isWallJump == false)
        {
            anim.SetBool("isWall", true);
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * fallSpeed);
        }
        else if (isWall == false) 
        {
            anim.SetBool("isWall", false);
        }
    }

    private void jumpWall()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isWall == true) 
        {
            isWallJump = true;
            Invoke("wallJumpFalse", 0.3f);
        }
        if(isWallJump == true) 
        {
            rigid.velocity = new Vector2(-isRight * wallJump, wallJump);
        }
    }

    private void wallJumpFalse()
    {
        isWallJump = false;
    }

    //private void dash()
    //{
    //    if(Input.GetKeyDown(KeyCode.LeftShift) && isdash == false) 
    //    {
    //        rigid.velocity = new Vector2(dashSpeed, 0);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Enemy") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt());
            //StartCoroutine(knockBack());
        }
        else if (coll.collider.CompareTag("Enemy") && curHp <= 0)
        {
            StartCoroutine(die());
        }
        if (coll.collider.CompareTag("Coin"))
        {
            playerScore += 1f;
            Destroy(coll.gameObject);
        }
    }

    IEnumerator hurt()
    {
        curHp -= 10f;
        isHurt = true;
        anim.SetTrigger("isHurt");

        yield return new WaitForSeconds(0.5f);

        isHurt = false;
    }

    IEnumerator knockBack()
    {
        if (sprite.flipX == true)
        {
            gameObject.transform.Translate(Vector2.right * knockBackPower * Time.deltaTime);
        }
        else
        {
            gameObject.transform.Translate(Vector2.left * knockBackPower * Time.deltaTime);
        }
        yield return null;
    }

    IEnumerator die()
    {
        isDie = true;
        anim.SetTrigger("isDie");
        yield return null;
    }

    private void hpBar()
    {
        hpSlider.value = curHp / maxHp;
    }

    private void score()
    {
        scoreText.text = "X " + playerScore.ToString();
    }
}
