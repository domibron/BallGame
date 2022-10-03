using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 VeiwportToWorld(Camera cam, Vector3 pos)
    {
        pos.z = cam.nearClipPlane;
        return cam.ViewportToWorldPoint(pos);
    }
}
