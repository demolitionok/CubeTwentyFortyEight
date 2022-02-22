using System;
using UnityEngine;
using UnityEngine;
using UnityEngine.Events;

public class CubeSpawner : MonoBehaviour
{
    #region serializedFields
    [SerializeField]
    private GameObject playerCubePrefab;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform leftBound;
    [SerializeField]
    private Transform rightBound;
    [SerializeField]
    private float newCubeSpawnForce;
    [SerializeField]
    private UnityEvent<CubeMovement> OnPlayerCubeSpawn;
    [SerializeField]
    private UnityEvent<int> OnScoreChange;
    #endregion

    private int score = 0;

    private void AddScore(int scoreToAdd) 
    {
        score += scoreToAdd;
        OnScoreChange?.Invoke(score);
    }

    private Vector3 spawnPosition { get => spawnPoint.position; }

    private void SpawnUpgradedCube(int value, Vector3 pos)
    {
        var cube = SpawnCube(value, pos);
        var cubeCollider = cube.GetComponent<CubeDataCollider>();
        cubeCollider.OnEqualCubeCollision += MergeCubes;

        var movement = cube.GetComponent<CubeMovement>();
        Destroy(movement);

        var rigidbody = cube.GetComponent<Rigidbody>();

        var randX = UnityEngine.Random.Range(-1f, 1f);
        var randZ = UnityEngine.Random.Range(-0.2f, 1f);
        Vector3 forceDirection = (new Vector3(randX, 1f, randZ)).normalized;

        rigidbody.AddForce(forceDirection * newCubeSpawnForce, ForceMode.Impulse);
        rigidbody.AddForceAtPosition(Vector3.forward * 2f, new Vector3(-0.3f, 0.1f, 0.6f), ForceMode.Impulse);
    }

    private void SpawnStartCube()
    {
        int val = UnityEngine.Random.Range(2, 5);
        var resultVal = (val / 2) * 2;
        var cube = SpawnCube(resultVal, spawnPosition, true);

        var cubeCollider = cube.GetComponent<CubeDataCollider>();
        cubeCollider.OnEqualCubeCollision += MergeCubes;
        cubeCollider.OnCubeAnyCollision += SpawnStartCube;

        var movement = cube.GetComponent<CubeMovement>();
        movement.Init(leftBound, rightBound);

        OnPlayerCubeSpawn?.Invoke(movement);
    }

    private GameObject SpawnCube(int value, Vector3 pos, bool isStartCube = false)
    {
        var cube = Instantiate(playerCubePrefab, pos, Quaternion.identity);
        var data = cube.GetComponent<CubeData>();
        data.Init(value, isStartCube);

        return cube;
    }

    private void MergeCubes(CubeData cubeData1, CubeData cubeData2) 
    {
        var pos1 = cubeData1.transform.position;
        var pos2 = cubeData2.transform.position;

        var newPos = MiddlePos(pos1, pos2);
        var newValue = cubeData1.value + cubeData2.value;
        AddScore(newValue);

        Destroy(cubeData1.gameObject);
        Destroy(cubeData2.gameObject);

        SpawnUpgradedCube(newValue, newPos);
    }
    private Vector3 MiddlePos(Vector3 vector1, Vector3 vector2)
    {
        var resultX = (vector1.x + vector2.x) / 2f;
        var resultY = (vector1.y + vector2.y) / 2f;
        var resultZ = (vector1.z + vector2.z) / 2f;

        return new Vector3(resultX, resultY, resultZ);
    }

    private void Start()
    {
        AddScore(0);
        SpawnStartCube();
    }
}
