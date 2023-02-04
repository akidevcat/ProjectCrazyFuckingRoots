using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageableEntity : MonoBehaviour
{
    public float Health = 100.0f;
    public float DieAnimationTime = 3.0f;
    public float DieAnimationHeight = 5.0f;

    public event EventHandler OnBecomeDead;
    
    public bool IsDead { get; private set; }
    
    private bool IsAnimatingDeath = false;
    
    public void TakeDamage(float damage)
    {
        Health = Mathf.Max(Health - damage, 0.0f);
    }

    private void Update()
    {
        if (Health <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        AnimateMoveDown();
    }

    private async UniTask AnimateMoveDown()
    {
        if (IsDead || IsAnimatingDeath)
            return;
        
        IsAnimatingDeath = true;
        var pos = transform.position;

        for (var t = 0.0f; t < DieAnimationTime; t += Time.deltaTime)
        {
            pos.y -= DieAnimationHeight * Time.deltaTime / DieAnimationTime;
            transform.position = pos;
            await UniTask.Yield();
        }

        IsDead = true;
        IsAnimatingDeath = false;
        
        OnBecomeDead?.Invoke(this, EventArgs.Empty);
    }
}
