// Tower.DoDamage.cs
// ManaCraft
// Created by Rohun Banerji on March 27, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public partial class Tower : MonoBehaviour 
{
    private void DoDamage(AttackableTarget target)
    {
        ApplyELementalEffects(target);
        target.OnAttacked(attackInfo.damage);
    }

    private void ApplyELementalEffects(AttackableTarget target)
    {
        Debug.Log("ApplyELementalEffects " + target.name);
    }
}
