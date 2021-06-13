using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class GhostController : Enemy
{
    //Ghost Rigidbody
    Rigidbody2D ghostRb;

    //Ghost Stats
    [SerializeField]
    private float health;
    [SerializeField]
    private int damage;
    //Movement Variables
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float movementDelay;
    private float movementDelayTimer;

    //Travel Variables
    private Vector2 startPosition;
    private bool destinationReached;
    [SerializeField]
    private float roamDistance;
    private bool facingRight;
    private bool movingUp;

    //Target related variables
    Collider2D target;
    Collider2D targetSpirit;
    [SerializeField]
    private float trackRadius;
    private bool targetFound;
    private Vector2 targetPosition;

    //Player layer to check for targets on
    public LayerMask playerMask;
    public LayerMask spiritMask;

    // Start is called before the first frame update
    void Start()
    {
        base._Health = health;
        base._Damage = damage;
        //Get ghost rigidbody
        ghostRb = GetComponent<Rigidbody2D>();

        //The Ghost's starting position
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Check for a target
        findTarget();

        //If no target found and movement timer is 0 or less, move to next destination
        if (!targetFound && movementDelayTimer <= 0)
        {
            destinationReached = false;
            ghostMovement();
        }
        //Once the ghost reaches it's destination, begin counting down until next move
        else if(!targetFound && destinationReached)
        {
            movementDelayTimer -= Time.deltaTime;
        }
        //If the ghost has a target, ignore movement delays
        else
        {
            ghostMovement();
        }
    }

    //Method for finding a target
    private void findTarget()
    {
        //Check for nearby targets within a radius of the ghost
        target = Physics2D.OverlapCircle(transform.position, trackRadius, playerMask);
        targetSpirit = Physics2D.OverlapCircle(transform.position, trackRadius, spiritMask);


        if (target != null)
        {
            if (target.gameObject.CompareTag("Player"))
            {
                targetFound = true;
            }
        }

        if(targetSpirit != null)
        {
            if(targetSpirit.gameObject.CompareTag("Spirit"))
            {
                targetFound = true;
            }

        }
        else
        {
            targetFound = false;
        }

    }

    //Ghost movement method
    private void ghostMovement()
    {
        CheckTurn();
        //If a player target has been found, set the targetPosition to the target's position
        if (!targetFound)
        {
            if (transform.position.x >= targetPosition.x - 0.5 && transform.position.x <= targetPosition.x + 0.05 && transform.position.y >= targetPosition.y - 0.5 && transform.position.y <= targetPosition.y + 0.5)
            {
                //Arrived at destination
                destinationReached = true;
                //Reset movement delay timer
                movementDelayTimer = movementDelay;

                //Random x,y values for next position
                float targetX = Random.Range(startPosition.x - roamDistance, startPosition.x + roamDistance);
                float targetY = Random.Range(startPosition.y - roamDistance, startPosition.y + roamDistance);
                targetPosition = new Vector2(targetX, targetY);
            }
            
        }
        //If no current player target, continue moving the ghost towards the target position
        //Set a new random target position once arrived at the old target position
        else
        {
            targetPosition = target.transform.position;
        }
        //Move the ghost towards the target position if not arrived
        if(!destinationReached)
        {
            ghostRb.transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    //Check which way the ghost should be facing
    private void CheckTurn()
    {
        if (targetPosition.x < transform.position.x)
        {
            facingRight = false;
        }
        else if (targetPosition.x > transform.position.x)
        {
            facingRight = true;
        }

        if (targetPosition.y < transform.position.y)
        {
            movingUp = false;
        }
        else if (targetPosition.y > transform.position.y)
        {
            movingUp = true;
        }
    }



    //Draw the search radius
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, trackRadius);
    }
}
