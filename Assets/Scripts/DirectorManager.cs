using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

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
            if (track.streamName.Contains("Attacker"))
            {
                if (track.streamName.Contains("Script"))
                {
                    pd.SetGenericBinding(track.sourceObject, attacker);
                }
                else
                {
                    pd.SetGenericBinding(track.sourceObject, attacker.actorController.anim);
                }
            }
            else if (track.streamName.Contains("Victim"))
            {
                if (track.streamName.Contains("Script"))
                {
                    pd.SetGenericBinding(track.sourceObject, victim);
                }
                else
                {
                    Debug .Log(victim.actorController);
                    pd.SetGenericBinding(track.sourceObject, victim.actorController.anim);
                }
            }
        }

        pd.Play();
    }
}