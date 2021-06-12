using UnityEngine;


[RequireComponent( typeof(PlayerMotor))]
public class PlayerControls : MonoBehaviour
{
    private PlayerMotor _motor;


    public int jumpLimit = 1;

    private int _remainingJump;
    void Start()
    {
        _motor = GetComponent<PlayerMotor>();
        ResetJumping();
    }

    // Update is called once per frame
    void Update()
    {
        TakeMoveInput();
        TakeJumpAction();
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

    private void ResetJumping()
    {
        _remainingJump = jumpLimit;
    }
}
