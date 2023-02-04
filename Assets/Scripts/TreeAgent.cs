using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAgent : BuildableEntity
{
    private void Start()
    {
        OnBecomeDead += Die;
    }
    
    private void Die(object sender, EventArgs args)
    {
        Destroy(gameObject);
    }
}
