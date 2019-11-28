using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCasterManager : IActorManager
{
    public string eventName;
    public bool active;
    public Vector3 offset = new Vector3(0, 0, 1.0f);

    private void Start()
    {
        if (actorManager == null)
        {
            actorManager = GetComponentInParent<ActorManager>();
        }
    }
}