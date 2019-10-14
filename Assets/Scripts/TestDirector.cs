using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TestDirector : MonoBehaviour
{
    public PlayableDirector pd;

    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            pd.Stop();
            pd.time = 0;
            pd.Evaluate();//重新计算位置
            pd.Play();
        }
    }
}