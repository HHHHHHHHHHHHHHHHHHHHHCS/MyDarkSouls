using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    private static DirectorManager directorManager;

    [HideInInspector] public ActorController actorController;
    private BattleManager battleManager;
    private WeaponManager weaponManager;
    private StateManager stateManager;
    private EventCasterManager eventCasterManager;
    private InteractionManager interactionManager;

    public StateManager StateManager => stateManager;

    private void Awake()
    {
        actorController = GetComponent<ActorController>();

        battleManager = Bind<BattleManager>();
        weaponManager = Bind<WeaponManager>();
        stateManager = Bind<StateManager>();
        eventCasterManager = Bind<EventCasterManager>(transform.Find("Character").DeepFind("Caster")?.gameObject);
        directorManager = Bind<DirectorManager>(GameObject.Find("Director"));
        interactionManager = Bind<InteractionManager>(battleManager.msgSender.gameObject);

        actorController.OnAction += DoAction;
    }

    private T Bind<T>(GameObject go = null) where T : IActorManager
    {
        go = go ?? gameObject;
        T temp = go.GetComponent<T>();
        if (temp == null)
        {
            temp = go.AddComponent<T>();
        }

        temp.actorManager = this;
        return temp;
    }

    public void TryDoDamage(WeaponController wc, bool attackValid, bool counterValid)
    {
        if (stateManager.isCounterBackSucceed)
        {
            if (counterValid)
            {
                wc.weaponManager.actorManager.Stunned();
            }
        }
        else if (stateManager.isCounterBackFailure)
        {
            if (attackValid)
            {
                HitOrDie(false);
            }
        }
        else if (stateManager.isImmortal)
        {
            //无敌状态
        }
        else if (stateManager.isDefense)
        {
            OnBlocked();
        }
        else
        {
            if (attackValid)
            {
                HitOrDie(true);
            }
        }
    }

    public void HitOrDie(bool doHitAnim)
    {
        if (stateManager.hp <= 0)
        {
        }
        else
        {
            DoDamage();
            if (stateManager.hp > 0)
            {
                if (doHitAnim)
                {
                    OnHit();
                }
            }
            else
            {
                OnDie();
            }
        }
    }

    public void Stunned()
    {
        actorController.IssueTrigger("stunned");
    }

    public void DoDamage()
    {
        stateManager.AddHp(-5);
    }

    public void OnBlocked()
    {
        actorController.IssueTrigger("blocked");
    }

    public void OnHit()
    {
        actorController.IssueTrigger("hit");
    }

    public void OnDie()
    {
        actorController.IssueTrigger("die");
        actorController.pi.enabled = false;

        if (actorController.Camcon.lockState)
        {
            actorController.Camcon.LockUnlock();
        }

        actorController.Camcon.enabled = false;
        battleManager.DisableMsgSender();
    }


    public void SetIsCounterBack(bool value)
    {
        stateManager.isCounterBackEnabled = value;
    }

    public void LockUnlockActorController(bool value)
    {
        actorController.SetBool("lock", value);
    }

    public void DoAction()
    {
        if (interactionManager.overlapEcastms.Count != 0)
        {
            if (interactionManager.overlapEcastms[0].eventName == "frontStab")
            {
                directorManager.PlayFrontStab(this, interactionManager.overlapEcastms[0].actorManager);
            }
        }
    }
}