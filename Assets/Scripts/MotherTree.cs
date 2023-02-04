using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherTree : MonoBehaviour
{
    public float Health = 100.0f;

    public void TakeDamage(float damage)
    {
        Health = Mathf.Max(Health - damage, 0.0f);

        if (Health <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        
    }
}
