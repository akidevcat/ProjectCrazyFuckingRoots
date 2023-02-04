using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTHealthBarUI : MonoBehaviour
{
    
    private MotherTree _motherTree;

    private void Awake()
    {
        _motherTree = FindObjectOfType<MotherTree>();
    }

    private void Update()
    {
        var rt = (RectTransform)transform;
        var scale = rt.localScale;
        scale.x = _motherTree.Health / 100.0f;
        rt.localScale = scale;
    }
}
