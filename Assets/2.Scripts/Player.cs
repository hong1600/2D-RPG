using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance;

    Rigidbody2D rigid;
    Animator anim;
    CapsuleCollider2D cap;
    SpriteRenderer sprite;
    GameBtnManager gameBtnManager;

    [SerializeField] GameObject btnManager;
    [SerializeField] float maxHp;
    [SerializeField] float curHp;
    [SerializeField] float maxMp;
    [SerializeField] float curMp;
    [SerializeField] float maxExp;
    public float curExp;
    [SerializeField] int curLevel = 1;
    public int curSkillPoint = 0;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float dJumpForce;
    [SerializeField] float landerSpeed;
    [SerializeField] float knockBackPower;
    [SerializeField] GameObject swordBox;
    [SerializeField] GameObject fireBall;
    GameObject scanObject;
    [SerializeField] Transform fireBallTrs;
    [SerializeField] Slider hpSlider;
    [SerializeField] float fallSpeed;
    [SerializeField] float walldistance;
    [SerializeField] float wallJump;
    [SerializeField] GameObject inventoryPanel;
    Vector2 dirVec;
    public int coin;
    public int hpup = 0;
    public int mpup = 0;


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
    bool skillPanelOn;
    bool isLander;
    bool landering;
    bool inventoryon = false;
    public bool skill1on;

    public float Maxhp
    { get { return maxHp; } }
    public float Curhp
    { get { return curHp; } }
    public float MaxMp
    { get { return maxMp; } }
    public float CurMp
    { get { return curMp; } }
    public float Maxexp
    { get { return maxExp; } }
    public float Curexp
    { get { return curExp; } set { curExp = value; } }
    public int Level
    { get { return curLevel; } }
    public int SkillPoint
    { get { return curSkillPoint; } }
    public int playerCoin
    { get { return coin; } }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

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
        level();
        scanObj();
        lander();
        fuction();
    }

    private void move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        //Vector2 movePos = transform.position;

        if (moveInput != 0 && isAttack == false && isDie == false && isWallJump == false 
            && isSkill1 == false && GameManager.instance.isAction == false && landering == false)
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

        if(isMove == true)
        {
            movePos.x = moveInput * moveSpeed;
        }

        rigid.velocity = movePos;

        if (moveInput > 0) { dirVec = Vector2.right; }
        else if (moveInput < 0) { dirVec = Vector2.left; }
    }

    private void turn()
    {
        if (Input.GetAxis("Horizontal") < 0 && isDie == false
            && isAttack == false && isSkill1 == false && landering == false)
        {
            gameObject.transform.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            //sprite.flipX = true;
            isRight = -1f;
        }
        else if (Input.GetAxis("Horizontal") > 0 && isDie == false 
            && isAttack == false && isSkill1 == false && landering == false)
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
        if (Physics2D.Raycast(cap.bounds.min, Vector2.down,
            cap.size.y * 0.8f, LayerMask.GetMask("Ground", "Wall")) 
            || Physics2D.Raycast(cap.bounds.max, Vector2.down,
            cap.size.y * 1.6f, LayerMask.GetMask("Ground", "Wall")))
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
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
            isGround == true && isAttack == false && isDie == false && curMp >= 5f
            && skill1on == true)    
        {
            StartCoroutine(skill1());
            curMp -= 5f;
        }
    }

    IEnumerator sword()
    {
        anim.SetBool("isSword", true);
        isAttack = true;
        isMove = false;
        rigid.velocity = new Vector2(0, rigid.velocity.y);

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

        yield return new WaitForSeconds(0.5f);

        anim.SetBool("isSkill1", false);
        isSkill1 = false;
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
        isMove = false;
    }

    private void level()
    {
        if( maxExp <= curExp ) 
        {
            curLevel += 1;
            curExp = 0;
            maxExp += 3;
            curSkillPoint += 1;
        }
    }

    private void scanObj()
    {
        RaycastHit2D hit = Physics2D.Raycast(cap.transform.position, dirVec, 1, LayerMask.GetMask("Object"));

        if (hit.collider != null)
        {
            scanObject = hit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }

        if(Input.GetKeyDown (KeyCode.F) && scanObject != null) 
        {
            GameManager.instance.Action(scanObject);
        }
    }

    private void fuction()
    {
        if(Input.GetKeyDown(KeyCode.I) && inventoryon == false) 
        {
            inventoryPanel.SetActive(true);
            inventoryon = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && inventoryon == true)
        {
            inventoryPanel.SetActive(false);
            inventoryon = false;
        }

        if (Input.GetKeyDown(KeyCode.Delete) && hpup >= 1)
        {
            if (curHp >= 50)
            {
                curHp = maxHp;
            }
            else
            {
                curHp += 50f;
            }

            hpup -= 1;
        }

        if (Input.GetKeyDown(KeyCode.End) && mpup >= 1)
        {
            if (curMp >= 50)
            {
                curMp = maxMp;
            }
            else
            {
                curMp += 50f;
            }

            mpup -= 1;
        }
    }

    private void lander()
    {
        float moveInput = Input.GetAxisRaw("Vertical");

        if (isLander == true && moveInput != 0)
        {
            anim.SetBool("isLander", true);
            rigid.velocity = new Vector2 (0, moveInput * landerSpeed);
            landering = true;
        }
        else
        {
            anim.SetBool("isLander", false);
            landering = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy2Attack1") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(10));
            StartCoroutine(knockBack(coll.gameObject));
        }
        else if (coll.gameObject.CompareTag("Enemy2Attack1") && curHp <= 0)
        {
            StartCoroutine(die());
        }

        if (coll.gameObject.CompareTag("Enemy2Attack2") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(20));
            StartCoroutine(knockBack(coll.gameObject));
        }
        else if (coll.gameObject.CompareTag("Enemy2Attack2") && curHp <= 0)
        {
            StartCoroutine(die());
        }

        if (coll.gameObject.CompareTag("Lander"))
        {
            isLander = true;
        }

        if (coll.gameObject.CompareTag("BossAttack1"))
        {
            StartCoroutine(hurt(10));
        }

        else if (coll.gameObject.CompareTag("BossAttack1") && curHp <= 0)
        {
            StartCoroutine(die());
        }

        if (coll.gameObject.CompareTag("BossAttack1"))
        {
            StartCoroutine(hurt(10));
        }

        else if (coll.gameObject.CompareTag("BossAttack1") && curHp <= 0)
        {
            StartCoroutine(die());
        }
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Enemy") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(5));
            StartCoroutine(knockBack(coll.gameObject));
        }
        else if (coll.collider.CompareTag("Enemy") && curHp <= 0)
        {
            StartCoroutine(die());
        }

        if (coll.collider.CompareTag("Boss") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(10));
            StartCoroutine(knockBack(coll.gameObject));
        }
        else if (coll.collider.CompareTag("Boss") && curHp <= 0)
        {
            StartCoroutine(die());
        }

        if (coll.collider.CompareTag("Coin"))
        {
            coin += 10;
            Destroy(coll.gameObject);
        }
    }


    IEnumerator hurt(int damage)
    {
        if (curHp > damage)
        {
            curHp -= damage;
        }
        else
        {
            curHp = 0;
        }
        isHurt = true;
        anim.SetTrigger("isHurt");
        isAttack = true;

        yield return new WaitForSeconds(0.3f);

        isHurt = false;
        isAttack = false;
    }

    IEnumerator knockBack(GameObject obj)
    {
        if (obj.transform.position.x > gameObject.transform.position.x)
        {
            rigid.velocity = new Vector2 (-2f, 1f);
        }
        else if (obj.transform.position.x < gameObject.transform.position.x)
        {
            rigid.velocity = new Vector2(2f, 1f);
        }
        yield return null;
    }

    IEnumerator die()
    {
        isDie = true;
        anim.SetTrigger("isDie");
        yield return null;
    }


}
