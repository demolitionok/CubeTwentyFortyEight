using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(MeshRenderer), typeof(CubeData))]
public class CubeViewController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] texts = new TextMeshProUGUI[6];

    private CubeData cubeData;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        cubeData = GetComponent<CubeData>();
    }

    private void OnEnable()
    {
        cubeData.OnValueChange += ChangeColor;
        cubeData.OnValueChange += ChangeTextes;
    }

    private void OnDisable()
    {
        cubeData.OnValueChange -= ChangeColor;
        cubeData.OnValueChange -= ChangeTextes;
    }

    private void ChangeColor(int value) 
    {
        meshRenderer.material.color = value.IntToRGB();
    }

    private void ChangeTextes(int value)
    {
        foreach (var text in texts) 
        {
            text.text = value.ToString();
        }
    }
}
