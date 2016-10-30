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
                GetTargetsInAoEAroundPoint();            
                break;
            }
            case AttackInfo.TargetAcquisition.GetTargetsInConeAoE:
            {
                Assert.IsTrue(false);
                break;
            }
            case AttackInfo.TargetAcquisition.GetTargetsInChainAoE:
            {
                GetTargetsInChainAoE();
                break;
            }
            default:
            {
                Assert.IsTrue(false);
                break;
            }
        }
    }

    private void GetTargetsInAoEAroundPoint()
    {
        Assert.IsTrue(attackInfo.groundTargetAoERadius > 0.0f);

        Collider[] collidersInAoERange = Physics.OverlapSphere(visualTargetPosition, attackInfo.groundTargetAoERadius, 1 << TagsAndLayers.CreepLayer);

        StartCoroutine(DebugDrawSphereTimed(2.0f, attackInfo.groundTargetAoERadius));

        foreach (var collider in collidersInAoERange)
        {
            AttackableTarget targetToDamage = collider.gameObject.GetComponentSafe<AttackableTarget>();
            targetsToDamage.Add(targetToDamage);
        }
    }

    private void GetTargetsInChainAoE()
    {
        Assert.IsTrue(attackInfo.chainAoERadius > 0.0f);
        Assert.IsTrue(attackInfo.numChainJumps > 0);

        Collider[] collidersInAoERange = Physics.OverlapSphere(visualTargetPosition, attackInfo.groundTargetAoERadius, 1 << TagsAndLayers.CreepLayer);

        StartCoroutine(DebugDrawSphereTimed(2.0f, attackInfo.chainAoERadius));

        int numTargetsToSelect = Mathf.Min(attackInfo.numChainJumps, collidersInAoERange.Length);

        for (int i = 0; i < numTargetsToSelect; ++i)
        {
            AttackableTarget targetToDamage = collidersInAoERange[i].gameObject.GetComponentSafe<AttackableTarget>();
            targetsToDamage.Add(targetToDamage);
        }
    }

    //Debug Draw
    private bool drawGizmos = false;
    private float gizmoSphereRadius;

    private IEnumerator DebugDrawSphereTimed(float durationS, float radius)
    {
        gizmoSphereRadius = radius;
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
        Gizmos.DrawWireSphere(visualTargetPosition, gizmoSphereRadius);
    }
}