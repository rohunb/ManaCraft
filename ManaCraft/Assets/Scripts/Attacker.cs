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
    private DetectTarget detectTarget;
    private List<AttackableTarget> targetList = new List<AttackableTarget>();

    private float attacksPerSecond;

    private void Awake()
    {
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

        StartCoroutine(Attack());
    }

    private void TargetLost(AttackableTarget target)
    {
        Assert.IsNotNull(target);

        Assert.IsTrue(targetList.Contains(target));

        targetList.Remove(target);
    }

    private IEnumerator Attack()
    {
        Assert.IsTrue(targetList.Count > 0);


        var firstAttackableTarget = targetList[0];

        Debug.Log("Attack....");


        yield return null;
    }


}
