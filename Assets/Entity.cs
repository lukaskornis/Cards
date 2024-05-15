using UnityEngine;

public class Entity : MonoBehaviour
{
    public int energy;
    public int maxEnergy;
    
    public int health;
    public int maxHealth;
    
    public GameObject dieEffect;
    
    public void Restore()
    {
        energy = maxEnergy;
        health = maxHealth;
    }

    private void Start()
    {
        Restore();
    } 

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
