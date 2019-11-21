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

        if (targetWc == null)
        {
            return;
        }

        GameObject attacker = targetWc.weaponManager.actorManager.actorController.Model;
        GameObject receiver = actorManager.actorController.Model;

        Vector3 attackingDir = receiver.transform.position - attacker.transform.position;
        attackingDir.y = 0;
        Vector3 attackerForward = attacker.transform.forward;
        attackerForward.y = 0;
        float attackingAngle = Vector3.Angle(attackingDir, attackerForward);

        Vector3 receiverForward = receiver.transform.forward;
        receiverForward.y = 0;

        Vector3 counterDir = -attackingDir;

        float counterAngle1 = Vector3.Angle(receiverForward, counterDir);
        float counterAngle2 = Vector3.Angle(attackerForward, receiverForward);

        bool attackValid = (attackingAngle < 45f);
        bool counterValid = (counterAngle1 < 30 && Mathf.Abs(counterAngle2 - 180) < 30);

        if (attackingAngle <= 45f)
        {
            actorManager.TryDoDamage(targetWc, attackValid, counterValid);
        }
    }

    public void DisableMsgSender()
    {
        msgSender.DoDisable();
    }

    public static bool CheckAnglePlayer(GameObject player, GameObject target, float playerAngleLimit)
    {
        Vector3 counterDir = target.transform.position - player.transform.position;
        counterDir.y = 0;

        Vector3 targetForward = target.transform.forward;
        targetForward.y = 0;

        Vector3 playerForward = player.transform.forward;
        playerForward.y = 0;

        float counterAngle1 = Vector3.Angle(playerForward, counterDir);
        float counterAngle2 = Vector3.Angle(targetForward, playerForward);

        bool counterValid = (counterAngle1 < playerAngleLimit && Mathf.Abs(counterAngle2 - 180f) < playerAngleLimit);
        return counterValid;
    }

    public static bool CheckTargetPlayer(GameObject player, GameObject target, float targetAngleLimit)
    {
        Vector3 attackingDir = player.transform.position - target.transform.position;
        attackingDir.y = 0;

        Vector3 targetForward = target.transform.forward;
        targetForward.y = 0;

        float attackAngle = Vector3.Angle(targetForward, attackingDir);

        bool attackValid = attackAngle < targetAngleLimit;
        return attackValid;
    }
}