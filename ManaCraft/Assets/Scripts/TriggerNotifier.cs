// TriggerNotifier.cs
// ManaCraft
// Created by Rohun Banerji on March 23, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class TriggerNotifier : MonoBehaviour 
{
    public delegate void TriggerEntered(Collider otherCollider);
    public delegate void TriggerExited(Collider otherCollider);

    public event TriggerEntered OnTriggerEntered = new TriggerEntered((Collider) => { });
    public event TriggerExited OnTriggerExited = new TriggerExited((Collider) => { });

    [SerializeField]
    private SphereCollider attachedCollider;

    public void SetSphereColliderRange(float range)
    {
        Assert.IsNotNull(attachedCollider);

        attachedCollider.radius = range;
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        OnTriggerEntered(otherCollider);
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        OnTriggerExited(otherCollider);
    }

}
