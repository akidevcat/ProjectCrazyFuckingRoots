using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToScreen : MonoBehaviour
{
    public Transform TargetTransform;
    public Vector3 TargetOffset;
    public bool DestroyOnTargetNull;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (DestroyOnTargetNull && TargetTransform == null)
        {
            Destroy(gameObject);
            return;
        }
        
        var hSize = new Vector3(Screen.width, Screen.height) / 2.0f;
        var pos = _camera.WorldToScreenPoint(TargetTransform == null ? TargetOffset : TargetTransform.position + TargetOffset) - hSize;
        var rt = (RectTransform)transform;
        rt.anchoredPosition = pos;
    }
}
