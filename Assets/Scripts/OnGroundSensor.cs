using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    /// <summary>
    /// 把胶囊体,半径缩小,中心往下移动,为了方便检测斜坡和掉落检测
    /// </summary>
    public float offset = 0.1f;

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

        radius = capCol.radius - 0.05f;
    }

    private void FixedUpdate()
    {
        point1 = transform.position + transform.up * (radius - offset);
        point2 = transform.position + transform.up * (capCol.height - offset) - transform.up * radius;

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
