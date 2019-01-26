﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ActorController : MonoBehaviour
{
    private readonly int forwardKey = Animator.StringToHash("forward");
    private readonly int jumpKey = Animator.StringToHash("jump");

    public float wakeSpeed = 1.4f;
    public float runMultiplier = 2.75f;
    public float jumpVelocity = 3.0f;

    private PlayerInput pi;
    private GameObject model;
    private Animator anim;
    private Rigidbody rigi;
    private Vector3 planarVec; //跳跃 平面量
    private Vector3 thrustVec; //跳跃 垂直量

    private bool lockPlanar;


    private void Awake()
    {
        pi = transform.GetComponent<PlayerInput>();
        model = transform.Find("ybot").gameObject;
        anim = model.GetComponent<Animator>();
        rigi = transform.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        anim.SetFloat(forwardKey, pi.dmag * Mathf.Lerp(anim.GetFloat(forwardKey), pi.isRun ? 2.0f : 1f, 0.5f));

        if (pi.jump)
        {
            anim.SetTrigger(jumpKey);
        }

        if (pi.dmag >= 0.1f)
        {
            //角度建议用Slerp,避免出现死锁的情况
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.dVec, 0.3f);
        }

        if (!lockPlanar)
        {
            planarVec = pi.dmag * model.transform.forward * wakeSpeed * (pi.isRun ? runMultiplier : 1f);
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

    private void OnJumpExit()
    {
        pi.inputEnable = true;
        lockPlanar = false;
    }
}