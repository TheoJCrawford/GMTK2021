using UnityEngine;

public class SlimeController : MonoBehaviour
{
    Rigidbody2D slimeRb;
    [SerializeField]
    private float health;
    public float bumpDamage;
    [SerializeField]
    private float movePower;
    [SerializeField]
    private float moveDelay;
    private float moveDelayTimer;
    [SerializeField]
    private float slashDamage;

    GameObject target;
    [SerializeField]
    private float trackRadius;
    [SerializeField]
    private LayerMask playerLayer;

    private bool trackingTarget;

    private Vector2 startPosition;
    [SerializeField]
    private float roamDistance;
    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        slimeRb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveDelayTimer <= 0)
        {
            Move();
            moveDelayTimer = moveDelay;
        }
        else
        {
            moveDelayTimer -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if(transform.position.x < startPosition.x - roamDistance)
        {
            facingRight = true;
        }
        else if(transform.position.x > startPosition.x + roamDistance)
        {
            facingRight = false;
        }

        if(facingRight)
        {
            slimeRb.AddForce(Vector3.right * movePower, ForceMode2D.Impulse);
        }
        else
        {
            slimeRb.AddForce(-Vector3.right * movePower, ForceMode2D.Impulse);
        }
    }

    private void Track()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, trackRadius, playerLayer);

        foreach(Collider2D col in targets)
        {
            if(target.CompareTag("Player"))
            {
                target = col.gameObject;
                trackingTarget = true;
                break;
            }
            else
            {
                trackingTarget = false;
            }
        }
    }
}
