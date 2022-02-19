using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeMovement : MonoBehaviour
{
    [SerializeField]
    private Transform leftMovingBound;
    [SerializeField]
    private Transform rightMovingBound;
    [SerializeField]
    private float throwStrength;

    private new Rigidbody rigidbody;

    public Vector3 leftBound { get => leftMovingBound.position; }
    public Vector3 rightBound { get => rightMovingBound.position; }

    public void Init(Transform leftMovingBound, Transform rightMovingBound)
    {
        this.leftMovingBound = leftMovingBound;
        this.rightMovingBound = rightMovingBound;
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void MoveTo(Vector3 pos)
    {
        var percent = pos.x / Screen.width;
        var result = Mathf.Lerp(leftBound.x, rightBound.x, percent);
        transform.position = new Vector3(result, transform.position.y, transform.position.z);
    }

    public void ThrowForward() 
    {
        rigidbody.AddForce(transform.forward * throwStrength, ForceMode.Impulse);
        Destroy(this);
    }
}
