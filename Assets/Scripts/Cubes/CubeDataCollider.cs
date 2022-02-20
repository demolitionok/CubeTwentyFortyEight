using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(CubeData))]
public class CubeDataCollider : MonoBehaviour
{
    [SerializeField]
    private LayerMask ignoreLayer;
    private CubeData cubeData;
    public event Action<CubeData, CubeData> OnEqualCubeCollision;
    public event Action OnCubeAnyCollision;

    private void Awake()
    {
        cubeData = GetComponent<CubeData>();
        OnCubeAnyCollision += () => cubeData.isStartCube = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;

        if (1 << collision.gameObject.layer == ignoreLayer.value)
        {
            return;
        }


        if (other.TryGetComponent(out CubeData cubeData))
        {
            if (cubeData.value == this.cubeData.value)
            {
                OnEqualCubeCollision?.Invoke(this.cubeData, cubeData);
            }
        }
        OnCubeAnyCollision?.Invoke();
        OnCubeAnyCollision = null;
    }

    private void OnDestroy()
    {
        OnEqualCubeCollision = null;
        OnCubeAnyCollision = null;
    }
}
