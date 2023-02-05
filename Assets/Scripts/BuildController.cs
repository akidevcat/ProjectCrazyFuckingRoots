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
    public float MinTreeBuildRange = 5.0f;
    public float MinRootBuildRange = 5.0f;

    public RectTransform BuildingRT;
    private WorldToScreen _buildingRTW2S;
    
    private Vector3 _selectedPoint = Vector3.zero;
    private bool _pointSelected => _selectedPoint != Vector3.zero;

    private List<BuildableEntity> _builtTrees;
    private List<BuildableEntity> _builtRoots;

    private GameController _gameController;
    private GameUIController _gameUIController;
    
    private int surfaceLayer;

    private void Awake()
    {
        _buildingRTW2S = BuildingRT.gameObject.GetComponent<WorldToScreen>();
        surfaceLayer = LayerMask.NameToLayer("Surface");
        _builtTrees = new List<BuildableEntity>();
        _builtRoots = new List<BuildableEntity>();
        _gameController = FindObjectOfType<GameController>();
        _gameUIController = FindObjectOfType<GameUIController>();
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
                    BuildAgent(_selectedPoint,
                        Resources.Load<GameObject>("Prefabs/Tree").GetComponent<BuildableEntity>());
                }
            }
        }
    }

    public void RegisterAgent(BuildableEntity entity)
    {
        if (entity is RootAgent)
        {
            _builtRoots.Add(entity);
        }

        if (entity is TreeAgent)
        {
            Debug.Log("Tree registered");
            _builtTrees.Add(entity);
        }
    }
    
    private bool BuildAgent(Vector3 point, BuildableEntity prefabEntity)
    {
        if (_gameController.Mana < prefabEntity.ManaPrice)
        {
            _gameUIController.SpawnFloatingText("<color=red>Not Enough Mana</color>", point);
            return false;
        }
        
        var flTreeFound = false;
        
        // Find closest tree
        foreach (var t in _builtTrees)
        {
            var tree = (TreeAgent)t;
            var d = Vector3.Distance(t.transform.position, point);
            if (d < MinTreeBuildRange)
            {
                _gameUIController.SpawnFloatingText("<color=red>Cannot Build Here</color>", point);
                return false;
            }
            if (d <= tree.BuildingRange)
            {
                flTreeFound = true;
                break;
            }
        }
        
        // Find closest root
        foreach (var r in _builtRoots)
        {
            var root = (RootAgent)r;
            var d = Vector3.Distance(root.transform.position, point);
            if (d < MinRootBuildRange)
            {
                _gameUIController.SpawnFloatingText("<color=red>Cannot Build Here</color>", point);
                return false;
            }
        }

        if (!flTreeFound)
        {
            _gameUIController.SpawnFloatingText("<color=red>Cannot Build Here</color>", point);
            return false;
        }

        // ToDo
        _gameController.Mana -= prefabEntity.ManaPrice;
        Instantiate(prefabEntity.gameObject, point, Quaternion.identity);
        
        return true;
    }
}
