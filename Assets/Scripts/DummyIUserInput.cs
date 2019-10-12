using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIUserInput : IUserInput
{
    
    private IEnumerator Start()
    {

        dUp = 1.0f;
        dRight = 0;
        yield return  new WaitForSeconds(1f);
        dUp = 0f;
        dRight = 1;
        yield return new WaitForSeconds(1f);
        dUp = 0f;
        dRight = 0;
    }
    

    private void Update()
    {
        rightAttack = true;
        UpdateDmagDevc(dUp,dRight);
    }
}
