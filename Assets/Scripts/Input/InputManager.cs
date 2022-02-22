using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private CubeMovement cube;

    public void SetCube(CubeMovement cube) 
    {
        this.cube = cube;
    }

    private void HandleMouse() 
    {
        var mousePos = Input.mousePosition;

        cube.MoveTo(mousePos);
        if (Input.GetMouseButtonDown(0))
        {
            cube.ThrowForward();
        }
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
        if (cube == null)
        {
            Debug.Log("Input does not handling cube is null");
            return;
        }


        if (Input.touchCount > 0)
        {
            HandleTouch();
        }
        else
        {
            HandleMouse();
        }
    }
}
