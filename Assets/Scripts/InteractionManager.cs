using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : IActorManager
{
    private CapsuleCollider theCollider;

    private void Awake()
    {
        theCollider = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        EventCasterManager[] ecastm = other.GetComponents<EventCasterManager>();
        foreach (var caster in ecastm)
        {
        }
    }


}
