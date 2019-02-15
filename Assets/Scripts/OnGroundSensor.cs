using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    private ActorController actCtrl;
    private CapsuleCollider capCol;

    private float radius;
    private Vector3 point1;
    private Vector3 point2;
    private int raycatLayer;

    private void Awake()
    {
        raycatLayer = LayerMask.GetMask("Ground");
        var parent = transform.parent;
        actCtrl = parent.GetComponent<ActorController>();
        capCol = parent.GetComponent<CapsuleCollider>();

        radius = capCol.radius;
    }

    private void FixedUpdate()
    {
        point1 = transform.position + transform.up * radius;
        point2 = transform.position + transform.up * capCol.height - transform.up * radius;

        Collider[] outputCols = Physics.OverlapCapsule(point1, point1, radius, raycatLayer);
        if (outputCols.Length != 0)
        {
            //foreach (var item in outputCols)
            //{
            //    Debug.Log(item.name);
            //}
            actCtrl.IsGround();
        }
        else
        {
            actCtrl.IsNotGround();
        }
    }
}
