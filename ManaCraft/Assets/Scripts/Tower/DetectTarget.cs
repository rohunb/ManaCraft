// DetectTarget.cs
// ManaCraft
// Created by Rohun Banerji on March 23, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TriggerNotifier))]
public class DetectTarget : MonoBehaviour
{
    public delegate void TargetEnter(AttackableTarget target);
    public delegate void TargetExit(AttackableTarget target);

    public event TargetEnter OnTargetEnter = new TargetEnter((AttackableTarget target) => {});
    public event TargetExit OnTargetExit = new TargetExit((AttackableTarget target) => {});

    private TriggerNotifier triggerNotifier;

    private void Awake()
    {
        triggerNotifier = gameObject.GetComponentSafe<TriggerNotifier>();
    }

    private void Start()
    {
        triggerNotifier.OnTriggerEntered += TriggerEnter;
        triggerNotifier.OnTriggerExited += TriggerExit;
    }

    private void TriggerEnter(Collider otherCollider)
    {
        var attackableTarget = otherCollider.gameObject.GetComponentSafe<AttackableTarget>();
        OnTargetEnter(attackableTarget);
    }

    private void TriggerExit(Collider otherCollider)
    {
        var attackableTarget = otherCollider.gameObject.GetComponentSafe<AttackableTarget>();
        OnTargetExit(attackableTarget);
    }

    public void SetDetectionRadius(float range)
    {
        Assert.IsTrue(range > 0.0f);
        triggerNotifier.SetSphereColliderRange(range);
    }
}
