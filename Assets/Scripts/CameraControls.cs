using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private const float Z_OFFSET = -10f;
    private const float Y_OFFSET = .8f;

    [SerializeField] Transform target;
    [SerializeField] float _timeDilation;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ChangeTarget();
        }
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3( target.position.x, target.position.y + Y_OFFSET, target.position.z + Z_OFFSET), _timeDilation * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f);
    }
    public void ChangeTarget()
    {
        if(target.tag == "Spirit" || target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Spirit").transform;
        }
    }
}
