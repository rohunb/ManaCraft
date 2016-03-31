// ElementalEffects.cs
// ManaCraft
// Created by Rohun Banerji on March 30, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct ElementalEffect
{
    public enum ElementalEffectType { None, Burn, Slow, Stun, Piercing };

    public ElementalEffectType elementalEffectType;
    public float durationS;
    public float damagePerSecond;
    public float movementReduction;

    public bool DoesDamage()
    {
        bool isTypeThatDoesDamage = (elementalEffectType == ElementalEffectType.Burn);

        bool validDamage = (damagePerSecond > 0.0f);
        Assert.IsTrue(validDamage);

        bool validDuration = (durationS > 0.0f);
        Assert.IsTrue(validDuration);

        bool doesDamage = isTypeThatDoesDamage && validDamage && validDuration;
        return doesDamage;
    }
}
