using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    float distance = 10.0f;

    float height = 8.0f;
    float rotationDamping = 2.0f;

    private void LateUpdate()
    {
        float currentAngleY = Mathf.LerpAngle(transform.eulerAngles.y, target.eulerAngles.y, rotationDamping * Time.deltaTime);

        Quaternion rot = Quaternion.Euler(0, currentAngleY, 0);

        transform.position = target.position - (rot * Vector3.forward * distance) + (Vector3.up * height);
        transform.LookAt(target); 
    }
}