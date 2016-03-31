// AttackableTarget.cs
// ManaCraft
// Created by Rohun Banerji on March 24, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Health), typeof(ElementalEffectReceiver))]
public class AttackableTarget : MonoBehaviour 
{
    public delegate void Destroyed(AttackableTarget target);
    public event Destroyed OnDestroyed = new Destroyed((AttackableTarget target) => { });

    public bool IsAlive
    {
        get { return health.IsAlive; }
    }

    private Health health;
    private ElementalEffectReceiver elementalEffectReceiver;

    private void Awake()
    {
        health = gameObject.GetComponentSafe<Health>();
        elementalEffectReceiver = gameObject.GetComponentSafe<ElementalEffectReceiver>();
    }

    private void Start()
    {
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

    public void OnElementalEffect(ElementalEffect elementalEffect)
    {
        elementalEffectReceiver.Apply(elementalEffect);
    }
}
