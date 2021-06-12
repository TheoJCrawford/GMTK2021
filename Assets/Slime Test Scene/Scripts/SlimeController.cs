using UnityEngine;

public class SlimeController : MonoBehaviour
{
    Rigidbody2D slimeRb;
    [SerializeField]
    private float health;
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
        //Check for a target to chase
        FindTarget();

        //If no target to chase, and moveDelayTimer is less than 0, move
        if(moveDelayTimer <= 0)
        {
            Move();
            moveDelayTimer = moveDelay;
        }
        //If not tracking target, reduce moveDelayTimer
        else
        {
            moveDelayTimer -= Time.deltaTime;
        }
    }

    //Slime movement script
    private void Move()
    {
        Debug.Log(trackingTarget);
        if(trackingTarget)
        {
            if(target.transform.position.x > transform.position.x)
            {
                facingRight = true;
            }
            else
            {
                facingRight = false;
            }
        }
        else
        {
            //Check direction the slime should be facgin
            if (transform.position.x < startPosition.x - roamDistance)
            {
                facingRight = true;
            }
            else if (transform.position.x > startPosition.x + roamDistance)
            {
                facingRight = false;
            }
        }


        //Move the slime in the correct direction based on its facing direction
        if(facingRight)
        {
            slimeRb.AddForce(Vector3.right * movePower, ForceMode2D.Impulse);
        }
        else
        {
            slimeRb.AddForce(-Vector3.right * movePower, ForceMode2D.Impulse);
        }
    }

    //Check for a target
    private void FindTarget()
    {
        //Check for nearby colliders on the player layer
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, trackRadius, playerLayer);

        //Check each of the colliders for the player tag
        foreach(Collider2D col in targets)
        {
            if(col.CompareTag("Player"))
            {
                //Set target to the found player
                target = col.gameObject;
                //Set tracking to true
                trackingTarget = true;
                break;
            }
            //If no targets in range, turn off tracking
            else
            {
                trackingTarget = false;
            }
        }
    }

    //Method to damage the slime
    public void TakeDamage(float damage)
    {
        health -= damage;

        //If health is less than 0, kill the slime
        if(health <= 0)
        {
            Die();
        }
    }

    //Destroy the slime gameObject
    private void Die()
    {
        Destroy(gameObject);
    }
}