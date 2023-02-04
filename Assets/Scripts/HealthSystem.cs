using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField, Range(1, 100)] int maxHealth;
    private int currentHealth;
    public bool isDead => currentHealth <= 0;

    private void OnEnable()
    { 
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
