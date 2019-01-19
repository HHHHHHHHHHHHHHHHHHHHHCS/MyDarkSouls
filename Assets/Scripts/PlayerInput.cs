using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool inputEnable = true;
    public KeyCode keyUp = KeyCode.W;
    public KeyCode keyDown = KeyCode.S;
    public KeyCode keyLeft = KeyCode.A;
    public KeyCode keyRight = KeyCode.D;
    public float dUp, dRight, targetDUp, targetDRight, velocityDUp, velocityDRight;

    public float dmag; //向前的量
    public Vector3 dVec; //旋转的角度

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
        dVec = dRight * transform.right + dUp * transform.forward;
    }
}