using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CubeData : MonoBehaviour
{
    public int value { get; private set; }

    public event Action<int> OnValueChange;
    public event Action<CubeData, CubeData> OnCubeCollision;

    public void SetValue(int newValue) 
    {
        value = newValue;
        OnValueChange?.Invoke(newValue);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;
        if (other.TryGetComponent(out CubeData cubeData)) 
        {
            if(cubeData.value == value)
                OnCubeCollision?.Invoke(this, cubeData);
        }
    }

    private void OnDestroy()
    {
        OnCubeCollision = null;
    }

}
