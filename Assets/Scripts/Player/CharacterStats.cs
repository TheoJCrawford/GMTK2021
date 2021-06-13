using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterStats:MonoBehaviour
{
    public int maxHealth { get; internal set; } = 100;
    public int curHealth { get; internal set; }

    private void Awake()
    {
        ResetHealth();
    }

    public void SetMaxHealth(int hp)
    {
        maxHealth = hp;
    }
    public void ModifyCurHealth(int hpAdj)
    {
        if(curHealth <= 0)
        {
            Debug.LogWarning("Heavy is dead");
        }
        else if(curHealth + hpAdj >= maxHealth)
        {
            curHealth = maxHealth;
        }
        else
        {
            curHealth += hpAdj;
        }
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;

        if(curHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ResetHealth()
    {
        curHealth = maxHealth;
    }
}