// NewTower2.cs
// ManaCraft
// Created by Rohun Banerji on March 27, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class NewTower2 : MonoBehaviour 
{
    [SerializeField]
    private AttackInfo attackInfo;

    private AttackableTarget currentTarget;
    private List<AttackableTarget> targetList = new List<AttackableTarget>();
    private List<AttackableTarget> targetsToDamage = new List<AttackableTarget>();

    private DetectTarget detectTarget;
    private Coroutine attackRoutine;

    //Keep track of stuff to send between attack states
    float projectileTimeToImpact;

    private void Awake()
    {
        Assert.IsNotNull(attackInfo);

        detectTarget = gameObject.GetComponentSafe<DetectTarget>();
    }

    private void Start()
    {
        detectTarget.OnTargetEnter += TargetFound;
        detectTarget.OnTargetExit += TargetLost;
    }

    private void TargetFound(AttackableTarget target)
    {
        Assert.IsNotNull(target);
        //May not be required
        Assert.IsFalse(targetList.Contains(target));

        targetList.Add(target);
        target.OnDestroyed += TargetLost;

        if (attackRoutine == null)
        {
            attackRoutine = StartCoroutine(AttackRoutine());
        }
    }

    private void TargetLost(AttackableTarget target)
    {
        Assert.IsNotNull(target);

        Assert.IsTrue(targetList.Contains(target));

        target.OnDestroyed -= TargetLost;
        targetList.Remove(target);
    }

    private IEnumerator AttackRoutine()
    {
        while(targetList.Count > 0)
        {
            currentTarget = targetList[0];

            Debug.Log("Attacking " + currentTarget.name + "...");

            RunAttackVisual();
            RunDamageEffectDelayLogic();

            float attackDelay = 1.0f / attackInfo.attacksPerSecond;
            yield return new WaitForSeconds(attackDelay);
        }

        attackRoutine = null;
    }

    private void RunAttackVisual()
    {
        switch (attackInfo.attackVisual)
        {
            case AttackInfo.AttackVisual.LaunchProjectileMesh:
            {
                LaunchProjectileMesh();
                break;
            }
            case AttackInfo.AttackVisual.CreateLineRendererEffect:
                Assert.IsTrue(false);
                break;
            case AttackInfo.AttackVisual.CreateParticleEffect:
                Assert.IsTrue(false);
                break;
            case AttackInfo.AttackVisual.CreateConeEffect:
                Assert.IsTrue(false);
                break;
            default:
                Assert.IsTrue(false);
                break;
        }
    }

    private void LaunchProjectileMesh()
    {
        Assert.IsNotNull(currentTarget);
        Assert.IsNotNull(attackInfo.projectilePrefab);
        Assert.IsNotNull(attackInfo.shootPoint);
        Assert.IsTrue(attackInfo.projectileSpeed > 0.0f);

        Debug.Log("LaunchProjectileMesh");

        Vector3 projectilePosition = attackInfo.shootPoint.position;

        var projectile = (GameObject)Instantiate(attackInfo.projectilePrefab, projectilePosition, Quaternion.identity);

        Vector3 directionToTarget = currentTarget.transform.position - projectilePosition;
        float distanceToTarget = directionToTarget.magnitude;

        Assert.IsTrue(distanceToTarget > 0.0f);

        Vector3 shootDirection = directionToTarget / distanceToTarget;

        var rigidBody = projectile.GetComponentSafe<Rigidbody>();
        rigidBody.velocity = shootDirection * attackInfo.projectileSpeed;

        projectile.transform.LookAt(currentTarget.transform);

        projectileTimeToImpact = distanceToTarget / attackInfo.projectileSpeed;

        Destroy(projectile, projectileTimeToImpact);

        Debug.Log("Projectile velocity = " + projectile.GetComponent<Rigidbody>().velocity);
        Debug.Log("Time to impact = " + projectileTimeToImpact);
    }

    private void RunDamageEffectDelayLogic()
    {
        switch (attackInfo.damageEffectDelay)
        {
        case AttackInfo.DamageEffectDelayType.Instant:
            Assert.IsTrue(false);
            break;
        case AttackInfo.DamageEffectDelayType.WaitForProjectileTravelTime:
        {
            StartCoroutine(DamageAfterProjectileTravelTime(currentTarget));
            break;
        }
        case AttackInfo.DamageEffectDelayType.WaitForProjectileGroundImpact:
            Assert.IsTrue(false);
            break;
        default:
            Assert.IsTrue(false);
            break;
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
            RunImpactEffects(target);
            RunAcquireTargetLogic(target);
            DoDamage(target);
        }
    }

    private void RunImpactEffects(AttackableTarget target)
    {
        Debug.Log("Impact Effects on " + target.name);
    }
    
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
            Assert.IsTrue(false);
            break;
            default:
            Assert.IsTrue(false);
            break;
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
    }
}
