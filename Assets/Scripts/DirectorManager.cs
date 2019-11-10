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

        TimelineAsset timeline = (TimelineAsset) pd.playableAsset;

        foreach (var track in timeline.GetOutputTracks())
        {
            Object obj = null;

            switch (track.name)
            {
                case "Attacker Animation":
                    obj = attacker.actorController.anim;
                    break;
                case "Attacker Script":
                    obj = attacker;
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myClip = clip.asset as MySuperPlayableClip;
                        MySuperPlayableBehaviour myBehav = myClip.template;
                        myBehav.myFloat = 777;  
                    }
                    break;
                case "Victim Animation":
                    obj = victim.actorController.anim;
                    break;
                case "Victim Script":
                    obj = victim;
                    break;
            }

            if (obj != null)
                pd.SetGenericBinding(track, obj);
        }

        /*
        foreach (var trackBinding in pd.playableAsset.outputs)
        {
            Object obj = null;
            switch (trackBinding.streamName)
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
                pd.SetGenericBinding(trackBinding.sourceObject, obj);
        }
        */

        pd.Play();
    }
}