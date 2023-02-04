using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootAgent : DamageableEntity
{
    public float AttackRadius;

    private void Start()
    {
        OnBecomeDead += Die;
    }

    private void Die(object sender, EventArgs args)
    {
        Destroy(gameObject);
    }
}
