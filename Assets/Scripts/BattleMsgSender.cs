using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleMsgSender : MonoBehaviour
{
    public BattleManager battleManager;

    private void Awake()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
