using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    private static DirectorManager directorManager;

    [HideInInspector] public ActorController actorController;

    public bool needBattle = true;
    public bool needWeapon = true;
    public bool needState = true;


    private BattleManager battleManager;
    private WeaponManager weaponManager;
    private StateManager stateManager;
    private InteractionManager interactionManager;

    public StateManager StateManager => stateManager;

    private void Awake()
    {
        actorController = GetComponent<ActorController>()?.Init();
        if (needBattle)
            battleManager = Bind<BattleManager>();
        if (needWeapon)
            weaponManager = Bind<WeaponManager>();
        if (needState)
            stateManager = Bind<StateManager>();
        directorManager = GameObject.Find("DirectorManager").GetComponent<DirectorManager>();
        if (needBattle)
            interactionManager = Bind<InteractionManager>(battleManager.msgSender.gameObject);
        else
            interactionManager = Bind<InteractionManager>(actorController.Model);

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
                HitOrDie(wc, false);
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
                HitOrDie(wc, true);
            }
        }
    }

    public void HitOrDie(WeaponController targetCtrl, bool doHitAnim)
    {
        if (stateManager.hp <= 0)
        {
        }
        else
        {
            DoDamage(targetCtrl.GetAtk());
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

    public void DoDamage(int damage)
    {
        stateManager.AddHp(-1 * damage);
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
            var eventObj = interactionManager.overlapEcastms[0];

            if (eventObj.active && !directorManager.IsPlaying)
            {
                if (eventObj.eventName == "frontStab")
                {
                    directorManager.PlayFrontStab(this, eventObj.actorManager);
                }
                else if (eventObj.eventName == "openBox")
                {
                    var target = eventObj.actorManager.transform;

                    if (BattleManager.CheckAnglePlayer(actorController.Model,
                        target.gameObject, 15))
                    {
                        eventObj.active = false;
                        transform.position = target.position
                                             + target.TransformVector(eventObj.offset);
                        actorController.Model.transform.LookAt(target, Vector3.up);
                        directorManager.PlayOpenBox(this, eventObj.actorManager);
                    }
                }
                else if (eventObj.eventName == "leverUp")
                {
                    var target = eventObj.actorManager.transform;

                    if (BattleManager.CheckAnglePlayer(actorController.Model,
                        target.gameObject, 15))
                    {
                        eventObj.active = false;
                        transform.position = target.position
                                             + target.TransformVector(eventObj.offset);
                        actorController.Model.transform.LookAt(target, Vector3.up);
                        directorManager.PlayLeverUp(this, eventObj.actorManager);
                    }
                }
            }
        }
    }
}