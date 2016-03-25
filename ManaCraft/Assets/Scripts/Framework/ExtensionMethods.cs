// ExtensionMethods.cs
// ManaCraft
// Created by Rohun Banerji on March 23, 2016.
// Copyright (c) 2016 Rohun Banerji. All rights reserved.

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

//this class adds extended functionality to various built in classes like Transform, GameObject, etc.
public static class ExtensionMethods
{
    #region Vector Extensions
    public static Vector2 ToVector2(this Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.y);
    }

    public static Vector3 ToVector3(this Vector2 vec2)
    {
        return new Vector3(vec2.x, vec2.y, 0.0f);
    }
    
    public static Vector4 ToVector4(this Vector3 vec3)
    {
        return new Vector4(vec3.x, vec3.y, vec3.z, 1.0f);
    }
    
    public static void SetX(this Vector3 vec, float value)
    {
        vec.Set(value, vec.y, vec.z);
    }
    
    public static void SetY(this Vector3 vec, float value)
    {
        vec.Set(vec.x, value, vec.z);
    }
    
    public static void SetZ(this Vector3 vec, float value)
    {
        vec.Set(vec.x, vec.y, value);
    }
    #endregion Vector Extensions

    #region Transform Extensions
    
    public static void ResetTransform(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1f, 1f, 1f);
    }
    
    public static void TranslateX(this Transform trans, float distance)
    {
        trans.Translate(new Vector3(distance, 0.0f, 0.0f));
    }
    
    public static void TranslateY(this Transform trans, float distance)
    {
        trans.Translate(new Vector3(0.0f, distance, 0.0f));
    }
    
    public static void TranslateZ(this Transform trans, float distance)
    {
        trans.Translate(new Vector3(0.0f, 0.0f, distance));
    }
    
    public static void SetPositionX(this Transform trans, float x)
    {
        Vector3 newPosition = new Vector3(x, trans.position.y, trans.position.z);
        trans.position = newPosition;
    }
    
    public static void SetPositionY(this Transform trans, float y)
    {
        Vector3 newPosition = new Vector3(trans.position.x, y, trans.position.z);
        trans.position = newPosition;
    }

    public static void SetPositionZ(this Transform trans, float z)
    {
        Vector3 newPosition = new Vector3(trans.position.x, trans.position.y, z);
        trans.position = newPosition;
    }

    public static void RotateAroundXAxis(this Transform trans, float angle)
    {
        trans.Rotate(angle, 0f, 0f);
    }

    public static void RotateAroundYAxis(this Transform trans, float angle)
    {
        trans.Rotate(0f, angle, 0f);
    }

    public static void RotateAroundZAxis(this Transform trans, float angle)
    {
        trans.Rotate(0f, 0f, angle);
    }

    public static void SetEulerX(this Transform trans, float x)
    {
        Vector3 newRot = new Vector3(x, trans.eulerAngles.y, trans.eulerAngles.z);
        trans.eulerAngles = newRot;
    }

    public static void SetEulerY(this Transform trans, float y)
    {
        Vector3 newRot = new Vector3(trans.eulerAngles.x, y, trans.eulerAngles.z);
        trans.eulerAngles = newRot;
    }

    public static void SetEulerZ(this Transform trans, float z)
    {
        Vector3 newRot = new Vector3(trans.eulerAngles.x, trans.eulerAngles.y, z);
        trans.eulerAngles = newRot;
    }

    public static void LookAtWithSameY(this Transform trans, Vector3 target)
    {
        Vector3 lookAtPos = new Vector3(target.x, trans.position.y, target.z);
        trans.LookAt(lookAtPos);
    }
    #endregion

    #region RectTransform Extensions
    public static Vector2 Size(this RectTransform trans)
    {
        return trans.rect.size;
    }
    public static float Width(this RectTransform trans)
    {
        return trans.rect.width;
    }
    public static float Height(this RectTransform trans)
    {
        return trans.rect.height;
    }
    public static void SetSize(this RectTransform trans, Vector2 newSize)
    {
        Vector2 oldSize = trans.rect.size;
        Vector2 deltaSize = newSize - oldSize;
        trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
        trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
    }
    public static void SetWidth(this RectTransform trans, float newSize)
    {
        SetSize(trans, new Vector2(newSize, trans.rect.size.y));
    }
    public static void SetHeight(this RectTransform trans, float newSize)
    {
        SetSize(trans, new Vector2(trans.rect.size.x, newSize));
    }
    #endregion RectTransform Extensions

    #region GameObject Extensions
    
    public static T GetComponentSafe<T>(this GameObject obj) where T : Component
    {
        T component = obj.GetComponent<T>();
        Assert.IsNotNull(component, "Could not find " + typeof(T) + " component");
        return component;
    }

    public static T GetComponentInParent<T> (this GameObject go) where T: Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();
        if (comp != null) return comp;
        Transform trans = go.transform.parent;
        while(trans!=null && comp == null)
        {
            comp = trans.gameObject.GetComponent<T>();
            trans = trans.parent;
        }
        return comp;
    }
    #endregion

    #region Rigidbody Extensions
    public static void ResetMovement(this Rigidbody _rigidbody)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    public static void SetVelocityX(this Rigidbody _rigidbody, float x)
    {
        Vector3 newVelocity = new Vector3(x, _rigidbody.velocity.y, _rigidbody.velocity.z);
        _rigidbody.velocity = newVelocity;
    }

    public static void SetVelocityY(this Rigidbody _rigidbody, float y)
    {
        Vector3 newVelocity = new Vector3(_rigidbody.velocity.x, y, _rigidbody.velocity.z);
        _rigidbody.velocity = newVelocity;
    }

    public static void SetVelocityZ(this Rigidbody _rigidbody, float z)
    {
        Vector3 newVelocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, z);
        _rigidbody.velocity = newVelocity;
    }

    #endregion

    #region Material Extensions
    public static Color WithAplha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
    #endregion

    #region List Extensions
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while(n-- > 1)
        {
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    #endregion List Extensions

}


