using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public enum BuildingState { Disabled, SelectingPoint, SelectingBaseType, SelectingSubType }
    
    public Camera Camera;
    public float CastDistance = 200.0f;
    public BuildingState State;

    public RectTransform BuildingRT;
    private WorldToScreen _buildingRTW2S;
    
    private Vector3 _selectedPoint = Vector3.zero;
    private bool _pointSelected => _selectedPoint != Vector3.zero;

    private List<BuildableEntity> _builtEntities;
    
    private int surfaceLayer;

    private void Awake()
    {
        _buildingRTW2S = BuildingRT.gameObject.GetComponent<WorldToScreen>();
        surfaceLayer = LayerMask.NameToLayer("Surface");
        _builtEntities = new List<BuildableEntity>();
    }

    private void Update()
    {
        if (State == BuildingState.Disabled)
            return;
        
        // ToDo UI block check
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ray = Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, CastDistance))
            {
                if (hit.collider.gameObject.layer == surfaceLayer)
                {
                    _selectedPoint = hit.point;
                    _buildingRTW2S.TargetTransform = null;
                    _buildingRTW2S.TargetOffset = _selectedPoint;
                    if (!_buildingRTW2S.gameObject.activeSelf)
                        _buildingRTW2S.gameObject.SetActive(true);
                    State = BuildingState.SelectingBaseType;
                }
            }
        }
    }
}
