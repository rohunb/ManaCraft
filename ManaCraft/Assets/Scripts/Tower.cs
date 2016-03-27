// Tower.cs
// ManaCraft
// Created by Rohun Banerji on March 25, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour 
{
    [SerializeField]
    private AttackInfo attackInfo;

    [SerializeField]
    private List<TowerAction> towerActionSequence;

    private List<AttackableTarget> targetList = new List<AttackableTarget>();
    private AttackableTarget currentTarget;

    private List<AttackableTarget> targetsToDamage = new List<AttackableTarget>();

    //State transition conditions
    float projectileTimeToImpact;

    private DetectTarget detectTarget;
    private Coroutine towerActionRoutine;

    private void Awake()
    {
        Assert.IsNotNull(attackInfo);
        Assert.IsNotNull(towerActionSequence);
        Assert.IsTrue(towerActionSequence.Count > 0);

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

        if(towerActionRoutine == null)
        {
            towerActionRoutine = StartCoroutine(StartActionSequence());
        }
    }

    private void TargetLost(AttackableTarget target)
    {
        Assert.IsNotNull(target);

        Assert.IsTrue(targetList.Contains(target));

        target.OnDestroyed -= TargetLost;
        targetList.Remove(target);
    }

    private IEnumerator StartActionSequence()
    {
        int actionIndex = 0;
        int numActions = towerActionSequence.Count;

        //Keep cycling through the action sequence.
        while (targetList.Count > 0)
        {
            //#TODO Keep an eye on whether the string usage causes performance issues
            string coroutineName = towerActionSequence[actionIndex].ToString();
            Debug.Log("Starting action: " + coroutineName);
            yield return StartCoroutine(coroutineName);

            //Go back to first action at the end of the sequence
            actionIndex = (actionIndex + 1) % numActions;
        }

        towerActionRoutine = null;
    }

    private IEnumerator GetFirstTarget()
    {
        if(targetList.Count > 0)
        {
            currentTarget = targetList[0];
        }

        yield break;
    }

    private IEnumerator RetargetAfterAttacking()
    {
        if(targetList.Count > 0)
        {
            Assert.IsTrue(false);
        }

        yield break;
    }

    private IEnumerator LaunchProjectileMesh()
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

        yield break;
    }

    private IEnumerator CreateImpactVFX()
    {
        Debug.Log("Impact VFX");

        yield break;
    }

    private IEnumerator PlayImpactSound()
    {
        Debug.Log("PlayImpactSound");
        yield break;
    }

    private IEnumerator GetTargetsInAoEAroundPoint()
    {
        Debug.Log("GetTargetsInAoEAroundPoint");

        yield break;
    }

    private IEnumerator TargetCurrentTarget()
    {
        Debug.Log("TargetCurrentTarget");

        targetsToDamage.Clear();
        targetsToDamage.Add(currentTarget);

        yield break;
    }

    private IEnumerator DamageTargets()
    {
        Debug.Log("DamageTargets");

        Assert.IsTrue(targetsToDamage.Count > 0);

        foreach (var target in targetsToDamage)
        {
            target.OnAttacked(attackInfo.damage);
        }

        yield break;
    }

    private IEnumerator ApplyDamageOverTime()
    {
        Debug.Log("ApplyDamageOverTime");

        foreach (var target in targetsToDamage)
        {
            Debug.Log("Dot on " + target.name);
        }

        yield break;
    }

    private IEnumerator WaitForProjectileTravelTime()
    {
        Assert.IsTrue(projectileTimeToImpact > 0.0f);

        Debug.Log("WaitForProjectileTravelTime " + projectileTimeToImpact);

        yield return new WaitForSeconds(projectileTimeToImpact);
    }
}
