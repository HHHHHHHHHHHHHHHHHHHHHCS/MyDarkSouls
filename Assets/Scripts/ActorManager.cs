using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    [HideInInspector] public ActorController actorController;
    private BattleManager battleManager;
    private WeaponManager weaponManager;
    private StateManager stateManager;

    private void Awake()
    {
        actorController = GetComponent<ActorController>();

        battleManager = Bind<BattleManager>();
        weaponManager = Bind<WeaponManager>();
        stateManager = Bind<StateManager>();
    }

    private T Bind<T>(GameObject go = null) where T : IActorManager
    {
        go = go ?? gameObject;
        T temp = GetComponent<T>();
        if (temp == null)
        {
            temp = go.AddComponent<T>();
        }

        temp.actorManager = this;
        return temp;
    }

    public void TryDoDamage()
    {
        DoDamage();
    }

    public void DoDamage()
    {
        stateManager.AddHp(-5);
    }

    public void OnHit()
    {
        actorController.IssueTrigger("hit");

    }

    public void OnDie()
    {
        actorController.IssueTrigger("die");
        actorController.pi.inputEnable = false;
        if (actorController.Camcon.lockState)
        {
            actorController.Camcon.LockUnlock();
        }

        actorController.Camcon.enabled = false;
    }
}