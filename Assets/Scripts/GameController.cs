using System;
using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameState State;
    
    private MotherTree _motherTree;

    private void Awake()
    {
        _motherTree = FindObjectOfType<MotherTree>();
    }
}
