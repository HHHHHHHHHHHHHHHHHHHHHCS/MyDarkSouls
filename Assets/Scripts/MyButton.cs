using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton
{
    public bool IsPressing = false; //是否正在按下
    public bool OnPressed = false; //是否第一次按下
    public bool OnReleased = false; //是否第一次抬起

    private bool curState = false;
    private bool lastState = false;

    public void Tick(bool input)
    {
        curState = input;

        IsPressing = curState;

        OnPressed = false;
        OnReleased = false;
        if (curState != lastState)
        {
            if (curState)
            {
                OnPressed = true;
            }
            else
            {
                OnReleased = true;
            }
        }

        lastState = curState;
    }
}