using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCasterManager : IActorManager
{
    public string eventName;
    public bool active;

    private void Start()
    {
        if (actorManager == null)
        {
            actorManager = GetComponentInParent<ActorManager>();
        }
    }
}
