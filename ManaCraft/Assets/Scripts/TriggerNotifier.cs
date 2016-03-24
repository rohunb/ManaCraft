// TriggerNotifier.cs
// ManaCraft
// Created by Rohun Banerji on March 23, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class TriggerNotifier : MonoBehaviour 
{
    public delegate void TriggerEntered(Collider otherCollider);
    public delegate void TriggerExited(Collider otherCollider);

    public event TriggerEntered OnTriggerEntered = new TriggerEntered((Collider) => { });
    public event TriggerExited OnTriggerExited = new TriggerExited((Collider) => { });

    private Collider attachedCollider;

    private void OnTriggerEnter(Collider otherCollider)
    {
        OnTriggerEntered(otherCollider);
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        OnTriggerExited(otherCollider);
    }

}
