// TagsAndLayers.cs
// ManaCraft
// Created by Rohun Banerji on March 27, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TagsAndLayers", menuName = "Data/TagsAndLayers")]
public class TagsAndLayers : ScriptableObject
{
    [SerializeField]
    private int towerLayer = 8;
    [SerializeField]
    private int creepLayer = 9;
    [SerializeField]
    private int groundLayer = 10;

    public static int TowerLayer { get; private set; }
    public static int CreepLayer { get; private set; }
    public static int GroundLayer { get; private set; }

    
    private void OnEnable()
    {
        TowerLayer = towerLayer;
        CreepLayer = creepLayer;
        GroundLayer = groundLayer;
    }       

}
