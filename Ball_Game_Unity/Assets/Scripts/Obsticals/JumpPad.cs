using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float power;
    public float coolDown;

    private float coolDownTime;
    private float time;

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (time >= coolDownTime)
        {
            Rigidbody rb = other.transform.root.GetComponentInChildren<Rigidbody>();

            rb.AddForce(Vector3.up * power, ForceMode.Impulse);

            coolDownTime = time + coolDown;
        }
    }
}
