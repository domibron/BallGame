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
    }
}
#endif
