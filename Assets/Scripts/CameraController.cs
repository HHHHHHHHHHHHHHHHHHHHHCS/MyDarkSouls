using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Image lockDot;

    public float horizontalSpeed = 100f;
    public float verticalSpeed = 80.0f;
    public float cameraDampValue = 0.05f;

    public bool lockState;

    private Transform playerHandle;
    private Transform cameraHandle;
    private Transform cameraPos;
    private KeyboardInput pi;
    private float tempEulerX;
    private Transform model;
    private Camera camera;
    private Transform cameraTS;
    private LockTargetCls lockTarget;

    private Vector3 cameraDampVelocity;

    public LockTargetCls LockTarget
    {
        get => lockTarget;
        set
        {
            lockTarget = value;
            SetLockDot();
        }
    }

    private void Awake()
    {
        cameraHandle = transform;
        playerHandle = cameraHandle.transform.parent;
        pi = playerHandle.GetComponent<KeyboardInput>();
        tempEulerX = cameraHandle.eulerAngles.x;

        cameraPos = transform.Find("CameraPos").transform;

        camera = Camera.main;
        cameraTS = camera.transform;
        cameraTS.position = transform.position;

        Cursor.lockState = CursorLockMode.Locked; //hide mouse cursor 
        SetLockDot();
    }

    private void Start()
    {
        model = playerHandle.GetComponent<ActorController>().Model.transform;

        FixedUpdate();
    }

    private void Update()
    {
        if (LockTarget != null)
        {
            var pos = camera.WorldToScreenPoint(LockTarget.GetHalfPos);
            pos.z = 0;
            lockDot.rectTransform.position = pos;
        }
    }


    private void FixedUpdate()
    {
        if (LockTarget == null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;

            if (pi.jRight != 0)
            {
                playerHandle.Rotate(Vector3.up, pi.jRight * horizontalSpeed * Time.fixedDeltaTime);
            }

            if (pi.jUp != 0)
            {
                tempEulerX += -pi.jUp * verticalSpeed * Time.fixedDeltaTime;
                tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
                var angles = cameraHandle.eulerAngles;
                angles.x = tempEulerX;
                cameraHandle.eulerAngles = angles;
            }

            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            Vector3 tempForward = LockTarget.target.position - transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
            cameraTS.LookAt(LockTarget.target.position);
        }


        cameraTS.position = Vector3.SmoothDamp(cameraTS.transform.position, cameraPos.position, ref cameraDampVelocity,
            cameraDampValue);
        //cameraTS.transform.eulerAngles = transform.eulerAngles;
        cameraTS.transform.LookAt(transform);
    }

    public void LockUnlock()
    {
        //try to lock
        var modelOrigin1 = model.transform.position;
        var modelOrigin2 = modelOrigin1 + Vector3.up;
        var boxCenter = modelOrigin2 + model.forward * 5f;
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), model.rotation);

        if (cols.Length == 0)
        {
            LockTarget = null;
            return;
        }

        foreach (var item in cols)
        {
            if (item.CompareTag("Enemy"))
            {
                if (LockTarget != null && item.transform == LockTarget.target)
                {
                    LockTarget = null;
                    return;
                }
                else
                {
                    LockTarget = new LockTargetCls(item.transform, item.bounds.extents.y);
                    return;
                }
            }
        }
    }

    private void SetLockDot()
    {
        lockState = lockDot.enabled = LockTarget != null;
    }


    private void OnDrawGizmos()
    {
        if (LockTarget==null)
        {
            return;

        }
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(LockTarget.GetHalfPos,0.1f);
        if (!model)
            return;
        var modelOrigin1 = model.transform.position;
        var modelOrigin2 = modelOrigin1 + Vector3.up;
        var boxCenter = modelOrigin2 + model.forward * 5f;
        Gizmos.DrawCube(boxCenter, new Vector3(0.5f, 0.5f, 5f));
    }


    public class LockTargetCls
    {
        public Transform target;
        public float halfHeight;

        public Vector3 GetHalfPos => new Vector3(target.position.x, target.position.y + halfHeight, target.position.z);


        public LockTargetCls(Transform _target, float _halfHeight)
        {
            target = _target;
            halfHeight = _halfHeight;
        }
    }
}