using UnityEngine;

public class CharacterStats:MonoBehaviour
{
    public int maxHealth { get; internal set; } = 50;
    public int curHealth { get; internal set; }

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
    public void ResetHealth()
    {
        curHealth = maxHealth;
    }
}