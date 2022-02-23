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
    private bool merged = false;

    public event Action<CubeData, CubeData> OnEqualCubeCollision;
    public event Action OnCubeAnyCollision;

    private void DisableIsStartCube() => cubeData.isStartCube = false;

    private void Awake()
    {
        cubeData = GetComponent<CubeData>();
        OnCubeAnyCollision += DisableIsStartCube;
    }
    private void CompareCubes(GameObject other) 
    {
        if (other.TryGetComponent(out CubeData cubeData))
        {
            if (cubeData.value == this.cubeData.value)
            {
                if (other.TryGetComponent(out CubeDataCollider collider))
                {
                    if ((collider.merged || merged) == false)
                    {
                        collider.merged = true;
                        merged = true;
                        OnEqualCubeCollision?.Invoke(this.cubeData, cubeData);
                    }
                }
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;

        if (1 << collision.gameObject.layer == ignoreLayer.value)
        {
            return;
        }


        CompareCubes(other);

        OnCubeAnyCollision?.Invoke();
        OnCubeAnyCollision = null;
    }

    private void OnDestroy()
    {
        OnEqualCubeCollision = null;
        OnCubeAnyCollision = null;
    }
}
