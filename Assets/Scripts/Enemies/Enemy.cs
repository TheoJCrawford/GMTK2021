using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    public float _Health;

    public void TakeDamage(float damage)
    {
        Debug.Log("Take Damage called");
        _Health -= damage;

        if(_Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
