using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : IActorManager
{
    private BattleMsgSender msgSender;
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
        actorManager.TryDoDamage();
    }
}