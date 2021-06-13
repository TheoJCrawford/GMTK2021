using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpiritCapture : MonoBehaviour
{
    private Deus God;
    private GameObject hostage;
    private BoxCollider2D col;
    private bool playerNear;
    // Start is called before the first frame update
    void Start()
    {
        God = GameObject.Find("Deus").GetComponent<Deus>();
        col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
    }

    private void Update()
    {
        Debug.Log(playerNear);
        if (Input.GetKeyDown(KeyCode.F) && playerNear)
        {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.enabled = false;
            Destroy(hostage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spirit"))
        {
            hostage = collision.gameObject;
            hostage.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            God.ChangeController();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerNear = false;
        }
    }

}
