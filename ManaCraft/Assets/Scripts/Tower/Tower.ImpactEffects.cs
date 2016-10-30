// Tower.DamageEffects.cs
// ManaCraft
// Created by Rohun Banerji on March 27, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public partial class Tower : MonoBehaviour 
{
    private void RunImpactEffects(AttackableTarget target)
    {
        switch (attackInfo.impactEffectType)
        {
            case AttackInfo.ImpactEffectType.AtTarget:
            {
                Debug.LogWarning("Not Implemented");
                break;
            }
            case AttackInfo.ImpactEffectType.OnGround:
            {
                Debug.LogWarning("Not Implemented");
                break;
            }
            case AttackInfo.ImpactEffectType.ChainTargets:
            {

                break;
            }
            default:
            break;
        }

        foreach (var impactEffect in attackInfo.attackImpactEffectList)
        {
            switch (impactEffect)
            {
                case AttackInfo.AttackImpactEffect.VFX:
                {
                    Debug.Log("Impact VFX on " + target.name);
                    break;
                }
                case AttackInfo.AttackImpactEffect.Sound:
                {
                    Debug.Log("Impact Sound on " + target.name);
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
}
