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

    public static Vector3 ViewportToScreenPoint(Camera cam, Vector3 pos) // use this other wise camera is going to bork.
    {
        pos.z = cam.nearClipPlane;
        return cam.ViewportToScreenPoint(pos);
    }
}
