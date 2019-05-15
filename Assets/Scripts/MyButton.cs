using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton
{
    /// <summary>
    /// 是否正在按下
    /// </summary>
    public bool IsPressing = false;

    /// <summary>
    /// 是否第一次按下
    /// </summary>
    public bool OnPressed = false;

    /// <summary>
    /// 是否第一次抬起
    /// </summary>
    public bool OnReleased = false;

    /// <summary>
    /// 是否双击 
    /// </summary>
    public bool IsDoubleClick => IsExtending && OnPressed;

    /// <summary>
    /// 扩展计时器是否在跑
    /// </summary>
    public bool IsExtending = false;

    /// <summary>
    /// 延迟计时器是否在跑
    /// </summary>
    public bool IsDelaying = false;


    [Header("==== Settings =====")] public float extendingDuration = 0.15f;
    public float delayingDuration = 1.0f;


    private bool curState = false;
    private bool lastState = false;

    private MyTimer extTimer = new MyTimer();
    private MyTimer delayingTimer = new MyTimer();

    public void Tick(bool input)
    {
        extTimer.Tick();
        delayingTimer.Tick();

        curState = input;

        IsPressing = curState;

        OnPressed = false;
        OnReleased = false;

        if (curState != lastState)
        {
            if (curState)
            {
                OnPressed = true;
                StartTimer(delayingTimer, delayingDuration);
            }
            else
            {
                OnReleased = true;
                StartTimer(extTimer, extendingDuration);
            }
        }

        lastState = curState;

        IsExtending = extTimer.state == MyTimer.State.Run;
        IsDelaying = delayingTimer.state == MyTimer.State.Run;
    }

    private void StartTimer(MyTimer timer, float duration)
    {
        timer.duration = duration;
        timer.Go();
    }
}