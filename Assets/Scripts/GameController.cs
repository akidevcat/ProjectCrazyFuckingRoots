using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameState State = GameState.Building;
    public int Round = 1;
    public float RoundTime = 0.0f;
    public float Mana = 0.0f;
    
    private MotherTree _motherTree;

    private void Awake()
    {
        _motherTree = FindObjectOfType<MotherTree>();
    }

    private void Update()
    {
        
    }
}
