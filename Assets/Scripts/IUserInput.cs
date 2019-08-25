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
    public bool islock; //是否开启了锁定
    public float jUp, jRight; //镜头上下左右
    //public bool attack; //是否在攻击
    public bool roll; //是否在翻滚

    public bool isDefense; //是否在防御
    public bool leftAttack;//是否左手攻击
    public bool lt;
    public bool lb;
    public bool rightAttack;//是否右手攻击
    public bool rt;
    public bool rb;

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

    protected void UpdateDmagDevc(float dUp2, float dRight2)
    {
        dmag = Mathf.Sqrt(dUp2 * dUp2 + dRight2 * dRight2);
        dVec = dUp2 * transform.forward + dRight2 * transform.right;
    }
}
