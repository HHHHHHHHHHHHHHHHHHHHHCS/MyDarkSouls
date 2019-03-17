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

    [Space(10)] [Header("===== Friction Settings ====")]
    public PhysicMaterial frictionOne;

    public PhysicMaterial frctionZero;


    private IUserInput pi;
    private Animator anim;
    private Rigidbody rigi;
    private Vector3 planarVec; //跳跃 平面量
    private Vector3 thrustVec; //偏移的量
    private bool lockPlanar; //锁移动的量
    private bool canAttack = true; //是否可以攻击
    private CapsuleCollider capCol; //玩家的碰撞盒
    private float lerpTarget; // 动画层过渡用
    private Vector3 deltaPos; // 动画位置偏移用

    private int attackLayer; //攻击的Layer

    public GameObject Model { get; private set; }


    private void Awake()
    {
        var inputs = transform.GetComponents<IUserInput>();
        foreach (var item in inputs)
        {
            if (item.enabled)
            {
                pi = item;
                break;
            }
        }

        Model = transform.Find("ybot").gameObject;
        anim = Model.GetComponent<Animator>();
        rigi = transform.GetComponent<Rigidbody>();
        capCol = transform.GetComponent<CapsuleCollider>();

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
            canAttack = false;
        }

        if (pi.attack && CheckState("ground") && canAttack)
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
        rigi.position += deltaPos;
        rigi.velocity = new Vector3(planarVec.x, rigi.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }

    private bool CheckState(string stateName, string layerName = "Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
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
        canAttack = true;
        capCol.material = frictionOne;
    }

    public void OnGroundExit()
    {
        capCol.material = frctionZero;
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
        pi.inputEnable = false;
        lerpTarget = 1.0f;
    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = Model.transform.forward * anim.GetFloat("attack1hVelocity");
        anim.SetLayerWeight(attackLayer, Mathf.Lerp(anim.GetLayerWeight(attackLayer), lerpTarget, 0.4f));
    }

    public void OnAttackIdleEnter()
    {
        pi.inputEnable = true;
        lerpTarget = 0f;
    }

    public void OnAttackIdleUpdate()
    {
        anim.SetLayerWeight(attackLayer, Mathf.Lerp(anim.GetLayerWeight(attackLayer), lerpTarget, 0.4f));
    }

    public void OnUpdateRM(object _deltaPos)
    {
        if (CheckState("attack1hC", "attack"))
        {
            //deltaPos += (deltaPos + (Vector3) _deltaPos) / 2.0f;
            deltaPos += (0.6f*deltaPos + 0.4f*(Vector3)_deltaPos) / 1.0f;
        }
    }
}