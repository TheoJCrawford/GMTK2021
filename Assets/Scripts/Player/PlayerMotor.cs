using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor:MonoBehaviour
{
    public float _moveSpeed;
    public float _jumpSpeed;
    public float knockbackForce;

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
        _rb.velocity = new Vector2( _moveVec, _rb.velocity.y);
    }
    public void SetMovement(float move = 0)
    {
        _moveVec = move * _moveSpeed;
        
    }
    public void EngageJump()
    {
        _rb.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
    }

    internal void SetState()
    {
        if(_rb.bodyType == RigidbodyType2D.Dynamic)
        {
            _rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void Knockback(Vector3 hitdirection)
    {
        Debug.Log("Knockback");
        _rb.AddForce(-hitdirection * knockbackForce, ForceMode2D.Impulse);
    }
}
