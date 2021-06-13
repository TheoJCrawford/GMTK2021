using UnityEngine;


[RequireComponent(typeof(PlayerMotor), typeof(Animator))]
public class PlayerControls : MonoBehaviour
{
    public GameObject otherHalf;
    public bool isMainChar;
    public bool inControl => _inControl;

    private PlayerMotor _motor;
    private Animator _anima;
    private bool _inControl;
    public float pullDist = 4f;
    public int jumpLimit = 1;
    private int _remainingJump;
    void Start()
    {
        _motor = GetComponent<PlayerMotor>();
        _anima = GetComponent<Animator>();
        ResetJumping();
        _inControl = true;
    }

    // Update is called once per frame
    void Update()
    {
        TakeSplitSwapAction();
        if (_inControl == false)
        {
            _motor.SetMovement();
        }
        else
        {
            TakeMoveInput();
            TakeJumpAction();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.Raycast(transform.position, Vector2.down))
        {
            ResetJumping();
        }

    }
    private void TakeMoveInput()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            _motor.SetMovement();
        }
        else
        {
            _motor.SetMovement(Input.GetAxis("Horizontal"));
        }
    }
    private void TakeJumpAction()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _remainingJump > 0)
        {
            _motor.EngageJump();
            _remainingJump--;
        }
    }
    private void TakeSplitSwapAction()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _inControl)
        {
            
            if (!isMainChar)
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= pullDist)
                {

                }
                else
                {
                    GameObject.Find("Deus").GetComponent<Dues>().ChangeController();
                    GameObject.Destroy(GameObject.FindGameObjectWithTag("Spirit"));
                }
            }
            if(isMainChar && GameObject.FindGameObjectWithTag("Spirit") == null)
            {
                GameObject.Instantiate<GameObject>(otherHalf, transform.position, otherHalf.transform.rotation);
                GameObject.Find("Deus").GetComponent<Dues>().ChangeController();
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, pullDist);
    }
}
