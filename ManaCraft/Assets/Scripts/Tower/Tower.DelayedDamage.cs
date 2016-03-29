// Tower.DelayedDamage.cs
// ManaCraft
// Created by Rohun Banerji on March 27, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public partial class Tower : MonoBehaviour 
{
    private void RunDamageEffectDelayLogic()
    {
        switch (attackInfo.damageEffectDelay)
        {
            case AttackInfo.DamageEffectDelayType.Instant:
            {
                Assert.IsTrue(false);
                break;
            }
            case AttackInfo.DamageEffectDelayType.WaitForProjectileTravelTime:
            {
                StartCoroutine(DamageAfterProjectileTravelTime(currentTarget));
                break;
            }
            case AttackInfo.DamageEffectDelayType.WaitForProjectileGroundImpact:
            {
                StartCoroutine(DamageAfterProjectileGroundImpact(currentTarget));
                break;
            }
            default:
            {
                Assert.IsTrue(false);
                break;
            }
        }
    }

    private IEnumerator DamageAfterProjectileTravelTime(AttackableTarget target)
    {
        Assert.IsTrue(projectileTimeToImpact > 0.0f);

        yield return new WaitForSeconds(projectileTimeToImpact);

        projectileTimeToImpact = 0.0f;

        //If the target that was fired at is still alive
        bool targetIsStillValid = target != null
                                && target.gameObject != null
                                && target.IsAlive;

        if(targetIsStillValid)
        {
            ApplyDamage(target);
        }
    }

    private IEnumerator DamageAfterProjectileGroundImpact(AttackableTarget target)
    {
        Assert.IsTrue(projectileTimeToImpact > 0.0f);

        yield return new WaitForSeconds(projectileTimeToImpact);

        ApplyDamage(target);
    }
}
