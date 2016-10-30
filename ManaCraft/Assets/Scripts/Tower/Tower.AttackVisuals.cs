// Tower.AttackVisuals.cs
// ManaCraft
// Created by Rohun Banerji on March 27, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public partial class Tower : MonoBehaviour 
{
    //Keep track of this to trigger the delayed damage effect when set to WaitForProjectileTravelTime
    float projectileTimeToImpact;
    //Used by ground targeted aoe
    Vector3 visualTargetPosition;

    private void RunAttackVisual()
    {
        DetermineVisualTargetPosition();

        switch (attackInfo.attackVisual)
        {
            case AttackInfo.AttackVisual.LaunchProjectileMesh:
            {
                LaunchProjectileMesh(visualTargetPosition);
                break;
            }
            case AttackInfo.AttackVisual.LineRendererEffect:
            {
                StartCoroutine(CreateLineRendererEffect(visualTargetPosition));
                break;
            }
            case AttackInfo.AttackVisual.ParticleEffect:
            {
                Assert.IsTrue(false);
                break;
            }
            case AttackInfo.AttackVisual.ConeEffect:
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

    private void DetermineVisualTargetPosition()
    {
        Assert.IsNotNull(currentTarget);

        Vector3 currentTargetPosition = currentTarget.transform.position;

        switch (attackInfo.attackVisualTarget)
        {
            case AttackInfo.AttackVisualTarget.TargetCreep:
            {
                visualTargetPosition = currentTargetPosition;
                break;
            }
            case AttackInfo.AttackVisualTarget.TargetGroundBelowCreep:
            {
                Ray rayDownFromTargetToGround = new Ray(currentTargetPosition, Vector3.down);
                RaycastHit rayCastHitOnGround;

                bool targetIsOnGround = Physics.Raycast(rayDownFromTargetToGround, out rayCastHitOnGround, 100.0f, 1 << TagsAndLayers.GroundLayer);

                Assert.IsTrue(targetIsOnGround, "Target is not on ground (may need to increase raycast distance");

                visualTargetPosition = rayCastHitOnGround.point;

                break;
            }
            default:
            {
                Assert.IsTrue(false);
                break;
            }
        }
    }

    private void LaunchProjectileMesh(Vector3 targetPosition)
    {
        Assert.IsNotNull(currentTarget);
        Assert.IsNotNull(attackInfo.projectilePrefab);
        Assert.IsNotNull(attackInfo.shootPoint);
        Assert.IsTrue(attackInfo.projectileSpeed > 0.0f);

        Debug.Log("LaunchProjectileMesh at " + currentTarget.name);

        Vector3 projectileLaunchPosition = attackInfo.shootPoint.position;

        var projectile = (GameObject)Instantiate(attackInfo.projectilePrefab, projectileLaunchPosition, Quaternion.identity);

        Vector3 directionToTarget = targetPosition - projectileLaunchPosition;
        float distanceToTarget = directionToTarget.magnitude;

        Assert.IsTrue(distanceToTarget > 0.0f);

        Vector3 shootDirection = directionToTarget / distanceToTarget;

        var rigidBody = projectile.GetComponentSafe<Rigidbody>();
        rigidBody.velocity = shootDirection * attackInfo.projectileSpeed;

        projectile.transform.LookAt(targetPosition);

        projectileTimeToImpact = distanceToTarget / attackInfo.projectileSpeed;

        Destroy(projectile, projectileTimeToImpact);

        Debug.Log("Projectile velocity = " + projectile.GetComponent<Rigidbody>().velocity);
        Debug.Log("Time to impact = " + projectileTimeToImpact);
    }

    private IEnumerator CreateLineRendererEffect(Vector3 targetPosition)
    {
        LineRenderer line = gameObject.GetComponentSafe<LineRenderer>();

        line.SetVertexCount(2);
        line.SetPosition(0, attackInfo.shootPoint.position);
        line.SetPosition(1, targetPosition);
        line.SetColors(Color.red, Color.red);

        line.enabled = true;

        yield return new WaitForSeconds(attackInfo.lineEffectDuration);

        line.enabled = false;
    }
}