using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManager
{
    private GameObject leftHandle, rightHandle;

    private Collider weaponColliderL, weaponColliderR;
    private WeaponSender sender;
    private WeaponController wlC, wrC;

    private void Awake()
    {
        var senderTs = transform.Find("ybot");
        sender = senderTs.GetComponent<WeaponSender>();
        if (!sender)
        {
            sender = senderTs.gameObject.AddComponent<WeaponSender>();
        }

        sender.weaponManager = this;

        leftHandle = transform.DeepFind("LWeaponHandle").gameObject;
        rightHandle = transform.DeepFind("RWeaponHandle").gameObject;


        wlC = BindWeaponController(leftHandle);
        wrC = BindWeaponController(rightHandle);

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


    public WeaponController BindWeaponController(GameObject targetObj)
    {
        WeaponController tempWC;
        tempWC = targetObj.GetComponent<WeaponController>();
        if (tempWC == null)
        {
            tempWC = targetObj.AddComponent<WeaponController>();
        }

        tempWC.weaponManager = this;

        return tempWC;
    }

    public void WeaponDisable()
    {
        weaponColliderL.enabled = false;
        weaponColliderR.enabled = false;
    }
}