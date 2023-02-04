using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TreeAgent : BuildableEntity
{
    public DecalProjector Decal;
    public float BuildingRange;
    public int ManaValue;
    public float ManaDelay;

    public bool ShowHealthBar = true;
    
    private float ManaTimeout;

    private GameController _gameController;
    private GameUIController _gameUIController;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
        _gameUIController = FindObjectOfType<GameUIController>();
    }

    private void Start()
    {
        OnBecomeDead += Die;
        var size = Decal.size;
        size.x = BuildingRange * 2.0f;
        size.y = BuildingRange * 2.0f;
        Decal.size = size;
        
        if (ShowHealthBar)
            _gameUIController.SpawnHealthBar(this, Vector3.up * 5.0f);
    }

    protected override void Update()
    {
        base.Update();

        if (_gameController.State != GameState.Defending)
        {
            ManaTimeout = ManaDelay;
            return;
        }
        
        if (ManaTimeout > 0.0f)
        {
            ManaTimeout -= Time.deltaTime;
            return;
        }

        ManaTimeout = ManaDelay;

        _gameController.Mana += ManaValue;
        _gameUIController.SpawnFloatingText($"+{ManaValue}", transform.position + Vector3.up * 6.0f);
    }

    private void Die(object sender, EventArgs args)
    {
        Destroy(gameObject);
    }
}
