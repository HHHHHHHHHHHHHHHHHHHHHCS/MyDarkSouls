using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IActorManager
{
    public int maxHp = 30;
    public int hp = 15;

    public void AddHp(int value)
    {
        hp = Mathf.Clamp(hp + value, 0, maxHp);
        if (value > 0)
        {
            return;
        }

        if (hp > 0)
        {
            actorManager.OnHit();
        }
        else
        {
            actorManager.OnDie();
        }
    }
}