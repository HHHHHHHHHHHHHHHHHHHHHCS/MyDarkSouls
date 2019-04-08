using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer
{
    public enum State
    {
        Idle,
        Run,
        Finished
    }

    /// <summary>
    /// 当前状态
    /// </summary>
    public State state;

    /// <summary>
    /// 每次间隔
    /// </summary>
    public float duration = 1f;

    /// <summary>
    /// 当前的计时器
    /// </summary>
    private float elapsedTime = 0;

    public void Tick()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Run:
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= duration)
                {
                    state = State.Finished;
                }

                break;
            }
            case State.Finished:
                break;
            default:
                Debug.LogError("MyTimer Error");
                break;
        }
    }

    public void Go()
    {
        elapsedTime = 0;
        state = State.Run;
    }
}