// Attacker.cs
// ManaCraft
// Created by Rohun Banerji on March 23, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(DetectTarget))]
public class Attacker : MonoBehaviour
{
    [SerializeField]
    private float attacksPerSecond = 1.0f;
    [SerializeField]
    private float damage;

    [SerializeField]
    private AttackInfo attackInfo;

    private DetectTarget detectTarget;
    private List<AttackableTarget> targetList = new List<AttackableTarget>();

    private Coroutine attackRoutine;

    private void Awake()
    {
        detectTarget = gameObject.GetComponentSafe<DetectTarget>();

        Assert.IsTrue(attacksPerSecond > 0.0f);
        Assert.IsTrue(damage > 0.0f);
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

        //Attack coroutine is not running
        if (attackRoutine == null)
        {
            attackRoutine = StartCoroutine(Attack());
        }
    }

    private void TargetLost(AttackableTarget target)
    {
        Assert.IsNotNull(target);

        Assert.IsTrue(targetList.Contains(target));

        target.OnDestroyed -= TargetLost;
        targetList.Remove(target);
    }

    private IEnumerator Attack()
    {
        while (targetList.Count > 0)
        {
            var firstAttackableTarget = targetList[0];

            Debug.Log("Attacking " + firstAttackableTarget.name + "...");

            firstAttackableTarget.OnAttacked(damage);

            float attackDelay = 1.0f / attacksPerSecond;
            yield return new WaitForSeconds(attackDelay);
        }

        attackRoutine = null;
    }


}
