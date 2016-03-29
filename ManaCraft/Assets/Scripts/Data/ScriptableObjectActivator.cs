// ScriptableObjectActivator.cs
// ManaCraft
// Created by Rohun Banerji on March 27, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

//This just holds references to the various ScriptableObject data containers to instantiate them in each scene so the scriptable objects can be accessed statically without having individual references in every script that requires access to them
public class ScriptableObjectActivator : MonoBehaviour 
{
    [SerializeField]
    private TagsAndLayers tagsAndLayers;

    private void Awake()
    {
        Assert.IsNotNull(tagsAndLayers);
    }
}
