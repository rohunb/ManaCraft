// Tower.Main.cs
// ManaCraft
// Created by Rohun Banerji on March 27, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

//Main Tower Logic
public partial class Tower : MonoBehaviour 
{
    [SerializeField]
    private AttackInfo attackInfo;

    private AttackableTarget currentTarget;
    private List<AttackableTarget> targetList = new List<AttackableTarget>();

    private DetectTarget detectTarget;
    private Coroutine attackRoutine;

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
        while (targetList.Count > 0)
        {
            currentTarget = targetList[0];

            RunAttackVisual();
            RunDamageEffectDelayLogic();

            float attackDelay = 1.0f / attackInfo.attacksPerSecond;
            yield return new WaitForSeconds(attackDelay);
        }
        //To indicate that the tower is not attacking anymore
        attackRoutine = null;
    }

    private void ApplyDamage(AttackableTarget target)
    {
        RunAcquireTargetLogic(target);

        Assert.IsTrue(targetsToDamage.Count > 0);

        foreach (var targetToDamage in targetsToDamage)
        {
            RunImpactEffects(targetToDamage);
            DoDamage(targetToDamage);
        }
    }
}
