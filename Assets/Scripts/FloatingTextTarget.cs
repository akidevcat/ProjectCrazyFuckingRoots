using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextTarget : MonoBehaviour
{
    public float Speed = 1.0f;
    public float Timeout = 3.0f;
    
    private void Update()
    {
        transform.position += Vector3.up * Speed * Time.deltaTime;
        Timeout -= Time.deltaTime;

        if (Timeout <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
