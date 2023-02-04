using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MotherTree : DamageableEntity
{
    public CanvasGroup DeathScreenCG;

    private void Start()
    {
        OnBecomeDead += Die;
    }

    private void Die(object sender, EventArgs args)
    {
        AnimateDeathScreen();
    }

    private async UniTask AnimateDeathScreen()
    {
        for (var t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            DeathScreenCG.alpha = t;
            await UniTask.Yield();
        }

        await UniTask.Delay(3000);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
