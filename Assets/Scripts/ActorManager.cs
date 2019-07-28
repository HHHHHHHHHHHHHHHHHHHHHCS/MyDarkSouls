using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    [HideInInspector]
    public ActorController actorController;
    private BattleManager battleManager;
    private WeaponManager weaponManager;

    private void Awake()
    {
        actorController = GetComponent<ActorController>();

        battleManager = GetComponent<BattleManager>();
        if (!battleManager)
        {
            battleManager = gameObject.AddComponent<BattleManager>();
        }

        battleManager.actorManager = this;

        weaponManager = GetComponent<WeaponManager>();
        if (!weaponManager)
        {
            weaponManager = gameObject.AddComponent<WeaponManager>();
        }

        weaponManager.actorManager = this;
    }


    public void DoDamage()
    {
        //actorController
        actorController.IssueTrigger("die");
    }
}