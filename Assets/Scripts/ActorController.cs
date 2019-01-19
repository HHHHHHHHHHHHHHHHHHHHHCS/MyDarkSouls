using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    private PlayerInput pi;
    private GameObject model;
    private Animator anim;

    private void Awake()
    {
        pi = transform.GetComponent<PlayerInput>();
        model = transform.Find("ybot").gameObject;
        anim = model.GetComponent<Animator>();
    }

    private void Update()
    {
        //Debug.Log(pi.dUp);
        anim.SetFloat("forward", pi.dmag);
        model.transform.forward = pi.dVec;
    }
}