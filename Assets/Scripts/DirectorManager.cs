using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Object = UnityEngine.Object;

[RequireComponent(typeof(PlayableDirector))]
public class DirectorManager : IActorManager
{
    private PlayableDirector pd;

    [Header("=== Timeline assets ===")] public TimelineAsset frontStab;


    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
        pd.playOnAwake = false;
        //pd.playableAsset = Instantiate(frontStab);
    }

    public void PlayFrontStab(ActorManager attacker, ActorManager victim)
    {
        pd.playableAsset = Instantiate(frontStab);

        foreach (var track in pd.playableAsset.outputs)
        {
            Object obj = null;
            switch (track.streamName)
            {
                case "Attacker Animation":
                    obj = attacker.actorController.anim;
                    break;
                case "Attacker Script":
                    obj = attacker;
                    break;
                case "Victim Animation":
                    obj = victim.actorController.anim;
                    break;
                case "Victim Script":
                    obj = victim;
                    break;
            }

            if (obj != null)
                pd.SetGenericBinding(track.sourceObject, obj);
        }

        pd.Play();
    }
}