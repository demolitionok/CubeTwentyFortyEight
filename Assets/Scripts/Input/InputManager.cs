using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private CubeMovement cube;

    private bool performed = false;

    private void Awake()
    {
    }


    private void HandleTouch()
    {
        var touch = Input.GetTouch(0);
        var phase = touch.phase;
        var pos = touch.position;
        switch (phase) 
        {
            case TouchPhase.Moved:
                cube.MoveTo(pos);
                break;
            case TouchPhase.Ended:
                cube.ThrowForward();
                break;
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            HandleTouch();
        }
    }
}
