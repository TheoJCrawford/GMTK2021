using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor:MonoBehaviour
{
    public float _moveSpeed;
    public float _jumpSpeed;

    private Rigidbody2D _rb;
    private float _moveVec;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        EnactMovement();
    }
    private void EnactMovement()
    {
        _rb.AddForce(new Vector2( _moveVec, 0), ForceMode2D.Force);
    }
    public void GetNormalMovement(int move = 0)
    {
        _moveVec = move * _moveSpeed;
        
    }
    public void EnguageJump()
    {
        _rb.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
    }
}
