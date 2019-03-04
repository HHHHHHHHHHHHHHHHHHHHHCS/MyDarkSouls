using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput
{
    [Header("===== Keys Setting====")]
    public KeyCode keyUp = KeyCode.W; //move up
    public KeyCode keyDown = KeyCode.S; //move down
    public KeyCode keyLeft = KeyCode.A; //move left
    public KeyCode keyRight = KeyCode.D; //move right
    public KeyCode keyA = KeyCode.LeftShift; //run
    public KeyCode keyB = KeyCode.Space; //jump roll jab
    public KeyCode keyC = KeyCode.K;
    public KeyCode keyD = KeyCode.D;
    public KeyCode keyJUp = KeyCode.UpArrow; //camera up
    public KeyCode keyJDown = KeyCode.DownArrow; //camera down
    public KeyCode keyJLeft = KeyCode.LeftArrow; //camera left
    public KeyCode keyJRight = KeyCode.RightArrow; //camera right

    private void Update()
    {
        jUp = (Input.GetKey(keyJUp) ? 1.0f : 0f)- (Input.GetKey(keyJDown) ? 1.0f : 0f);
        jRight = (Input.GetKey(keyJRight) ? 1.0f : 0f) - (Input.GetKey(keyJLeft) ? 1.0f : 0f);

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

        bool newAttack = Input.GetKey(keyC);
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