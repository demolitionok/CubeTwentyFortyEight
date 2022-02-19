using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject playerCubePrefab;

    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform leftBound;
    [SerializeField]
    private Transform rightBound;
    [SerializeField]
    private float spawnForce;
    [SerializeField]
    private UnityEvent<CubeMovement> OnPlayerCubeSpawn;

    int count = 0;
    private bool merging = false;

    private Vector3 spawnPosition { get => spawnPoint.position; }

    private void SpawnUpgradedCube(int value, Vector3 pos) 
    {
        var cube = SpawnCube(value, pos);
        var cubeCollider = cube.GetComponent<CubeDataCollider>();
        cubeCollider.OnEqualCubeCollision += MergeCubes;

        var movement = cube.GetComponent<CubeMovement>();
        Destroy(movement);

        var rigidbody = cube.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.up*spawnForce, ForceMode.Impulse);
    }

    private void SpawnStartCube() 
    {
        var cube = SpawnCube(2, spawnPosition);
        var cubeCollider = cube.GetComponent<CubeDataCollider>();
        cubeCollider.OnEqualCubeCollision += MergeCubes;
        cubeCollider.OnCubeAnyCollision += SpawnStartCube;

        var movement = cube.GetComponent<CubeMovement>();
        movement.Init(leftBound, rightBound);

        OnPlayerCubeSpawn?.Invoke(movement);
    }

    private Vector3 MiddlePos(Vector3 vector1, Vector3 vector2)
    {
        var resultX = (vector1.x + vector2.x) / 2f;
        var resultY = (vector1.y + vector2.y) / 2f;
        var resultZ = (vector1.z + vector2.z) / 2f;

        return new Vector3(resultX, resultY, resultZ);
    }

    private GameObject SpawnCube(int value, Vector3 pos)
    {
        var cube = Instantiate(playerCubePrefab, pos, Quaternion.identity);
        var data = cube.GetComponent<CubeData>();
        data.SetValue(value);

        return cube;
    }

    private void MergeCubes(CubeData cubeData1, CubeData cubeData2) 
    {
        count++;
        if (count == 2)
        {
            var pos1 = cubeData1.transform.position;
            var pos2 = cubeData2.transform.position;

            var newPos = MiddlePos(pos1, pos2);
            var newValue = cubeData1.value + cubeData2.value;

            Destroy(cubeData1.gameObject);
            Destroy(cubeData2.gameObject);

            SpawnUpgradedCube(newValue, newPos);
            count = 0;
        }
    }

    private void Start()
    {
        SpawnStartCube();
    }
}
