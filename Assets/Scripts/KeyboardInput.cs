using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput
{
    [Header("===== Keys Setting====")] public KeyCode keyUp = KeyCode.W; //move up
    public KeyCode keyDown = KeyCode.S; //move down
    public KeyCode keyLeft = KeyCode.A; //move left
    public KeyCode keyRight = KeyCode.D; //move right
    public KeyCode keyA = KeyCode.LeftShift; //isRun
    public KeyCode keyB = KeyCode.Space; //jump roll jab
    public KeyCode keyC = KeyCode.J; //isAttack
    public KeyCode keyD = KeyCode.K; //isDefense
    public KeyCode keyJUp = KeyCode.UpArrow; //camera up
    public KeyCode keyJDown = KeyCode.DownArrow; //camera down
    public KeyCode keyJLeft = KeyCode.LeftArrow; //camera left
    public KeyCode keyJRight = KeyCode.RightArrow; //camera right
    public KeyCode keyJStick = KeyCode.CapsLock; //camera islock

    [Header("===== Mouse Settings =====")] public bool mouseEnable = false; //mouse rotate camera enable?
    public float mouseSensitivityX = 1f; //mouse rotate camera X speed
    public float mouseSensitivityY = 1f; //mouse rotate camera Y speed
    public string mouseRotateX = "Mouse X"; //mouse move X
    public string mouseRotateY = "Mouse Y"; //mouse move Y
    public KeyCode mouseAttack = KeyCode.Mouse0; //mouse attack
    public KeyCode mouseDefense = KeyCode.Mouse1; //mouse Defense

    public MyButton buttonA = new MyButton();
    public MyButton buttonB = new MyButton();
    public MyButton buttonC = new MyButton();
    public MyButton buttonD = new MyButton();
    public MyButton buttonJStick = new MyButton();


    private void Update()
    {
        buttonA.Tick(Input.GetKey(keyA));
        buttonB.Tick(Input.GetKey(keyB));
        buttonC.Tick(Input.GetKey(mouseEnable ? mouseAttack : keyC));
        buttonD.Tick(Input.GetKey(mouseEnable ? mouseDefense : keyD));
        buttonJStick.Tick(Input.GetKey(keyJStick));

        if (mouseEnable)
        {
            jUp = Input.GetAxis("Mouse Y") * mouseSensitivityY;
            jRight = Input.GetAxis("Mouse X") * mouseSensitivityX;
        }
        else
        {
            jUp = (Input.GetKey(keyJUp) ? 1.0f : 0f) - (Input.GetKey(keyJDown) ? 1.0f : 0f);
            jRight = (Input.GetKey(keyJRight) ? 1.0f : 0f) - (Input.GetKey(keyJLeft) ? 1.0f : 0f);
        }


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
        float tempDRight = tempDAxis.x;
        float tempDUp = tempDAxis.y;

        dmag = Mathf.Sqrt(tempDUp * tempDUp + tempDRight * tempDRight);
        dVec = tempDRight * transform.right + tempDUp * transform.forward;

        //玩家长按跑动要么在双击期间
        isRun = (buttonA.IsPressing && !buttonA.IsDelaying) || buttonA.IsExtending;
        //玩家锁定
        islock = buttonJStick.OnPressed;
        isDefense = buttonD.IsPressing;
        //双击跳跃
        jump = buttonA.OnPressed && buttonA.IsExtending;
        //短按翻滚
        roll = buttonA.OnReleased && buttonA.IsDelaying;
        attack = buttonC.OnPressed;
    }
}