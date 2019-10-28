using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public GameObject myCamera;
    public float myFloat;

    private PlayableDirector pd;

    public override void OnPlayableCreate(Playable playable)
    {
        //Debug.Log("OnPlayableCreate");
    }

    public override void OnGraphStart(Playable playable)
    {
        //Debug.Log("OnGraphStart");
        if (Application.isPlaying)
        {
            //暂存PD给STOP的时候用
            pd = playable.GetGraph().GetResolver() as PlayableDirector;

            foreach (var track in pd.playableAsset.outputs)
            {
                if (track.streamName.Contains("Attack Script"))
                {
                    ActorManager am = pd.GetGenericBinding(track.sourceObject) as ActorManager;
                    am.LockUnlockActorController(true);
                }
            }
        }
    }

    public override void OnGraphStop(Playable playable)
    {
        //Debug.Log("OnGraphStop");
        if (Application.isPlaying)
        {
            if (pd)
            {
                foreach (var track in pd.playableAsset.outputs)
                {
                    if (track.streamName.Contains("Attack Script"))
                    {
                        ActorManager am = pd.GetGenericBinding(track.sourceObject) as ActorManager;
                        am.LockUnlockActorController(false);
                    }
                }
            }
        }
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        //Debug.Log("OnBehaviourPlay");
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        //Debug.Log("OnBehaviourPause");
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        //Debug.Log("PrepareFrame");
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        //Debug.Log("OnPlayableDestroy");
    }
}