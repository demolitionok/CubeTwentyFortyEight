using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Vector3 spawnPosition { get => spawnPoint.position; }

    private void SpawnPlayerCube() 
    {
        var cube = SpawnCube(2, spawnPosition);
        var movement = cube.GetComponent<CubeMovement>();
        movement.Init(leftBound, rightBound);
    }

    private Vector3 MiddlePos(Vector3 vector1, Vector3 vector2)
    {
        var resultX = (vector1.x + vector2.x) / 0.5f;
        var resultY = (vector1.y + vector2.y) / 0.5f;
        var resultZ = (vector1.z + vector2.z) / 0.5f;

        return new Vector3(resultX, resultY, resultZ);
    }

    private GameObject SpawnCube(int value, Vector3 pos)
    {
        var cube = Instantiate(playerCubePrefab, pos, Quaternion.identity);
        var data = cube.GetComponent<CubeData>();
        data.SetValue(value);
        data.OnCubeCollision += AddCubes;

        return cube;
    }

    private void AddCubes(CubeData cubeData1, CubeData cubeData2) 
    {
        var pos1 = cubeData1.transform.position;
        var pos2 = cubeData2.transform.position;

        var newPos = MiddlePos(pos1, pos2);
        var newValue = cubeData1.value + cubeData2.value;

        SpawnCube(newValue, newPos);
    }

    private void Start()
    {
        SpawnPlayerCube();
    }
}
