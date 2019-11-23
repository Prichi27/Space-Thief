using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum RotationDirection
    {
        Left,
        Right
    }

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float rotationSpeed;

    private void LateUpdate()
    {
        var desiredPosition = target.position + offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }

    public void RotateCamera()
}
