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
        if (pd.playableAsset != null)
        {
            return;
        }

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
                        //给他一个单独的KEY 防止地址重复
                        myClip.actorManager.exposedName = System.Guid.NewGuid().ToString();
                        pd.SetReferenceValue(myClip.actorManager.exposedName,attacker);
                    }
                    break;
                case "Victim Animation":
                    obj = victim.actorController.anim;
                    break;
                case "Victim Script":
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myClip = clip.asset as MySuperPlayableClip;
                        MySuperPlayableBehaviour myBehav = myClip.template;
                        myBehav.myFloat = 6666;
                        myClip.actorManager.exposedName = System.Guid.NewGuid().ToString();
                        pd.SetReferenceValue(myClip.actorManager.exposedName, victim);
                    }
                    break;
            }

            if (obj != null)
                pd.SetGenericBinding(track, obj);
        }

        // 强行执行一次插值 , 把值初始一次
        pd.Evaluate();

        pd.Play();

    }

    public void OpenBox()
    {
        Debug.Log("OPEN");
    }

}