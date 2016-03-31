// ElementalEffectReceiver.cs
// ManaCraft
// Created by Rohun Banerji on March 30, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]
public class ElementalEffectReceiver : MonoBehaviour 
{
    private List<ElementalEffect> activeElementalEffects = new List<ElementalEffect>();

    private Health health;

    private void Awake()
    {
        health = gameObject.GetComponentSafe<Health>();
    }

    public void Apply(ElementalEffect elementalEffect)
    {
        int indexOfElementalEffectType = activeElementalEffects.FindIndex((effect)=>effect.elementalEffectType == elementalEffect.elementalEffectType);

        bool elementalEffectAlreadyActive = (indexOfElementalEffectType != -1);

        if(elementalEffectAlreadyActive)
        {
            //Replace the old one
            //#TODO Implement a comparison operator to determine which is better
            activeElementalEffects[indexOfElementalEffectType] = elementalEffect;
        }
        else
        {
            activeElementalEffects.Add(elementalEffect);
        }
    }

    private void Update()
    {
        for (int i = activeElementalEffects.Count - 1; i >= 0; --i)
        {
            ElementalEffect elementalEffect = activeElementalEffects[i];
            if (elementalEffect.durationS > 0.0f)
            {
                ApplyEffect(elementalEffect, Time.deltaTime);
                elementalEffect.durationS -= Time.deltaTime;
                activeElementalEffects[i] = elementalEffect; //Value type
            }
            else
            {
                activeElementalEffects.RemoveAt(i);
            }
        }
    }

    private void ApplyEffect(ElementalEffect effect, float dt)
    {
        switch (effect.elementalEffectType)
        {
            case ElementalEffect.ElementalEffectType.None:
                break;
            case ElementalEffect.ElementalEffectType.Burn:
            {
                float damagePerDT = effect.damagePerSecond * dt;
                health.TakeDamage(damagePerDT);

                break;
            }
            case ElementalEffect.ElementalEffectType.Slow:
            {
                Assert.IsTrue(false);
                break;
            }
            case ElementalEffect.ElementalEffectType.Stun:
            {
                Assert.IsTrue(false);
                break;
            }
            case ElementalEffect.ElementalEffectType.Piercing:
            {
                Assert.IsTrue(false);
                break;
            }
            default:
            {
                Assert.IsTrue(false);
                break;
            }
        }
    }
}
