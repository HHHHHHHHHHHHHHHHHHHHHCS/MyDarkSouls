using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManager
{
    public bool isPlayer;
    public WeaponController wcL, wcR;

    private GameObject leftHandle, rightHandle;

    private Collider weaponColliderL, weaponColliderR;
    private WeaponSender sender;

    private void Awake()
    {
        var senderTs = transform.Find("Character");
        sender = senderTs.GetComponent<WeaponSender>();
        if (!sender)
        {
            sender = senderTs.gameObject.AddComponent<WeaponSender>();
        }

        sender.weaponManager = this;

        leftHandle = transform.DeepFind("LWeaponHandle").gameObject;
        rightHandle = transform.DeepFind("RWeaponHandle").gameObject;


        wcL = BindWeaponController(leftHandle);
        wcR = BindWeaponController(rightHandle);

        if (leftHandle.transform.childCount > 0)
            weaponColliderL = leftHandle.transform.GetChild(0).GetComponent<Collider>();
        if (rightHandle.transform.childCount > 0)
            weaponColliderR = rightHandle.transform.GetChild(0).GetComponent<Collider>();
    }

    private void Start()
    {
        if (isPlayer)
        {
            GameManager.Instance.playerWeaponManger = this;
        }
    }

    public void UpdateWeaponCollider(string side, Collider col)
    {
        if (side == "L")
        {
            weaponColliderL = col;
        }
        else if (side == "R")
        {
            weaponColliderR = col;
        }
    }

    public void UnloadWeapon(string side)
    {
        Transform root = null;
        if (side == "L")
        {
            wcL.weaponData = null;
            weaponColliderL = null;
            root = wcL.transform;
        }
        else if (side == "R")
        {
            wcR.weaponData = null;
            weaponColliderR = null;
            root = wcR.transform;
        }

        if (root)
        {
            foreach (Transform item in root)
            {
                GameObject.Destroy(item);
            }
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

    public void WeaponEnable()
    {
        if (actorManager.actorController.CheckState("attackL") && (weaponColliderL != null))
        {
            weaponColliderL.enabled = true;
        }
        else if (weaponColliderR != null)
        {
            weaponColliderR.enabled = true;
        }
    }


    public void WeaponDisable()
    {
        if (weaponColliderL != null)
            weaponColliderL.enabled = false;
        if (weaponColliderR != null)
            weaponColliderR.enabled = false;
    }

    public void CounterBackEnable()
    {
        actorManager.SetIsCounterBack(true);
    }

    public void CounterBackDisable()
    {
        actorManager.SetIsCounterBack(false);
    }
}