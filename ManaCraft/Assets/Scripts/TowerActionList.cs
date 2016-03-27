// TowerActionList.cs
// ManaCraft
// Created by Rohun Banerji on March 25, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

public enum TowerAction
{
    //Targeting
    GetFirstTarget,
    RetargetAfterAttacking,

    //Attack Visual
    LaunchProjectileMesh,
    CreateLineRendererEffect,
    CreateParticleEffect,
    CreateConeEffect,

    //Impact Visual
    CreateImpactVFX,
    PlayImpactSound,

    //Acquire Target
    TargetCurrentTarget,
    GetTargetsInAoEAroundPoint,
    GetTargetsInConeAoE,
    GetTargetsInChainAoE,

    //Do Damage
    DamageTargets,

    //Special Effects (Happens before damage)
    ApplyDamageOverTime,
    ApplySlow,
    ApplyStun,
    ArmourPenetration,

    //Wait for event
    WaitForProjectileCollisionWithTarget,
    WaitForProjectileCollisionWithGround,
    WaitForProjectileTravelTime,
}
