using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformHelper
{
    public static Transform DeepFind(this Transform ts, string targetName)
    {
        foreach (Transform child in ts)
        {
            if (child.name.Equals(targetName))
            {
                return child;
            }

            var temp = DeepFind(child, targetName);
            if (temp != null)
            {
                return temp;
            }
        }

        return null;
    }
}