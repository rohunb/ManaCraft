// FillBar.cs
// ManaCraft
// Created by Rohun Banerji on March 30, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class FillBar : MonoBehaviour 
{
    [SerializeField]
    private Image fillImage;
    private float fillSpeed = 10.0f;

    private Coroutine fillRoutine;

    private void Awake()
    {
        Assert.IsNotNull(fillImage);
    }

    public void SetValue(float value, bool lerp)
    {
        Assert.IsTrue(value >= 0.0f && value <= 1.0f);

        if(lerp)
        {
            //Fill routine is currently active
            if(fillRoutine != null)
            {
                StopCoroutine(fillRoutine);
            }

            fillRoutine = StartCoroutine(LerpToTargetValue(value));
        }
        else
        {
            fillImage.fillAmount = value;
        }
    }

    public void ChangeValue(float delta, bool lerp)
    {
        SetValue(fillImage.fillAmount + delta, lerp);
    }

    public void SetFillColour(Color fillColour)
    {
        fillImage.color = fillColour;
    }

    private IEnumerator LerpToTargetValue(float targetValue)
    {
        float currentValue = fillImage.fillAmount;

        while(!Mathf.Approximately(currentValue, targetValue))
        {
            currentValue = Mathf.Lerp(currentValue, targetValue, fillSpeed * Time.deltaTime);
            fillImage.fillAmount = currentValue;
            yield return null;
        }

        fillImage.fillAmount = targetValue;
        fillRoutine = null;
    }
}
