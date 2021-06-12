using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private const float Z_OFFSET = -10f;

    [SerializeField] Transform target;
    [SerializeField] float _timeDilation;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3( target.position.x, target.position.y, target.position.z + Z_OFFSET), _timeDilation * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ChangeTarget();
        }
    }

    public void ChangeTarget()
    {
        if(target.tag == "Player")
        {
            target = GameObject.FindGameObjectWithTag("Spirit").transform;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
