// Tower.AcquireTargets.cs
// ManaCraft
// Created by Rohun Banerji on March 27, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public partial class Tower : MonoBehaviour
{
    private List<AttackableTarget> targetsToDamage = new List<AttackableTarget>();

    private void RunAcquireTargetLogic(AttackableTarget target)
    {
        targetsToDamage.Clear();

        switch (attackInfo.targetAcquisition)
        {
            case AttackInfo.TargetAcquisition.TargetCurrentTarget:
            {
                targetsToDamage.Add(target);
                break;
            }
            case AttackInfo.TargetAcquisition.GetTargetsInAoEAroundPoint:
            {
                Assert.IsTrue(false);
                break;
            }
            case AttackInfo.TargetAcquisition.GetTargetsInConeAoE:
            {
                Assert.IsTrue(false);
                break;
            }
            case AttackInfo.TargetAcquisition.GetTargetsInChainAoE:
            {
                Assert.IsTrue(false);
                break;
            }
            default:
            {
                Assert.IsTrue(false);
                break;
            }
        }
    }
}