using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IActorManager
{
    public int maxHp = 30;
    public int hp = 15;

    [Header("1st order state flags")]
    public bool isGround;
    public bool isJump;
    public bool isFall;
    public bool isRoll;
    public bool isJab;
    public bool isAttack;
    public bool isHit;
    public bool isDie;
    public bool isBlocked;
    public bool isDefense;

    [Header("2nd order state flags")]
    public bool isAllowDefense;

    private void Update()
    {
        isGround = actorManager.actorController.CheckState("ground");
        isJump = actorManager.actorController.CheckState("jump");
        isFall = actorManager.actorController.CheckState("fall");
        isRoll = actorManager.actorController.CheckState("roll");
        isJab = actorManager.actorController.CheckState("jab");
        isAttack = actorManager.actorController.CheckStateTag("attackR")
            || actorManager.actorController.CheckStateTag("attackL");
        isHit = actorManager.actorController.CheckState("hit");
        isDie = actorManager.actorController.CheckState("die");
        isBlocked = actorManager.actorController.CheckState("blocked");

        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense&& actorManager.actorController.CheckState("defense", "defense");
    }

    public void AddHp(int value)
    {
        hp = Mathf.Clamp(hp + value, 0, maxHp);


    }
}