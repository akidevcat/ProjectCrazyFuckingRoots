using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimeUI : MonoBehaviour
{

    public RectTransform ScaleRT;
    public Image ScaleImage;
    public TextMeshProUGUI TextUI;

    public Color BuildingPhaseColor;
    public Color DefendingPhaseColor;
    
    private GameController _gameController;
    private BalanceController _balanceController;
    
    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
        _balanceController = FindObjectOfType<BalanceController>();
    }

    private void Update()
    {
        var scale = ScaleRT.localScale;

        if (_gameController.State == GameState.Building)
        {
            scale.x = _gameController.RoundTime / _balanceController.BuildingTimeSeconds;
            ScaleImage.color = BuildingPhaseColor;
            TextUI.text = $"Next: Round {_gameController.Round}";
        }
        if (_gameController.State == GameState.Defending)
        {
            scale.x = _gameController.RoundTime / _balanceController.RoundTimeSeconds;
            ScaleImage.color = DefendingPhaseColor;
            TextUI.text = $"Round {_gameController.Round}";
        }
        
        ScaleRT.localScale = scale;
    }
}
