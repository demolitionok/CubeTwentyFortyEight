using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CubeData : MonoBehaviour
{
    public int value { get; private set; }

    public event Action<int> OnValueChange;

    public void SetValue(int newValue) 
    {
        value = newValue;
        OnValueChange?.Invoke(newValue);
    }
}
