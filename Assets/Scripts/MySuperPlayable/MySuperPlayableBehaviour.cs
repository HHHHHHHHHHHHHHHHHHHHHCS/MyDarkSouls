using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public ActorManager actorManager;
    public float myFloat;

    private PlayableDirector pd;

    public override void OnPlayableCreate(Playable playable)
    {
        //Debug.Log("OnPlayableCreate");
    }

    public override void OnGraphStart(Playable playable)
    {
        //Debug.Log("OnGraphStart");
        //�������Ǵ�PD  ��Ϊ֪��Graph �� Resolver
        pd = playable.GetGraph().GetResolver() as PlayableDirector;
    }

    public override void OnGraphStop(Playable playable)
    {
        //Debug.Log("OnGraphStop");
        //ֹͣ��ʱ���Ѿ���ȡ����Graph �� Resolver��
        if (pd)
        {
            pd.playableAsset = null;
        }
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        //Debug.Log("OnBehaviourPlay");
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        //Debug.Log("OnBehaviourPause");
        actorManager.LockUnlockActorController(false);
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        //Debug.Log("PrepareFrame");
        actorManager.LockUnlockActorController(true);
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        //Debug.Log("OnPlayableDestroy");
    }
}