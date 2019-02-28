using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : MonoBehaviour
{
    [Header("===== Joystick Setting =====")]
    public string axisX = "AxisX";
    public string axisY = "AxisY";
    public string axisJRight = "Axis3";
    public string axisJUp = "Axis5";
    public string btnA = "btnA";
    public string btnB = "btnB";
    public string btnC = "btnC";
    public string btnD = "btnD";

    [Header("===== Out Signals =====")]
    private float dUp, dRight; //上下左右的量
    public float dmag; //向前的量
    public Vector3 dVec; //旋转的角度
    public bool isRun; //是否在跑
    public bool jump; //是否在跳跃
    public bool lastJump; //最后是否按下了跳跃
    public float jUp, jRight; //镜头上下左右
    public bool attack; //是否在攻击
    private bool lastAttack; //最后是否按下了攻击


    [Header("===== Others =====")]
    public bool inputEnable = true;
    public float targetDUp, targetDRight, velocityDUp, velocityDRight;

    private void Update()
    {
        jUp = Input.GetAxis(axisJUp);
        jRight = Input.GetAxis(axisJRight);

        if (inputEnable)
        {
            targetDUp = Input.GetAxis(axisY);
            targetDRight = Input.GetAxis(axisX);
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

        isRun = Input.GetButton(btnA);

        bool newJump = Input.GetButton(btnB);
        //这个的作用是要抬起按下,在重新赋值
        if (newJump != lastJump && newJump)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        lastJump = newJump;

        bool newAttack = Input.GetButton(btnC);
        if (newAttack != lastAttack && newAttack)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
        lastAttack = newAttack;
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
