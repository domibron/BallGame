using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if (UNITY_EDITOR)
[ExecuteInEditMode]
public class GizmoShower : MonoBehaviour
{
    [Space]
    public bool enable = true;
    [Space]
    public bool Square = true;
    [Space]
    public bool Sphere;
    public float radius = 1f;
    [Space]
    public bool Line;
    public float distance = 1f;
    public bool bodyUp = false;
    public Vector3 direction = Vector3.up;

    private void OnDrawGizmos()
    {
        if (!enable || Application.isPlaying)
            return;

        if (Square)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(transform.position, transform.localScale);
        }

        if (Sphere)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, radius);
        }

        if (Line)
        {
            // checks if you want it to go straight up or up relitive to the object's rotation
            if (!bodyUp)
                Debug.DrawRay(transform.position, direction, Color.white, distance);
            else
                Debug.DrawRay(transform.position, transform.up, Color.white, distance);
        }


    }
}
#endif
