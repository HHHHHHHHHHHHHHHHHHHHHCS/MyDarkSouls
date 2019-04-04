using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : IUserInput
{
    [Header("===== Joystick Setting =====")]
    public string axisX = "AxisX";
    public string axisY = "AxisY";
    public string axisJRight = "Axis3";
    public string axisJUp = "Axis5";
    public string btn0 = "btn0";
    public string btn1 = "btn1";
    public string btn2 = "btn2";
    public string btn3 = "btn3";
    public string btnLB = "btnLB";
    public string btnLT = "btnLT";

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

        isRun = Input.GetButton(btn0);
        isDefense = Input.GetButton(btnLB);

        bool newJump = Input.GetButton(btn1);
        //这个的作用是要抬起按下,在重新赋值
        if (newJump != lastJump && newJump)
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
        lastJump = newJump;


        bool newAttack = Input.GetButton(btn2);
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


}
