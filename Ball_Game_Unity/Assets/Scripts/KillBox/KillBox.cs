using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.root.GetComponentInChildren<CheckpointManager>().RestartToChecpoint();
        }
    }
}
