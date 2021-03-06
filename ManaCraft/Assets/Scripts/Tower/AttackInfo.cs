// DamageType.cs
// ManaCraft
// Created by Rohun Banerji on March 25, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class AttackInfo
{
    public enum ElementType { Physical, Earth, Fire, Wind, Water }

    public enum AttackStyle { DirectDamage, PBAoE, TargetedAoE, Cone, Chain }

    public enum AttackVisual
    {
        LaunchProjectileMesh,
        LineRendererEffect,
        ParticleEffect,
        ConeEffect,
    }

    public enum AttackVisualTarget
    {
        TargetCreep,
        TargetGroundBelowCreep
    }

    public enum DamageEffectDelayType
    {
        Instant,
        WaitForProjectileTravelTime,
        WaitForProjectileGroundImpact
    }

    public enum ImpactEffectType
    {
        AtTarget,
        OnGround,
        ChainTargets
    }

    public enum AttackImpactEffect
    {
        VFX,
        Sound
    }

    public enum TargetAcquisition
    {
        TargetCurrentTarget,
        GetTargetsInAoEAroundPoint,
        GetTargetsInConeAoE,
        GetTargetsInChainAoE,
    }

    public ElementType elementType;
    public AttackStyle attackStyle;

    public AttackVisual attackVisual;
    public AttackVisualTarget attackVisualTarget;
    public DamageEffectDelayType damageEffectDelay;
    public ImpactEffectType impactEffectType;
    public List<AttackImpactEffect> attackImpactEffectList;
    public TargetAcquisition targetAcquisition;
    public List<ElementalEffect> elementalEffects;

    //Universal
    public float damage;
    public float range; //Unity units/Meters
    public float attacksPerSecond;

    //Fire
    public float damagePerTick; //Ticks every second
    public float dotDuration; //Seconds

    //Water
    public float slowPercentage;

    //Earth
    public float stunDuration; //Seconds

    //Wind
    public float armourPenetration; //Out of 10

    //Projectile
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed;

    //Line Renderer
    public float lineEffectDuration;

    public float groundTargetAoERadius;

    public int numChainJumps;
    public float chainAoERadius;
}
