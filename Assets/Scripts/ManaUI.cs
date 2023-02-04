using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaUI : MonoBehaviour
{
    private GameController _gameController;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.text = $"Mana: {_gameController.Mana}";
    }
}
