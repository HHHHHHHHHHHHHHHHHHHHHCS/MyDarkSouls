using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput:MonoBehaviour
{

    [Header("===== Out Signals =====")]
    protected float dUp, dRight; //上下左右的量
    public float dmag; //向前的量
    public Vector3 dVec; //旋转的角度
    public bool isRun; //是否在跑
    public bool jump; //是否在跳跃
    protected bool lastJump; //最后是否按下了跳跃
    public float jUp, jRight; //镜头上下左右
    public bool attack; //是否在攻击
    protected bool lastAttack; //最后是否按下了攻击


    [Header("===== Others =====")]
    public bool inputEnable = true;
    public float targetDUp, targetDRight, velocityDUp, velocityDRight;


    /// <summary>
    /// 把矩形坐标映射成圆形
    /// </summary>
    protected Vector2 SquareToCircle(float x, float y)
    {
        Vector2 output;
        output.x = x * Mathf.Sqrt(1 - (y * y) / 2.0f);
        output.y = y * Mathf.Sqrt(1 - (x * x) / 2.0f);
        return output;
    }
}
