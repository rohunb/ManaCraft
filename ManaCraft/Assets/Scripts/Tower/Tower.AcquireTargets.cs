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
                Assert.IsTrue(attackInfo.groundTargetAoERadius > 0.0f);

                Collider[] collidersInAoERange = Physics.OverlapSphere(visualTargetPosition, attackInfo.groundTargetAoERadius, 1 << TagsAndLayers.CreepLayer);

                StartCoroutine(DebugDrawExplosionSphereTimed(2.0f));

                foreach (var collider in collidersInAoERange)
                {
                    AttackableTarget targetToDamage = collider.gameObject.GetComponentSafe<AttackableTarget>();
                    targetsToDamage.Add(targetToDamage);
                }

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


    //Debug Draw
    private bool drawGizmos = false;

    private IEnumerator DebugDrawExplosionSphereTimed(float durationS)
    {
        drawGizmos = true;
        yield return new WaitForSeconds(durationS);
        drawGizmos = false;
    }

    public void OnDrawGizmos()
    {
        if(!drawGizmos)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(visualTargetPosition, attackInfo.groundTargetAoERadius);
    }
}