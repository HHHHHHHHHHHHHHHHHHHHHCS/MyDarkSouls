using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Playables;

public class TestDirector : MonoBehaviour
{
    public Animator attacker;
    public Animator victim;

    private PlayableDirector pd;


    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (var track in pd.playableAsset.outputs)
            {
                if (track.streamName.Contains("Attacker"))
                {
                    pd.SetGenericBinding(track.sourceObject, attacker);
                }
                else if (track.streamName.Contains("Victim"))
                {
                    pd.SetGenericBinding(track.sourceObject, victim);
                }
            }

            //pd.Stop();
            //pd.time = 0;
            //pd.Evaluate();//重新计算位置
            pd.Play();
        }
    }
}