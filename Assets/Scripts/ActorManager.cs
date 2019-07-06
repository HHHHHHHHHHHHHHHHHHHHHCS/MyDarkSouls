using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    private BattleManager battleManager;

    private void Awake()
    {
        battleManager = GetComponent<BattleManager>();
        if (battleManager == null)
        {
            battleManager = gameObject.AddComponent<BattleManager>();
        }

        battleManager.actorManager = this;
    }
}
