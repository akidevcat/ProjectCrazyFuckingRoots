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
    public int Mana = 100;
    
    private MotherTree _motherTree;
    private BalanceController _balanceController;
    
    private void Awake()
    {
        _motherTree = FindObjectOfType<MotherTree>();
        _balanceController = FindObjectOfType<BalanceController>();
    }

    private void Start()
    {
        State = GameState.Building;
        RoundTime = _balanceController.BuildingTimeSeconds;
        Mana = _balanceController.StartManaValue;
    }

    private void Update()
    {
        RoundTime = Mathf.Max(0.0f, RoundTime - Time.deltaTime);
        
        if (RoundTime == 0.0f)
        {
            if (State == GameState.Building)
            {
                State = GameState.Defending;
                RoundTime = _balanceController.RoundTimeSeconds;
            }
            else if (State == GameState.Defending)
            {
                State = GameState.Building;
                RoundTime = _balanceController.BuildingTimeSeconds;
            }
        }
    }
}
