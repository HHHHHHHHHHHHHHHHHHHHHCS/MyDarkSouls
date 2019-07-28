using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public ActorManager actorManager;
    private GameObject leftHandle, rightHandle;

    private Collider weaponColliderL, weaponColliderR;
    private WeaponMsgSender sender;

    private void Awake()
    {
        var senderTs = transform.Find("ybot");
        sender = senderTs.GetComponent<WeaponMsgSender>();
        if (!sender)
        {
            sender = senderTs.gameObject.AddComponent<WeaponMsgSender>();
        }

        sender.weaponManager = this;

        leftHandle = transform.DeepFind("LWeaponHandle").gameObject;
        rightHandle = transform.DeepFind("RWeaponHandle").gameObject;

        weaponColliderL = rightHandle.transform.GetChild(0).GetComponent<Collider>();
        weaponColliderR = rightHandle.transform.GetChild(0).GetComponent<Collider>();
    }

    public void WeaponEnable()
    {
        if (actorManager.actorController.CheckState("attackL"))
        {
            weaponColliderL.enabled = true;
        }
        else
        {
            weaponColliderR.enabled = true;

        }
    }

    public void WeaponDisable()
    {
        weaponColliderL.enabled = false;
        weaponColliderR.enabled = false;
    }
}