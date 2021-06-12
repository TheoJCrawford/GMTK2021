using UnityEngine;


[RequireComponent( typeof(PlayerMotor))]
public class PlayerControls : MonoBehaviour
{
    public GameObject otherHalf;
    public bool isMainChar;

    private PlayerMotor _motor;
    private bool _inControl;
    public float pullDist = 4f;
    public int jumpLimit = 1;

    private int _remainingJump;
    void Start()
    {
        _motor = GetComponent<PlayerMotor>();
        ResetJumping();
        _inControl = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_inControl== false)
        {
            TakeMoveInput();
            TakeJumpAction();
            TakeSplitSwapAction();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Physics2D.Raycast(transform.position, Vector2.down))
        {
            ResetJumping();
        }
        
    }
    private void TakeMoveInput()
    {
        if (Input.GetAxis("Horizontal") ==0)
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
        if (Input.GetKeyDown(KeyCode.Space) && _remainingJump >0)
        {
            _motor.EngageJump();
            _remainingJump--;
        }
    }
    private void TakeSplitSwapAction()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _inControl)
        {
            _inControl = false;
        }
    }
    private void ResetJumping()
    {
        _remainingJump = jumpLimit;
    }
    
}
