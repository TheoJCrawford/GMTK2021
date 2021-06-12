using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor:MonoBehaviour
{
    private Rigidbody2D _rb;

    private Vector2 _moveVec;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    

    public void GetNormalMovement(int move = 0)
    {
        _moveVec = new Vector2(move, 0);
    }
}
