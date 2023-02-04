using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public Camera Camera;
    public float CastDistance = 200.0f;
    public bool BuildingAllowed = true;

    private int surfaceLayer;

    private void Awake()
    {
        surfaceLayer = LayerMask.NameToLayer("Surface");
    }

    private void Update()
    {
        if (!BuildingAllowed)
            return;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ray = Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, CastDistance))
            {
                if (hit.collider.gameObject.layer == surfaceLayer)
                {
                    
                }
            }
        }
    }
}
