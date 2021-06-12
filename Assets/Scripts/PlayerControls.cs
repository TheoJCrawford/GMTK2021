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

    }

    // Update is called once per frame
    void Update()
    {
        
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
        
    }

    private void ResetJumping()
    {
        _remainingJump = jumpLimit;
    }
}
