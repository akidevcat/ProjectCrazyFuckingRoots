using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    public DamageableEntity TargetEntity;
    public RectTransform ScaleRT;
    public bool DestroyOnTargetNull = true;
    
    private void Update()
    {
        if (TargetEntity == null && DestroyOnTargetNull)
        {
            Destroy(gameObject);
            return;
        }
        
        var scale = ScaleRT.localScale;
        scale.x = TargetEntity.Health / 100.0f;
        ScaleRT.localScale = scale;
    }
}
