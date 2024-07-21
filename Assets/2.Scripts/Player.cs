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

    string playerName;
    [SerializeField] float maxHp;
    [SerializeField] float curHp;
    [SerializeField] float maxMp;
    [SerializeField] float curMp;
    [SerializeField] float maxExp;
    [SerializeField] int level = 1;
    public float curExp;
    public int skillPoint = 0;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float dJumpForce;
    [SerializeField] float landerSpeed;
    [SerializeField] float knockBackPower;
    [SerializeField] float fallSpeed;
    [SerializeField] float walldistance;
    [SerializeField] float wallJump;
    public int coin;
    public int hpup = 0;
    public int mpup = 0;

    [SerializeField] GameObject swordBox;
    [SerializeField] GameObject fireBall;
    [SerializeField] GameObject fireHand;
    [SerializeField] GameObject meteors;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject diePanel;
    GameObject cutcam1;
    GameObject cutcam2;
    GameObject scanObject;

    [SerializeField] Transform fireBallTrs;
    [SerializeField] Transform fireHandTrs;
    [SerializeField] Transform meteorsTrs1;
    [SerializeField] Transform meteorsTrs2;
    [SerializeField] Transform meteorsTrs3;
    [SerializeField] Transform meteorsTrs4;
    Transform curpos;
    Vector2 dirVec;

    public bool isGround;
    bool isMove;
    bool isAttack;
    bool isDie = false;
    bool isHurt;
    bool isJump;
    bool doubleJump;
    bool isWall;
    bool isWallJump;
    float isRight;
    bool skillPanelOn;
    bool isLander;
    bool isLandering;
    bool inventoryon = false;
    public bool skill1on;
    public bool skill2on;
    public bool skill3on;


    [SerializeField] AudioClip attackclip;
    [SerializeField] AudioClip coinClip;
    [SerializeField] AudioClip dieClip;

    public float Maxhp
    { get { return maxHp; } }
    public float Curhp
    { get { return curHp; } set { curHp = value; } }
    public float Maxmp
    { get { return maxMp; } }
    public float Curmp
    { get { return curMp; } set { curMp = value; } }
    public float Maxexp
    { get { return maxExp; } }
    public float Curexp
    { get { return curExp; } set { curExp = value; } }
    public int Level
    { get { return level; } }
    public int SkillPoint
    { get { return skillPoint; } }
    public int playerCoin
    { get { return coin; } }
    public bool isdie
    { get { return isDie; } set { isDie = value; } }

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

    private void Start()
    {
        name += DataManager.instance.curPlayer.name;
        level += DataManager.instance.curPlayer.level;
        curExp += DataManager.instance.curPlayer.curExp;
        curHp += DataManager.instance.curPlayer.curHp;
        curMp += DataManager.instance.curPlayer.curMp;
        skillPoint += DataManager.instance.curPlayer.skillPoint;
        coin += DataManager.instance.curPlayer.coin;
        hpup += DataManager.instance.curPlayer.hpUp;
        mpup += DataManager.instance.curPlayer.mpUp;
        gameObject.transform.position = DataManager.instance.curPlayer.curPos;
    }

    private void FixedUpdate()
    {
        if (isDie) return;
        move();
    }

    private void Update()
    {
        if (isDie) return;
        turn();
        checkGround();
        jump();
        attack();
        checkWall();
        fallWall();
        jumpWall();
        levelUp();
        scanObj();
        lander();
        fuction();
    }

    private void move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        //Vector2 movePos = transform.position;

        if (moveInput != 0 && isAttack == false && isWallJump == false 
            && GameManager.instance.isAction == false && isLandering == false)
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
        if (Input.GetAxis("Horizontal") < 0
            && isAttack == false && isLandering == false)
        {
            gameObject.transform.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            //sprite.flipX = true;
            isRight = -1f;
        }
        else if (Input.GetAxis("Horizontal") > 0 
            && isAttack == false && isLandering == false)
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
            isAttack == false && isWall == false)
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
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGround == true &&
            isAttack == false)
        {
            StartCoroutine(sword());
        }
        else if (Input.GetKeyDown(KeyCode.A) && isGround == true 
            && isAttack == false && DataManager.instance.curPlayer.curMp >= 5f && skill1on == true)    
        {
            StartCoroutine(skill1());
            DataManager.instance.curPlayer.curMp -= 5f;
        }
        else if (Input.GetKeyDown(KeyCode.S) && isGround == true
        && isAttack == false && DataManager.instance.curPlayer.curMp >= 10f && skill2on == true)
        {
            StartCoroutine(skill2());
            DataManager.instance.curPlayer.curMp -= 10f;
        }
        else if (Input.GetKeyDown(KeyCode.D) && isGround == true
        && isAttack == false && DataManager.instance.curPlayer.curMp >= 20f && skill3on == true)
        {
            StartCoroutine(skill3());
            DataManager.instance.curPlayer.curMp -= 20f;
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
        AudioManager.instance.sfxPlayer("Attack", attackclip);

        yield return new WaitForSeconds(0.3f);

        AudioManager.instance.sfxPlayer("Attack", attackclip);

        yield return new WaitForSeconds(0.4f);

        swordBox.SetActive(false);
        anim.SetBool("isSword", false);
        isAttack = false;
        isMove = true;
    }

    IEnumerator skill1()
    {
        anim.SetBool("isSkill1", true);
        isAttack = true;
        Instantiate(fireBall, fireBallTrs.position, Quaternion.Euler(0, 0, 40));

        yield return new WaitForSeconds(0.5f);

        anim.SetBool("isSkill1", false);
        isAttack = false;
    }
    IEnumerator skill2()
    {
        anim.SetBool("isSkill1", true);
        isAttack = true;
        Instantiate(fireHand, fireHandTrs.position, Quaternion.Euler(0, 0, 0));

        yield return new WaitForSeconds(0.5f);

        anim.SetBool("isSkill1", false);
        isAttack = false;
    }
    IEnumerator skill3()
    {
        anim.SetBool("isSkill1", true);
        isAttack = true;
        Instantiate(meteors, meteorsTrs1.position, Quaternion.Euler(0, 0, 0));
        Instantiate(meteors, meteorsTrs2.position, Quaternion.Euler(0, 0, 0));
        Instantiate(meteors, meteorsTrs3.position, Quaternion.Euler(0, 0, 0));
        Instantiate(meteors, meteorsTrs4.position, Quaternion.Euler(0, 0, 0));

        yield return new WaitForSeconds(0.5f);

        anim.SetBool("isSkill1", false);
        isAttack = false;

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

    private void levelUp()
    {
        if (DataManager.instance.curPlayer.maxExp <= DataManager.instance.curPlayer.curExp) 
        {
            DataManager.instance.curPlayer.level++;
            DataManager.instance.curPlayer.curExp = 0;
            DataManager.instance.curPlayer.skillPoint += 1;
            DataManager.instance.curPlayer.maxExp += 2f;
        }
    }

    private void scanObj()
    {
        RaycastHit2D hit = Physics2D.Raycast(cap.transform.position, 
            dirVec, 1, LayerMask.GetMask("Object"));

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

        if (Input.GetKeyDown(KeyCode.Delete) && DataManager.instance.curPlayer.hpUp >= 1)
        {
            if (DataManager.instance.curPlayer.curHp >= 80)
            {
                DataManager.instance.curPlayer.curHp = DataManager.instance.curPlayer.maxHp;
            }
            else
            {
                DataManager.instance.curPlayer.curHp += 20f;
            }

            DataManager.instance.curPlayer.hpUp -= 1;
        }

        if (Input.GetKeyDown(KeyCode.End) && DataManager.instance.curPlayer.mpUp >= 1)
        {
            if (DataManager.instance.curPlayer.curMp >= 80)
            {
                DataManager.instance.curPlayer.curMp = DataManager.instance.curPlayer.maxMp;
            }
            else
            {
                DataManager.instance.curPlayer.curMp += 20f;
            }

            DataManager.instance.curPlayer.mpUp -= 1;
        }
    }

    private void lander()
    {
        float moveInput = Input.GetAxisRaw("Vertical");

        if (isLander == true && moveInput != 0)
        {
            rigid.velocity = new Vector2(0, moveInput * landerSpeed);
            anim.SetBool("isLander", true);
            rigid.gravityScale = 0;
            isLandering = true;
        }
        else if (isLander == true && moveInput == 0)
        {
            rigid.velocity = new Vector2(0, 0);
            rigid.gravityScale = 0;
        }
        else if (isLander == false)
        {
            anim.SetBool("isLander", false);
            rigid.gravityScale = 1;
            isLandering = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy2Attack1") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(10));
            StartCoroutine(knockBack(coll.gameObject));
        }
        if (coll.gameObject.CompareTag("Enemy2Attack2") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(20));
            StartCoroutine(knockBack(coll.gameObject));
        }
        if (coll.gameObject.CompareTag("BossAttack1"))
        {
            StartCoroutine(hurt(10));
            StartCoroutine(knockBack(coll.gameObject));
        }
        if (coll.gameObject.CompareTag("BossAttack2"))
        {
            StartCoroutine(hurt(20));
            StartCoroutine(knockBack(coll.gameObject));
        }


        if (coll.gameObject.CompareTag("Lander"))
        {
            isLander = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Lander"))
        {
            isLander = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Enemy") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(5));
            StartCoroutine(knockBack(coll.gameObject));
        }
        if (coll.collider.CompareTag("Boss") && curHp > 0 && isHurt == false)
        {
            StartCoroutine(hurt(10));
            StartCoroutine(knockBack(coll.gameObject));
        }

        if (coll.collider.CompareTag("Coin"))
        {
            AudioManager.instance.sfxPlayer("Coin", coinClip);
            DataManager.instance.curPlayer.coin += 10;
            Destroy(coll.gameObject);
        }
        if (coll.collider.CompareTag("HpPotion"))
        {
            DataManager.instance.curPlayer.hpUp += 1;
            Destroy(coll.gameObject);
        }
        if (coll.collider.CompareTag("MpPotion"))
        {
            DataManager.instance.curPlayer.mpUp += 1;
            Destroy(coll.gameObject);
        }
    }

    IEnumerator hurt(int damage)
    {
        if (isDie == true)
        {
            yield break;
        }
        DataManager.instance.curPlayer.curHp -= damage;
        isHurt = true;
        anim.SetTrigger("isHurt");
        isAttack = true;

        yield return new WaitForSeconds(0.3f);

        if (DataManager.instance.curPlayer.curHp <= 0)
        {
            AudioManager.instance.sfxPlayer("Die", dieClip);
            isDie = true;
            anim.SetTrigger("isDie");
            diePanel.SetActive(true);
            cap.enabled = false;
            rigid.simulated = false;
        }

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
}
