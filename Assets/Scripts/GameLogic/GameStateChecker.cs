using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class GameStateChecker : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnGameEnd;

    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }

    private void EndGame() 
    {
        Debug.Log("GameEnded");
        OnGameEnd?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isCube = other.TryGetComponent(out CubeData cubeData);
        if (isCube && !cubeData.isStartCube) 
        {
            EndGame();
        }
    }
}
