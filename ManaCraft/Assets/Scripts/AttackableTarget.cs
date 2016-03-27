// AttackableTarget.cs
// ManaCraft
// Created by Rohun Banerji on March 24, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]
public class AttackableTarget : MonoBehaviour 
{
    public delegate void Destroyed(AttackableTarget target);
    public event Destroyed OnDestroyed = new Destroyed((AttackableTarget target) => { });

    public bool IsAlive
    {
        get { return health.IsAlive; }
    }

    private Health health;

    private void Awake()
    {
        health = gameObject.GetComponentSafe<Health>();

        health.OnZeroHealth += ReachedZeroHealth;
    }

    private void ReachedZeroHealth()
    {
        OnDestroyed(this);
        Destroy(gameObject);
    }

    public void OnAttacked(float damage)
    {
        health.TakeDamage(damage);
    }
}
