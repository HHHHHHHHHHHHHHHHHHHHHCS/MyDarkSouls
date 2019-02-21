using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float horizontalSpeed = 100f;
    public float verticalSpeed = 80.0f;

    private Transform playerHandle;
    private Transform cameraHandle;
    private PlayerInput pi;
    private float tempEulerX;

    private void Awake()
    {
        cameraHandle = transform.parent;
        playerHandle = cameraHandle.transform.parent;
        pi = playerHandle.GetComponent<PlayerInput>();
        tempEulerX = cameraHandle.eulerAngles.x;
    }


    private void Update()
    {
        if (pi.jRight != 0)
        {
            playerHandle.Rotate(Vector3.up, pi.jRight * horizontalSpeed * Time.deltaTime);

        }

        if (pi.jUp != 0)
        {
            tempEulerX += -pi.jUp * verticalSpeed * Time.deltaTime;
            tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
            var angles = cameraHandle.eulerAngles;
            angles.x = tempEulerX;
            cameraHandle.eulerAngles = angles;
        }
    }
}
