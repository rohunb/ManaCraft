// DetectAttackableTarget.cs
// ManaCraft
// Created by Rohun Banerji on March 23, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TriggerNotifier))]
public class DetectAttackableTarget : MonoBehaviour 
{
    private TriggerNotifier triggerNotifier;

    private void Awake()
    {
    }
}
