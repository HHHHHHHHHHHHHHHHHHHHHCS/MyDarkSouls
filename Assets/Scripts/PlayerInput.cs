using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("===== Keys Setting====")] public KeyCode keyUp = KeyCode.W; //move up
    public KeyCode keyDown = KeyCode.S; //move down
    public KeyCode keyLeft = KeyCode.A; //move left
    public KeyCode keyRight = KeyCode.D; //move right
    public KeyCode keyA = KeyCode.LeftShift; //run
    public KeyCode keyB = KeyCode.Space;
    public KeyCode keyC = KeyCode.A;
    public KeyCode keyD = KeyCode.D;

    [Header("===== Out Signals =====")] private float dUp, dRight;
    public float dmag; //向前的量
    public Vector3 dVec; //旋转的角度
    public bool isRun; //是否在跑
    public bool jump; //是否在跳跃
    public bool lastJump; //最后的跳跃

    [Header("===== Others =====")] public bool inputEnable = true;


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


        Vector2 tempDAxis = SquareToCircle(dRight, dUp);
        float tempDright = tempDAxis.x;
        float tempDUp = tempDAxis.y;

        dmag = Mathf.Sqrt(tempDUp * tempDUp + tempDright * tempDright);
        dVec = tempDright * transform.right + tempDUp * transform.forward;

        isRun = Input.GetKey(keyA);

        bool newJump = Input.GetKey(keyB);
        if (newJump != lastJump && newJump)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

        lastJump = newJump;
    }

    /// <summary>
    /// 把矩形坐标映射成圆形
    /// </summary>
    private Vector2 SquareToCircle(float x, float y)
    {
        Vector2 output;
        output.x = x * Mathf.Sqrt(1 - (y * y) / 2.0f);
        output.y = y * Mathf.Sqrt(1 - (x * x) / 2.0f);
        return output;
    }
}