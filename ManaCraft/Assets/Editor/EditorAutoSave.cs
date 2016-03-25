// EditorAutoSave.cs
// ManaCraft
// Created by Rohun Banerji on March 23, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[InitializeOnLoad]
public class EditorAutoSave
{
    static float autoSaveTimeSeconds = 600.0f;

    static float timeToSave = 0;

    static EditorAutoSave()
    {
        timeToSave = Time.realtimeSinceStartup + autoSaveTimeSeconds;

        EditorApplication.update += Update;
    }

    static void Update()
    {
        float currentTime = Time.realtimeSinceStartup;

        bool shouldSave = EditorSceneManager.GetActiveScene().isDirty
                        && currentTime >= timeToSave;

        if(shouldSave)
        {
            Debug.Log("Autosaving...");

            EditorSceneManager.SaveOpenScenes();
            timeToSave = currentTime + autoSaveTimeSeconds;
        }
    }
}
