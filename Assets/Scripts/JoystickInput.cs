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
    public string btnA = "btnA";
    public string btnB = "btnB";
    public string btnC = "btnC";
    public string btnD = "btnD";

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


}
