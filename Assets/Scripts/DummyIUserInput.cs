using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIUserInput : IUserInput
{
    public bool isStatic = false;

    private IEnumerator Start()
    {
        if (!isStatic)
        {
            dUp = 1.0f;
            dRight = 0;
            yield return new WaitForSeconds(1f);
            dUp = 0f;
            dRight = 1;
            yield return new WaitForSeconds(1f);
            dUp = 0f;
            dRight = 0;
        }
    }


    private void Update()
    {
        if (!isStatic)
        {
            rightAttack = true;
            UpdateDmagDevc(dUp, dRight);
        }
    }
}