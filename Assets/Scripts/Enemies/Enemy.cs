using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    protected float _Health;
    protected int _Damage;
    public int bumpDamage;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Spirit"))
        {
            CharacterStats playerStats = collision.gameObject.GetComponent<CharacterStats>();
            playerStats.TakeDamage(_Damage);
        }
    }
}
