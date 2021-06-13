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
    //Is this object being controlled
    private bool _inControl;
    //Distance the spirit is returnable
    public float pullDist = 4f;
    //Amount of jumps allowed
    public int jumpLimit = 1;
    private int _remainingJump;
    private float moveVectorX;


    void Start()
    {
        God = GameObject.Find("Deus").GetComponent<Deus>();
        _motor = GetComponent<PlayerMotor>();
        _anima = GetComponent<Animator>();
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.Raycast(transform.position, Vector2.down))
        {
            _anima.SetBool("IsJumping", false);
            ResetJumping();
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
    
    private void Attack()
    {
        if(Input.GetMouseButtonDown(0) && !GameObject.FindGameObjectWithTag("Spirit"))
        {
            //Do attack (Oddly not specified what kind of attack, so I'm going to let this sit here)
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
            spriteRen.flipX = true;
        }
        else if(moveVectorX > 0)
        {
            spriteRen.flipX = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pullDist);
    }
}
