using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class KillFloor : MonoBehaviour
{
    Deus God;

    private void Start()
    {
        God = GameObject.Find("Deus").GetComponent<Deus>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(collision.gameObject.CompareTag("Spirit"))
        {
            God.ChangeController();
            Destroy(collision.gameObject);
        }
    }
}
