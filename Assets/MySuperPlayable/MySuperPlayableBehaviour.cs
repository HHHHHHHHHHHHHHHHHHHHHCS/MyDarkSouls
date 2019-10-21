using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public Camera myCamera;
    public float myFloat;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnGraphStart(Playable playable)
    {
        Debug.Log("OnGraphStart");
    }

    public override void OnGraphStop(Playable playable)
    {
        Debug.Log("OnGraphStop");
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        Debug.Log("OnBehaviourPlay");
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        Debug.Log("OnBehaviourPause");

    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        Debug.Log("PrepareFrame");

    }
}
