using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public MySuperPlayableBehaviour template = new MySuperPlayableBehaviour();
    public ExposedReference<ActorManager> actorManager;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MySuperPlayableBehaviour>.Create(graph, template);
        MySuperPlayableBehaviour clone = playable.GetBehaviour();
        //给他一个单独的KEY 防止地址重复
        actorManager.exposedName = GetInstanceID().ToString();
        clone.actorManager = actorManager.Resolve(graph.GetResolver());
        return playable;
    }
} 