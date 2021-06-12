using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    [SerializeField]
    private GameObject gate;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Spirit"))
        {
            gate.GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("Gate Disabled");
        }
    }

}
