using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ActorController : MonoBehaviour
{
    public float wakeSpeed = 1.4f;
    public float runMultiplier = 2.75f;

    private PlayerInput pi;
    private GameObject model;
    private Animator anim;
    private Rigidbody rigi;
    private Vector3 movingVec;

    private void Awake()
    {
        pi = transform.GetComponent<PlayerInput>();
        model = transform.Find("ybot").gameObject;
        anim = model.GetComponent<Animator>();
        rigi = transform.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        anim.SetFloat("forward", pi.dmag * (pi.isRun ? 2.0f : 1f));
        if (pi.dmag >= 0.1f)
        {
            model.transform.forward = pi.dVec;
        }

        movingVec = pi.dmag  * model.transform.forward * wakeSpeed * (pi.isRun ? runMultiplier : 1f);
    }

    private void FixedUpdate()
    {
        //rigi.position += movingVec * Time.fixedDeltaTime;
        rigi.velocity = new Vector3(movingVec.x, rigi.velocity.y, movingVec.z);
    }
}