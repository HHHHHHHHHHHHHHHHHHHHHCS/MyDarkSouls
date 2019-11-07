using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : IActorManager
{
    public List<EventCasterManager> overlapEcastms = new List<EventCasterManager>();

    private CapsuleCollider theCollider;

    private void Awake()
    {
        theCollider = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider col)
    {
        EventCasterManager[] ecastm = col.GetComponents<EventCasterManager>();
        foreach (var caster in ecastm)
        {
            if (!overlapEcastms.Contains(caster))
            {
                overlapEcastms.Add(caster);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        EventCasterManager[] ecastm = col.GetComponents<EventCasterManager>();
        foreach (var caster in ecastm)
        {
            if (overlapEcastms.Contains(caster))
            {
                overlapEcastms.Remove(caster);
            }
        }
    }
}
