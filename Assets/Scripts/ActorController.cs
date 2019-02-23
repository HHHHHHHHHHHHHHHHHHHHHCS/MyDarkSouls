using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ActorController : MonoBehaviour
{
    private readonly int forwardKey = Animator.StringToHash("forward");
    private readonly int jumpKey = Animator.StringToHash("jump");
    private readonly int isGroundKey = Animator.StringToHash("isGround");
    private readonly int rollKey = Animator.StringToHash("roll");
    private readonly int attackKey = Animator.StringToHash("attack");

    public float wakeSpeed = 1.4f;
    public float runMultiplier = 2.75f;
    public float jumpVelocity = 3.0f;
    public float rollVelocity = 3.0f;
    public float jabMultiplier = 1.0f;

    private PlayerInput pi;
    private Animator anim;
    private Rigidbody rigi;
    private Vector3 planarVec; //跳跃 平面量
    private Vector3 thrustVec; //跳跃 垂直量
    private bool lockPlanar; //锁移动的量

    private int attackLayer; //攻击的Layer

    public GameObject Model { get; private set; }


    private void Awake()
    {
        pi = transform.GetComponent<PlayerInput>();
        Model = transform.Find("ybot").gameObject;
        anim = Model.GetComponent<Animator>();
        rigi = transform.GetComponent<Rigidbody>();

        attackLayer = anim.GetLayerIndex("attack");
    }

    private void Update()
    {
        anim.SetFloat(forwardKey, pi.dmag * Mathf.Lerp(anim.GetFloat(forwardKey), pi.isRun ? 2.0f : 1f, 0.5f));

        if (rigi.velocity.magnitude > 5.0f)
        {
            anim.SetTrigger(rollKey);
        }

        if (pi.jump)
        {
            anim.SetTrigger(jumpKey);
        }

        if (pi.attack)
        {
            anim.SetTrigger(attackKey);
        }

        if (pi.dmag >= 0.1f)
        {
            //角度建议用Slerp,避免出现死锁的情况
            Model.transform.forward = Vector3.Slerp(Model.transform.forward, pi.dVec, 0.3f);
        }

        if (!lockPlanar)
        {
            planarVec = pi.dmag * Model.transform.forward * wakeSpeed * (pi.isRun ? runMultiplier : 1f);
        }
    }

    private void FixedUpdate()
    {
        rigi.velocity = new Vector3(planarVec.x, rigi.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
    }

    private void OnJumpEnter()
    {
        pi.inputEnable = false;
        thrustVec = new Vector3(0, jumpVelocity, 0);
        lockPlanar = true;
    }

    public void OnGroundEnter()
    {
        pi.inputEnable = true;
        lockPlanar = false;
    }

    public void IsGround()
    {
        anim.SetBool(isGroundKey, true);
    }

    public void IsNotGround()
    {
        anim.SetBool(isGroundKey, false);
    }

    public void OnFallEnter()
    {
        pi.inputEnable = false;
        lockPlanar = true;
    }

    public void OnRollEnter()
    {
        thrustVec = new Vector3(0, rollVelocity, 0);
        pi.inputEnable = false;
        lockPlanar = true;
    }

    public void OnJabEnter()
    {
        pi.inputEnable = false;
        lockPlanar = true;
    }

    public void OnJabUpdate()
    {
        thrustVec = Model.transform.forward * anim.GetFloat("jabVelocity") * jabMultiplier;
    }

    public void OnAttack1hAEnter()
    {
        anim.SetLayerWeight(attackLayer, 1.0f);

    }

    public void OnAttackIdle()
    {
        anim.SetLayerWeight(attackLayer, 0f);
    }
}