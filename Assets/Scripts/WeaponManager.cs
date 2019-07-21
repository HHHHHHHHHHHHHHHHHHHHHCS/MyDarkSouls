using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public ActorManager actorManager;
    public GameObject leftHandle, rightHandle;

    private Collider weaponCollider;
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

        weaponCollider = rightHandle.transform.GetChild(0).GetComponent<Collider>();
    }

    public void WeaponEnable()
    {
        weaponCollider.enabled = true;
    }

    public void WeaponDisable()
    {
        weaponCollider.enabled = false;
    }
}