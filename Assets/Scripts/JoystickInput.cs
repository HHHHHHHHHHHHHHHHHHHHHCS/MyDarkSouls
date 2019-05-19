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
    public string btnA = "btn0";
    public string btnB = "btn1";
    public string btnC = "btn2";
    public string btnD = "btn3";
    public string btnLB = "btnLB";
    public string btnLT = "btnLT";
    public string btnJStick = "btn11";

    public MyButton buttonA = new MyButton();
    public MyButton buttonB = new MyButton();
    public MyButton buttonC = new MyButton();
    public MyButton buttonD = new MyButton();
    public MyButton buttonLB = new MyButton();
    public MyButton buttonLT = new MyButton();
    public MyButton buttonJStick = new MyButton();

    private void Update()
    {
        buttonA.Tick(Input.GetButton(btnA));
        buttonB.Tick(Input.GetButton(btnB));
        buttonC.Tick(Input.GetButton(btnC));
        buttonD.Tick(Input.GetButton(btnD));
        buttonLB.Tick(Input.GetButton(btnLB));
        buttonLT.Tick(Input.GetButton(btnLT));
        buttonJStick.Tick(Input.GetButton(btnJStick));

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

        //玩家长按跑动要么在双击期间
        isRun = (buttonA.IsPressing && !buttonA.IsDelaying) || buttonA.IsExtending;
        //玩家锁定
        islock = buttonJStick.OnPressed;
        isDefense = buttonLB.IsPressing;
        //双击跳跃
        jump = buttonA.OnPressed && buttonA.IsExtending;
        //短按翻滚
        roll = buttonA.OnReleased && buttonA.IsDelaying;
        attack = buttonC.OnPressed;
    }
}