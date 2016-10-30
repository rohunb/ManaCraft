// Tower.DoDamage.cs
// ManaCraft
// Created by Rohun Banerji on March 27, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class Tower : MonoBehaviour 
{
    private void ApplyDamage(AttackableTarget target)
    {
        RunAcquireTargetLogic(target);
        RunImpactEffects(target);

        foreach (var targetToDamage in targetsToDamage)
        {
            DoDamage(targetToDamage);
        }
    }

    private void DoDamage(AttackableTarget target)
    {
        ApplyELementalEffects(target);
        target.OnAttacked(attackInfo.damage);
    }

    private void ApplyELementalEffects(AttackableTarget target)
    {
        Debug.Log("ApplyELementalEffects " + target.name);

        foreach (var elementalEffect in attackInfo.elementalEffects)
        {
            Assert.IsTrue((attackInfo.elementalEffects.Count((effect) => effect.elementalEffectType == elementalEffect.elementalEffectType) == 1), "More than 1 of the same type of effect do not stack");

            target.OnElementalEffect(elementalEffect);
        }
    }
}
