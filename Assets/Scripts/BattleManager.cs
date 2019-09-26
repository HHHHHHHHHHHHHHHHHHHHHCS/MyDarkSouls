using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : IActorManager
{
    public BattleMsgSender msgSender;
    private CapsuleCollider defCol;

    private void Awake()
    {
        var senderTs = transform.Find("Sensor");
        msgSender = senderTs.GetComponent<BattleMsgSender>();
        if (msgSender == null)
        {
            msgSender = senderTs.gameObject.AddComponent<BattleMsgSender>();
        }

        msgSender.battleManager = this;

        defCol = msgSender.GetComponent<CapsuleCollider>();
        defCol.center = Vector3.up;
        defCol.height = 2.0f;
        defCol.radius = 0.5f;
        defCol.isTrigger = true;
    }

    public void AcceptSender(GameObject go)
    {
        var targetWc = go.transform.parent.GetComponent<WeaponController>();

        GameObject attacker = targetWc.weaponManager.actorManager.actorController.Model;
        GameObject receiver = actorManager.actorController.Model;

        Vector3 attackingDir = receiver.transform.position - attacker.transform.position;
        attackingDir.y = 0;

        Vector3 forward = attacker.transform.forward;
        forward.y = 0;

        float attackingAngle = Vector3.Angle(attackingDir, forward);
        //TODO:
        if (attackingAngle <= 45f)
        {
            actorManager.TryDoDamage(targetWc);
        }
    }

    public void DisableMsgSender()
    {
        msgSender.DoDisable();
    }
}