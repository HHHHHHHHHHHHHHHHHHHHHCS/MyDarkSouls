﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleMsgSender : MonoBehaviour
{
    private const string weaponTag = "Weapon";

    public BattleManager battleManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(weaponTag))
        {
            battleManager.AcceptSender(other.gameObject);
        }
    }

    public void DoDisable()
    {
        GetComponent<Collider>().enabled = false;
        enabled = false;
    }
}
