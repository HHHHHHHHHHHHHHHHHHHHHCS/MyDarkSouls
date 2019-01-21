using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("===== Keys Setting====")]
    public KeyCode keyUp = KeyCode.W;//move up
    public KeyCode keyDown = KeyCode.S;//move down
    public KeyCode keyLeft = KeyCode.A;//move left
    public KeyCode keyRight = KeyCode.D;//move right
    public KeyCode keyA = KeyCode.LeftShift;//run
    public KeyCode keyB = KeyCode.S;
    public KeyCode keyC = KeyCode.A;
    public KeyCode keyD = KeyCode.D;

    [Header("===== Out Signals =====")]
    private float dUp, dRight;
    public float dmag; //向前的量
    public Vector3 dVec; //旋转的角度
    public bool isRun;//是否在跑

    [Header("===== Others =====")]
    public bool inputEnable = true;


    public float targetDUp, targetDRight, velocityDUp, velocityDRight;

    private void Update()
    {
        if (inputEnable)
        {
            targetDUp = (Input.GetKey(keyUp) ? 1 : 0) - (Input.GetKey(keyDown) ? 1 : 0);
            targetDRight = (Input.GetKey(keyRight) ? 1 : 0) - (Input.GetKey(keyLeft) ? 1 : 0);
        }
        else
        {
            targetDUp = 0;
            targetDRight = 0;
        }

        dUp = Mathf.SmoothDamp(dUp, targetDUp, ref velocityDUp, 0.1f);
        dRight = Mathf.SmoothDamp(dRight, targetDRight, ref velocityDRight, 0.1f);


        dmag = Mathf.Sqrt(dUp * dUp + dRight * dRight);
        dVec = (dRight * transform.right + dUp * transform.forward).normalized;

        isRun = Input.GetKey(keyA);
    }
}