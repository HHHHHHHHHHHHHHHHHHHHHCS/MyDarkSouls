using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBones : MonoBehaviour
{
    public SkinnedMeshRenderer srcMeshRenderer;
    public SkinnedMeshRenderer tgtMeshRenderer;

    void Awake()
    {
        tgtMeshRenderer.bones = srcMeshRenderer.bones;
    }

}