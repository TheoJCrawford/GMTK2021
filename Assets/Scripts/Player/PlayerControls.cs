using UnityEngine;


[RequireComponent(typeof(PlayerMotor), typeof(Animator))]
public class PlayerControls : MonoBehaviour
{
    private Deus God;
    //Spirit Object
    public GameObject otherHalf;
    public bool isMainChar;
    public bool inControl => _inControl;

    //Player Movement Script
    private PlayerMotor _motor;
    //Player Animator
    private Animator _anima;
    //Player Stats
    private CharacterStats _stats;
    //Is this object being controlled
    private bool _inControl;
    //Distance the spirit is returnable
    public float pullDist = 4f;
    //Amount of jumps allowed
    public int jumpLimit = 1;
    private int _remainingJump;
    private float moveVectorX;

    private Collider2D[] attackHitbox;
    [SerializeField]
    private float baseAttackDamage;
    public LayerMask enemyLayer;
    public Vector2 hitSize;
    private bool facingRight = true;
    [SerializeField]
    private GameObject spiritSlashPrefab;
    void Start()
    {
        God = GameObject.Find("Deus").GetComponent<Deus>();
        _motor = GetComponent<PlayerMotor>();
        _anima = GetComponent<Animator>();
        _stats = GetComponent<CharacterStats>();
        ResetJumping();
        _inControl = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for shift press to change control
        TakeSplitSwapAction();

        //If not being controlled, turns the player's movespeed to 0
        if (_inControl == false)
        {
            _motor.SetMovement();
        }
        //If being controlled, take movement and jump actions
        else
        {
            TakeMoveInput();
            FlipCheck();
            TakeJumpAction();
        }

        if (isMainChar && inControl && !GameObject.FindGameObjectWithTag("Spirit"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Attacking");
                RegularAttackBool();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                SpiritSlash();
            }
        }
        else if(!isMainChar && inControl)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Attacking");
                RegularAttackBool();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                SpiritSlash();
            }
        }


        }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.Raycast(transform.position, Vector2.down))
        {
            _anima.SetBool("IsJumping", false);
            ResetJumping();
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with enemy");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            _stats.TakeDamage(enemy.bumpDamage);
            _motor.Knockback(collision.contacts[0].point);
        }
    }
    //Gets the X input vector
    private void TakeMoveInput()
    {
        moveVectorX = Input.GetAxis("Horizontal");
        _anima.SetFloat("Speed", Mathf.Abs(moveVectorX));
        _motor.SetMovement(moveVectorX);
    }

    //Jump Function
    private void TakeJumpAction()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _remainingJump > 0)
        {
            _anima.SetBool("IsJumping", true);
            _motor.EngageJump();
            _remainingJump--;
        }
    }

    //Swap Action function
    private void TakeSplitSwapAction()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _inControl)
        {
            //If controlling the spirit
            if (!isMainChar)
            {
                //If further than the pull distance
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= pullDist)
                {
                    //Change controls back to the player, destroy the spirit object
                    _anima.SetFloat("Speed", 0f);
                    God.ChangeController();
                    GameObject.Destroy(GameObject.FindGameObjectWithTag("Spirit"));
                }
            }
            //If controlling the player
            else
            {
                //If no instantiated spirit
                if (GameObject.FindGameObjectWithTag("Spirit") == null)
                {
                    _anima.SetFloat("Speed", 0f);
                    GameObject.Instantiate<GameObject>(otherHalf, transform.position, otherHalf.transform.rotation);
                    God.ChangeController();
                }
                else
                {
                    if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Spirit").transform.position) <= pullDist)
                    {
                        GameObject.Destroy(GameObject.FindGameObjectWithTag("Spirit"));
                    }
                }
            }
        }
    }
    
    private void RegularAttackBool()
    {
       _anima.SetBool("Attack", true);
        
    }

    private void ResetAttackBool()
    {
        _anima.SetBool("Attack", false);
        Debug.Log("Attack bool reset");
    }

    private void SpiritSlash()
    {
        _anima.SetBool("Attack", true);
        Vector3 projectileOffset = new Vector3(0.5f, 0f, 0f);
        if(facingRight)
        {
            GameObject spiritSlash = Instantiate(spiritSlashPrefab, transform.position + projectileOffset, transform.rotation);
        }
        else
        {
            Quaternion projectileRotation = transform.rotation;
            projectileRotation *= Quaternion.Euler(0f, 0f, 180);
            GameObject spiritSlash = Instantiate(spiritSlashPrefab, transform.position - projectileOffset, projectileRotation);
        }
    }

    public void CreateHitbox()
    {
        if(facingRight)
        {
            if(attackHitbox == null)
            {
                attackHitbox = Physics2D.OverlapBoxAll(transform.position + new Vector3(0.4f, 0f, 0f), hitSize, 0f, enemyLayer);

                if (attackHitbox.Length > 0)
                {
                    foreach (Collider2D col in attackHitbox)
                    {
                        Enemy enemy = col.GetComponent<Enemy>();
                        enemy.TakeDamage(baseAttackDamage);
                    }
                }
                attackHitbox = null;
            }
        }
        else
        {
            if(attackHitbox == null)
            {
                attackHitbox = Physics2D.OverlapBoxAll(transform.position - new Vector3(0.4f, 0f, 0f), hitSize, 0f, enemyLayer);

                if (attackHitbox.Length > 0)
                {
                    foreach (Collider2D col in attackHitbox)
                    {
                        Enemy enemy = col.GetComponent<Enemy>();
                        enemy.TakeDamage(baseAttackDamage);
                    }
                }
                attackHitbox = null;
            }
    
        }

    }

    private void ResetJumping()
    {
        _remainingJump = jumpLimit;
    }

    public void ControlShifting()
    {
        if (_inControl)
        {
            _inControl = false;
        }
        else
        {
            _inControl = true;
        }
    }

    private void FlipCheck()
    {
        SpriteRenderer spriteRen = GetComponent<SpriteRenderer>();
        if(moveVectorX < 0)
        {
            facingRight = false;
            spriteRen.flipX = true;
        }
        else if(moveVectorX > 0)
        {
            facingRight = true;
            spriteRen.flipX = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pullDist);
        Gizmos.color = Color.black;
        Vector3 drawlocation = new Vector3(transform.position.x + 0.4f, transform.position.y, 0f);
        Gizmos.DrawWireCube(drawlocation, hitSize);
    }
}
