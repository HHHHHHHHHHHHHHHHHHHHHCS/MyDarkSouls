using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour
{
    private readonly int defenseKey = Animator.StringToHash("defense");

    public Vector3 eulerAngles;
    private Animator anim;
    private ActorController ac;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        ac = GetComponentInParent<ActorController>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (ac.leftIsShield && !anim.GetBool(defenseKey))
        {
            Transform leftLowArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            leftLowArm.localEulerAngles += eulerAngles;
            anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowArm.localEulerAngles));
        }
    }
}
