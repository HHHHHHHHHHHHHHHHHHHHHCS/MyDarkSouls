using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
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

    [Header("===== Out Signals =====")]
    private float dUp, dRight; //上下左右的量
    public float dmag; //向前的量
    public Vector3 dVec; //旋转的角度
    public bool isRun; //是否在跑
    public bool jump; //是否在跳跃
    public bool lastJump; //最后是否按下了跳跃
    public float jUp, jRight; //镜头上下左右
    public bool attack; //是否在攻击
    private bool lastAttack; //最后是否按下了攻击


    [Header("===== Others =====")] public bool inputEnable = true;


    public float targetDUp, targetDRight, velocityDUp, velocityDRight;

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

    /// <summary>
    /// 把矩形坐标映射成圆形
    /// </summary>
    private Vector2 SquareToCircle(float x, float y)
    {
        Vector2 output;
        output.x = x * Mathf.Sqrt(1 - (y * y) / 2.0f);
        output.y = y * Mathf.Sqrt(1 - (x * x) / 2.0f);
        return output;
    }
}