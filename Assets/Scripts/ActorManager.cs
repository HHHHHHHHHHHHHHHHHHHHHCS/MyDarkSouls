using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    private ActorController actorController;
    private BattleManager battleManager;

    private void Awake()
    {
        battleManager = GetComponent<BattleManager>();
        if (!battleManager)
        {
            battleManager = gameObject.AddComponent<BattleManager>();
        }

        battleManager.actorManager = this;
        actorController = GetComponent<ActorController>();
    }


    public void DoDamage()
    {
        actorController.IssueTrigger("hit");
    }
}
