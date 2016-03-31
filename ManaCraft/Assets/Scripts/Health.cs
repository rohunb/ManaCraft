// Health.cs
// ManaCraft
// Created by Rohun Banerji on March 25, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour 
{
    [SerializeField]
    private float maxHealth = 0.0f;
    [SerializeField]
    private FillBar healthBar;

    public delegate void ZeroHealth();
    public event ZeroHealth OnZeroHealth = new ZeroHealth(() => { });

    public bool IsAlive
    {
        get { return currentHealth > 0.0f; }
    }

    private float currentHealth;

    private void Awake()
    {
        Assert.IsNotNull(healthBar);
    }

    private void Start()
    {
        Assert.IsTrue(maxHealth > 0.0f);
        currentHealth = maxHealth;
        healthBar.SetValue(1.0f, false);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth);

        healthBar.SetValue(currentHealth / maxHealth, true);

        //Debug.Log(gameObject.name + " Health = " + currentHealth);

        if(Mathf.Approximately(currentHealth, 0.0f))
        {
            OnZeroHealth();
        }
    }
}
