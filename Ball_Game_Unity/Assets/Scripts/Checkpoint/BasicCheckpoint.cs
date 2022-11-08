using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCheckpoint : MonoBehaviour
{
    public GameObject point;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.transform.root.GetComponentInChildren<Rigidbody>();

        rb.velocity = Vector3.zero;

        rb.transform.position = point.transform.position;
    }
}
