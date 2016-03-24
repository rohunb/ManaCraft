// ScriptCopyrightInfoInjector.cs
// ManaCraft
// Created by Rohun Banerji on March 23, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;
using System.IO;
using System;


// #SCRIPTNAME#.cs
// #PROJECTNAME#
// Created by Rohun Banerji on #CREATIONDATE#.
// Copyright (c) #YEAR# Rohun Banerji. All rights reserved.

//Injects copyright info into the template above
public class ScriptCopyrightInfoInjector : UnityEditor.AssetModificationProcessor 
{
    const string scriptExtension = ".cs";
    const string metaExtension = ".meta";
    const string creationDateTag = "#CREATIONDATE#";
    const string projectNameTag = "#PROJECTNAME#";
    const string yearTag = "#YEAR#";

    public static void OnWillCreateAsset(string path)
    {
        Debug.Log("Script: " + path);
        //not a script
        if (!path.Contains(scriptExtension))
        {
            Debug.Log("not cs");
            return;
        }

        //The callback receives the path to the meta file
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        string ext = path.Substring(index);
        if (ext != scriptExtension)
        {
            Debug.Log("meta ext " + ext);
            return;
        }

        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        string file = File.ReadAllText(path);

        if(!file.Contains(creationDateTag))
        {
            Debug.LogWarning("New script added, but is missing the " + creationDateTag + "Tag");
        }

        if(!file.Contains(projectNameTag))
        {
            Debug.LogWarning("New script added, but is missing the " + projectNameTag + "Tag");
        }

        if(!file.Contains(yearTag))
        {
            Debug.LogWarning("New script added, but is missing the " + yearTag + "Tag");
        }

        file = file.Replace(creationDateTag, DateTime.Now.ToString("MMMM dd, yyyy"));
        file = file.Replace(projectNameTag, PlayerSettings.productName);
        file = file.Replace(yearTag, DateTime.Now.Year.ToString());
        File.WriteAllText(path, file);

        AssetDatabase.Refresh();
    }

}
