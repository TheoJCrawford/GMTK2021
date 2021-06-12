using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    Rigidbody2D playerRb;

    [SerializeField]
    private float moveSpeed;
    private float inputX;
    private Vector2 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        moveVector = new Vector2(inputX, 0f);
        Move();
    }

    private void Move()
    {
        playerRb.AddForce(moveVector * moveSpeed);
    }
}
